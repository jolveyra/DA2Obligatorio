using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BuildingImporter;
using CustomExceptions;
using Domain;
using LogicInterfaces;
using RepositoryInterfaces;

namespace BusinessLogic
{
    public class ImportBuildingLogic : IImportBuildingLogic
    {
        private IImporterRepository _importerRepository;
        private IUserRepository _userRepository;
        private ISessionRepository _sessionRepository;
        private IBuildingRepository _buildingRepository;
        private IPeopleRepository _peopleRepository;
        private string _importerPath = @"..\..\Importers";
        private string _buildingFilesPath = @"..\..\BuildingFiles";

        public ImportBuildingLogic(IImporterRepository importerRepository, IUserRepository userRepository, ISessionRepository sessionRepository, IBuildingRepository buildingRepository)
        {
            _importerRepository = importerRepository;
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
            _buildingRepository = buildingRepository;
        }

        public void ImportBuildings(string dllName, string fileName)
        {
            Importer? importer = _importerRepository.GetAllImporters().FirstOrDefault(i => i.Name.Equals(dllName));

            if (importer == null)
            {
                throw new Exception("Importer not found");
            }
            
            Type implementedType = GetInterfaceImplementedType($"{_importerPath}\\{dllName}.dll");
            List<DTOBuilding> buildingsToImport = GetBuildingsToImport(fileName, implementedType);
            CreateBuildings(buildingsToImport);
        }

        private void CreateBuildings(List<DTOBuilding> buildingsToImport)
        {
            IEnumerable<User> users = _userRepository.GetAllUsers();
            List<Building> buildingsToCreate = new List<Building>();
            List<Flat> flatsToCreate = new List<Flat>();

            foreach (DTOBuilding dtoBuilding in buildingsToImport)
            {
                Guid? managerId = null;
                if (dtoBuilding.ManagerEmail is not null)
                {
                    User? manager = users.FirstOrDefault(u => u.Email.Equals(dtoBuilding.ManagerEmail) && u.Role == Role.Manager);
                    if (manager is not null)
                    {
                        managerId = manager.Id;
                    }
                    else
                    {
                        Guid addedManagerId = _userRepository.CreateUser(new User() { Name = "", Surname = "", Email = dtoBuilding.ManagerEmail, Password = "DefaultManager1234", Role = Role.Manager }).Id;
                        _sessionRepository.CreateSession(new Session() { UserId = addedManagerId });
                        managerId = addedManagerId;
                    }
                }
                Building building = new Building()
                {
                    Name = dtoBuilding.Name,
                    SharedExpenses = dtoBuilding.SharedExpenses,
                    Address = new Address()
                    {
                        Street = dtoBuilding.Street,
                        CornerStreet = dtoBuilding.CornerStreet,
                        DoorNumber = dtoBuilding.Number,
                        Latitude = dtoBuilding.Latitude,
                        Longitude = dtoBuilding.Longitude
                    },
                    ManagerId = managerId,
                };
                List<Flat> flats = ArrangeBuildingFlats(dtoBuilding.Flats, building);
                flatsToCreate.AddRange(flats);
                buildingsToCreate.Add(building);
            }

            if (buildingsToCreate.Any())
            {
                if (flatsToCreate.Any())
                {
                    //_buildingRepository.CreateFlats(flatsToCreate);
                }
                //_buildingRepository.CreateBuildings(buildingsToCreate);
            }
        }

        private List<Flat> ArrangeBuildingFlats(List<DTOFlat> flats, Building building)
        {
            IEnumerable<Person> people = _peopleRepository.GetPeople();
            List<Flat> flatsToCreate = new List<Flat>();

            foreach (DTOFlat dtoFlat in flats)
            {
                Person ownerToAdd = new Person() { Email = "", Name = "", Surname = "" };

                if (dtoFlat.OwnerEmail is not null)
                {
                    Person? owner = people.FirstOrDefault(u => u.Email.Equals(dtoFlat.OwnerEmail));
                    if (owner is not null)
                    {
                        ownerToAdd = owner;
                    }
                    else
                    {
                        ownerToAdd.Email = dtoFlat.OwnerEmail;
                        ownerToAdd = _peopleRepository.CreatePerson(ownerToAdd);
                    }
                }
                else
                {
                    ownerToAdd = _peopleRepository.CreatePerson(ownerToAdd);
                }
                Flat flat = new Flat()
                {
                    Number = dtoFlat.Number ?? 0,
                    Floor = dtoFlat.Floor ?? 0,
                    Bathrooms = dtoFlat.Bathrooms ?? 0,
                    Building = building,
                    HasBalcony = dtoFlat.HasBalcony ?? false,
                    Rooms = dtoFlat.Rooms ?? 0,
                    Owner = ownerToAdd,
                    OwnerId = ownerToAdd.Id
                };
                flatsToCreate.Add(flat);
            }

            return flatsToCreate;
        }

        private List<DTOBuilding> GetBuildingsToImport(string fileName, Type implementedType)
        {
            IBuildingImporter buildingImporter = (IBuildingImporter)Activator.CreateInstance(implementedType);

             return buildingImporter.ImportBuildingsFromFile($"{_buildingFilesPath}\\{fileName}");
        }

        private Type GetInterfaceImplementedType(string assemblyPath)
        {
            Assembly assembly = Assembly.LoadFrom(assemblyPath);

            Type? type = null;

            foreach (Type t in assembly.GetTypes())
            {
                if (typeof(IBuildingImporter).IsAssignableFrom(t) && t is { IsAbstract: false, IsInterface: false })
                {
                    type = t;
                }
            }

            if (type == null)
            {
                throw new ImportBuildingException("No type that implements necessary interface has been found in assembly");
            }

            return type;
        }
    }
}
