using RepositoryInterfaces;
using LogicInterfaces;
using CustomExceptions.BusinessLogic;
using Domain;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using System.Drawing;

namespace BusinessLogic;

public class BuildingLogic: IBuildingLogic
{
    private const int maximumCharactersForConstructorCompany = 100;
    private IBuildingRepository _iBuildingRepository;
    private IUserRepository _iUserRepository;

    public BuildingLogic(IBuildingRepository iBuildingRepository, IUserRepository iUserRepository)
    {
        _iBuildingRepository = iBuildingRepository;
        _iUserRepository = iUserRepository;
    }

    public Building CreateBuilding(Building building, int amountOfFlats, Guid userId)
    {
        ValidateFlatAmount(amountOfFlats);

        ValidateBuilding(building);

        CheckUniqueBuildingAddress(building);

        CheckUniqueBuildingName(building);

        CheckUniqueBuildingCoordinates(building);

        CreateBuildingFlats(building, amountOfFlats);

        return _iBuildingRepository.CreateBuilding(building);
    }

    private void CheckUniqueBuildingCoordinates(Building building)
    {
        if(_iBuildingRepository.GetAllBuildings().ToList().Exists(x => x.Latitude == building.Latitude && x.Longitude == building.Longitude))
        {
            throw new BuildingException("Building with same coordinates already exists");
        }
    }

    private void CheckUniqueBuildingAddress(Building building)
    {
        if(_iBuildingRepository.GetAllBuildings().ToList().Exists(x => x.Street == building.Street && x.DoorNumber == building.DoorNumber))
        {
            throw new BuildingException("Building with same address already exists");
        }
    }

    private void ValidateFlatAmount(int amountOfFlats)
    {
        if (amountOfFlats <= 0)
        {
            throw new BuildingException("It is not possible to create a building with no flats");
        }
    }

    private void CreateBuildingFlats(Building building, int amountOfFlats)
    {
        List<Flat> flats = new List<Flat>();
        for (int i = 0; i < amountOfFlats; i++)
        {
            Flat flat = new Flat
            {
                Building = building,
                Id = Guid.NewGuid()
            };

            _iBuildingRepository.CreateFlat(flat);
        }
    }

    private void ValidateBuilding(Building building)
    {
        CheckNotEmptyBuildingName(building);
        CheckNotNegativeSharedExpenses(building.SharedExpenses);
        CheckNotEmptyBuildingDirection(building);
        CheckConstructorCompany(building);
        CheckValidCoordinates(building);
    }

    private void CheckValidCoordinates(Building building)
    {
        if (building.Latitude < -90 || building.Latitude > 90)
        {
            throw new BuildingException("Invalid latitude, must be between -90 and 90 degrees");
        }

        if (building.Longitude < -180 || building.Longitude > 180)
        {
            throw new BuildingException("Invalid longitude, must be between -180 and 180 degrees");
        }
    }

    private void CheckConstructorCompany(Building building)
    {
        if (string.IsNullOrEmpty(building.ConstructorCompany))
        {
            throw new BuildingException("Building constructor company cannot be empty");
        }else if(building.ConstructorCompany.Length > maximumCharactersForConstructorCompany)
        {
            throw new BuildingException("Building constructor company cannot be longer than 100 characters");
        }
    }

    private void CheckNotEmptyBuildingDirection(Building building)
    {
        if (string.IsNullOrEmpty(building.Street))
        {
            throw new BuildingException("Building street cannot be empty");
        }
    }

    private void CheckNotNegativeSharedExpenses(float sharedExpenses)
    {
        if (sharedExpenses < 0)
        {
            throw new BuildingException("Shared expenses cannot be negative");
        }
    }

    private static void CheckNotEmptyBuildingName(Building building)
    {
        if (string.IsNullOrEmpty(building.Name))
        {
            throw new BuildingException("Building name cannot be empty");
        }
    }

    private void CheckUniqueBuildingName(Building building)
    {
        if (_iBuildingRepository.GetAllBuildings().ToList().Exists(x => x.Name.ToLower() == building.Name.ToLower()))
        {
            throw new BuildingException("Building with same name already exists");
        }
    }

    public void DeleteBuilding(Guid guid)
    {
        Building building = _iBuildingRepository.GetBuildingById(guid);
        _iBuildingRepository.DeleteBuilding(building);
    }

    public IEnumerable<Building> GetAllBuildings(Guid managerId)
    {
        return _iBuildingRepository.GetAllBuildings().Where(x => x.Manager.Id.Equals(managerId));
    }

    public Building GetBuildingById(Guid id)
    {
        return _iBuildingRepository.GetBuildingById(id);
        
    }

    public Flat GetFlatByFlatId(Guid flatId)
    {
        return _iBuildingRepository.GetFlatByFlatId(flatId);
    }

    public Building UpdateBuilding(Guid buildingId, float sharedExpenses, List<Guid> maintenanceEmployeeIds)
    {
        CheckNotNegativeSharedExpenses(sharedExpenses);

        CheckNotRepetitiveMaintenanceEmployeeIds(maintenanceEmployeeIds);

        Building building = _iBuildingRepository.GetBuildingById(buildingId);

        building.SharedExpenses = sharedExpenses;

        List<User> maintenanceEmployees = BuildMaintenanceEmployeeList(building, maintenanceEmployeeIds);

        building.MaintenanceEmployees = maintenanceEmployees;

        return _iBuildingRepository.UpdateBuilding(building);
    }

    private void CheckNotRepetitiveMaintenanceEmployeeIds(List<Guid> maintenanceEmployeeIds)
    {
        if (maintenanceEmployeeIds.Distinct().Count() != maintenanceEmployeeIds.Count)
        {
            throw new BuildingException("Maintenance employees list contains repeated ids");
        }
    }

    private List<User> BuildMaintenanceEmployeeList(Building building, List<Guid> maintenanceEmployeeIds)
    {
        List<User> maintenanceEmployees = new List<User>();

        foreach (Guid id in maintenanceEmployeeIds)
        {
            User user = _iUserRepository.GetUserById(id);
            CheckUserIsMaintenanceEmployee(user);

            maintenanceEmployees.Add(user);
        }
        return maintenanceEmployees;
    }

    private static void CheckUserIsMaintenanceEmployee(User user)
    {
        if (user.Role != Role.MaintenanceEmployee)
        {
            throw new BuildingException("User in maintenance employees list is not a maintenance employee");
        }
    }

    public Flat UpdateFlat(Guid flatId, Flat flat)
    {
        ValidateFlat(flat);

        Flat existingFlat = _iBuildingRepository.GetFlatByFlatId(flatId);
        CheckUniqueFlatNumberInBuilding(existingFlat, flat);

        if (IsNewOwnerEmailForExistingOwner(flat.OwnerEmail, existingFlat.OwnerEmail))
        {
            UpdateExistingFlatsOwnerEmail(existingFlat.OwnerEmail, flat.OwnerEmail);
        }

        existingFlat.Number = flat.Number;
        existingFlat.Bathrooms = flat.Bathrooms;
        existingFlat.OwnerName = flat.OwnerName;
        existingFlat.OwnerSurname = flat.OwnerSurname;
        existingFlat.OwnerEmail = flat.OwnerEmail;
        existingFlat.HasBalcony = flat.HasBalcony;

        return _iBuildingRepository.UpdateFlat(existingFlat);
    }

    private static bool IsNewOwnerEmailForExistingOwner(string newEmail, string existingEmail)
    {
        return !string.IsNullOrEmpty(existingEmail) && existingEmail.ToLower() != newEmail.ToLower();
    }

    private void UpdateExistingFlatsOwnerEmail(string previousEmail, string newEmail)
    {
        List<Flat> flatsWithPreviousOwnerEmail = _iBuildingRepository.GetAllFlats().
            Where(flat => flat.OwnerEmail.ToLower() == previousEmail.ToLower()).ToList();

        foreach (Flat flat in flatsWithPreviousOwnerEmail)
        {
            flat.OwnerEmail = newEmail;
            _iBuildingRepository.UpdateFlat(flat);
        }
                
    }

    private void ValidateFlat(Flat flat)
    {
        CheckFlatDigit(flat);
        CheckNotEmptyFLatOwnerName(flat);
        CheckFlatOwnerEmail(flat.OwnerEmail);
        CheckNotEmptyFLatOwnerSurname(flat);
        CheckNotNegativeBathrooms(flat.Bathrooms);
    }

    private void CheckNotNegativeBathrooms(int bathrooms)
    {
        if (bathrooms <= 0)
        {
            throw new BuildingException("Number of bathrooms cannot be negative or zero");
        }
    }

    private void CheckNotEmptyFLatOwnerSurname(Flat flat)
    {
        if (string.IsNullOrEmpty(flat.OwnerSurname))
        {
            throw new BuildingException("Owner surname cannot be empty");
        }
    }

    private void CheckFlatOwnerEmail(string email)
    {
        CheckNotEmptyEmail(email);
        CheckEmailWithAt(email);
        CheckEmailWithDot(email);
    }

    private void CheckEmailWithDot(string email)
    {
        if (!email.Contains("."))
        {
            throw new BuildingException("Invalid email, must contain .");
        }
    }

    private void CheckEmailWithAt(string email)
    {
        if (!email.Contains("@"))
        {
            throw new BuildingException("Invalid email, must contain @");
        }
    }

    private static void CheckNotEmptyEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new BuildingException("Owner email cannot be empty");
        }
    }

    private static void CheckNotEmptyFLatOwnerName(Flat flat)
    {
        if (string.IsNullOrEmpty(flat.OwnerName))
        {
            throw new BuildingException("Owner name cannot be empty");
        }
    }

    private void CheckFlatDigit(Flat flat)
    {
        if (!flat.Number.ToString().StartsWith(flat.Floor.ToString()))
        {
            throw new BuildingException("Invalid flat number, first digit must be floor number");
        }
    }

    private void CheckUniqueFlatNumberInBuilding(Flat existingFlat, Flat flat)
    {
        if (GetAllBuildingFlats(existingFlat.Building.Id).ToList().Exists(x => x.Id != existingFlat.Id && x.Number == flat.Number))
        {
            throw new BuildingException("Flat with same number already exists");
        }
    }

    public IEnumerable<Flat> GetAllBuildingFlats(Guid buildingId)
    {
        return _iBuildingRepository.GetAllBuildingFlats(buildingId);
    }
}
