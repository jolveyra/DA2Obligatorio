using System.Reflection;
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
        private IBuildingLogic _buildingLogic;
        private IPeopleRepository _peopleRepository;
        private IConstructorCompanyBuildingLogic _constructorCompanyBuildingLogic;
        private string _importerPath = @".\Importers";
        private string _buildingFilesPath = @".\BuildingFiles";

        public ImportBuildingLogic(IImporterRepository importerRepository, IUserRepository userRepository,
            ISessionRepository sessionRepository, IBuildingLogic buildingLogic, IPeopleRepository peopleRepository
            , IConstructorCompanyBuildingLogic constructorCompanyBuildingLogic)
        {
            _importerRepository = importerRepository;
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
            _buildingLogic = buildingLogic;
            _peopleRepository = peopleRepository;
            _constructorCompanyBuildingLogic = constructorCompanyBuildingLogic;
        }

        public List<Building> ImportBuildings(string dllName, string fileName, Guid userId)
        {
            Importer importer = _importerRepository.GetImporterByName(dllName);
            
            Type implementedType = GetInterfaceImplementedType($"{_importerPath}\\{importer.Name}.dll");
            List<DTOBuilding> buildingsToImport = GetBuildingsToImport(fileName, implementedType);
            return CreateBuildings(buildingsToImport, userId);
        }

        private List<Building> CreateBuildings(List<DTOBuilding> buildingsToImport, Guid userId)
        {
            IEnumerable<User> users = _userRepository.GetAllUsers();
            List<Building> buildings = new List<Building>();

            foreach (DTOBuilding dtoBuilding in buildingsToImport)
            {
                Guid? managerId = GetManagerId(dtoBuilding, users);
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
                Building createdBuilding = _constructorCompanyBuildingLogic.CreateConstructorCompanyBuilding(building, dtoBuilding.Flats.Count, userId);
                CreateBuildingFlats(dtoBuilding.Flats, createdBuilding);
                buildings.Add(createdBuilding);
            }
            
            return buildings;
        }

        private Guid? GetManagerId(DTOBuilding dtoBuilding, IEnumerable<User> users)
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

            return managerId;
        }

        private void CreateBuildingFlats(List<DTOFlat> flats, Building building)
        {
            List<Flat> flatsToUpdate = _buildingLogic.GetAllBuildingFlats(building.Id).ToList();
            IEnumerable<Person> people = _peopleRepository.GetPeople();

            for (int i=0; i<flats.Count; i++)
            {
                Person ownerToAdd = new Person()
                {
                    Name = "",
                    Surname = "",
                    Email = flats[i].OwnerEmail
                };

                Flat flat = new Flat()
                {
                    Number = flats[i].Number,
                    Floor = flats[i].Floor,
                    Bathrooms = flats[i].Bathrooms,
                    Building = building,
                    HasBalcony = flats[i].HasBalcony,
                    Rooms = flats[i].Rooms,
                    Owner = ownerToAdd,
                    OwnerId = ownerToAdd.Id
                };
                _buildingLogic.UpdateFlat(building.Id, flatsToUpdate[i].Id, flat, true);
            }
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
                if (t.GetInterface("IBuildingImporter") is not null && t is { IsAbstract: false, IsInterface: false })
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
