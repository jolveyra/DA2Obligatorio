using RepositoryInterfaces;
using LogicInterfaces;
using CustomExceptions;
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

        CheckUniqueBuildingName(building);
        CheckUniqueBuildingAddress(building);
        CheckUniqueBuildingCoordinates(building);
        
        building.Manager = _iUserRepository.GetUserById(userId);
        Building toReturn = _iBuildingRepository.CreateBuilding(building);

        CreateBuildingFlats(toReturn, amountOfFlats);

        return toReturn;
    }

    private void CheckUniqueBuildingCoordinates(Building building)
    {
        if(_iBuildingRepository.GetAllBuildings().ToList().Exists(x => x.Address.Latitude == building.Address.Latitude && x.Address.Longitude == building.Address.Longitude))
        {
            throw new BuildingException("Building with same coordinates already exists");
        }
    }

    private void CheckUniqueBuildingAddress(Building building)
    {
        if(_iBuildingRepository.GetAllBuildings().ToList().Exists(x => x.Address.Equals(building.Address)))
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
        if (building.Address.Latitude < -90 || building.Address.Latitude > 90)
        {
            throw new BuildingException("Invalid latitude, must be between -90 and 90 degrees");
        }

        if (building.Address.Longitude < -180 || building.Address.Longitude > 180)
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
        if (string.IsNullOrEmpty(building.Address.Street))
        {
            throw new BuildingException("Building's street cannot be empty");
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
        DeleteBuildingFlats(building);
        _iBuildingRepository.DeleteBuilding(building);
    }

    private void DeleteBuildingFlats(Building building)
    {
        IEnumerable<Flat> flats = _iBuildingRepository.GetAllBuildingFlats(building.Id);
        foreach (Flat flat in flats)
        {
            _iBuildingRepository.DeleteFlat(flat);
        }
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

    public Building UpdateBuilding(Guid buildingId, Building buildingData, List<Guid> maintenanceEmployeeIds)
    {
        CheckNotNegativeSharedExpenses(buildingData.SharedExpenses);

        CheckNotRepetitiveMaintenanceEmployeeIds(maintenanceEmployeeIds);

        Building building = _iBuildingRepository.GetBuildingById(buildingId);

        building.SharedExpenses = buildingData.SharedExpenses;
        building.ConstructorCompany = buildingData.ConstructorCompany;

        CheckMaintenanceEmployeeList(building, maintenanceEmployeeIds);

        building.MaintenanceEmployees = maintenanceEmployeeIds;

        return _iBuildingRepository.UpdateBuilding(building);
    }

    private void CheckNotRepetitiveMaintenanceEmployeeIds(List<Guid> maintenanceEmployeeIds)
    {
        if (maintenanceEmployeeIds.Distinct().Count() != maintenanceEmployeeIds.Count)
        {
            throw new BuildingException("Maintenance employees list contains repeated ids");
        }
    }

    private void CheckMaintenanceEmployeeList(Building building, List<Guid> maintenanceEmployeeIds)
    {
        foreach (Guid id in maintenanceEmployeeIds)
        {
            User user = _iUserRepository.GetUserById(id);
            CheckUserIsMaintenanceEmployee(user);
        }
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

        if (OwnerInfoChanges(flat, existingFlat))
        {
            UpdateExistingFlatsOwnerInfo(existingFlat, flat);
        }

        existingFlat.Number = flat.Number;
        existingFlat.Bathrooms = flat.Bathrooms;
        existingFlat.OwnerName = flat.OwnerName;
        existingFlat.OwnerSurname = flat.OwnerSurname;
        existingFlat.OwnerEmail = flat.OwnerEmail;
        existingFlat.HasBalcony = flat.HasBalcony;

        return _iBuildingRepository.UpdateFlat(existingFlat);
    }

    private bool OwnerInfoChanges(Flat flat, Flat existingFlat)
    {
        return IsNewOwnerEmailForExistingOwner(flat.OwnerEmail, existingFlat.OwnerEmail) || 
            IsNewOwnerNameForExistingOwner(flat.OwnerName, existingFlat.OwnerName) || 
            IsNewOwnerSurnameForExistingOwner(flat.OwnerSurname, existingFlat.OwnerSurname);
    }

    private bool IsNewOwnerSurnameForExistingOwner(string newSurname, string existingSurname)
    {
        return !string.IsNullOrEmpty(existingSurname) && existingSurname.ToLower() != newSurname.ToLower();
    }

    private bool IsNewOwnerNameForExistingOwner(string newName, string existingName)
    {
        return !string.IsNullOrEmpty(existingName) && existingName.ToLower() != newName.ToLower();
    }

    private static bool IsNewOwnerEmailForExistingOwner(string newEmail, string existingEmail)
    {
        return !string.IsNullOrEmpty(existingEmail) && existingEmail.ToLower() != newEmail.ToLower();
    }

    private void UpdateExistingFlatsOwnerInfo(Flat existingFlat, Flat newFlat)
    {
        List<Flat> flatsWithPreviousOwnerEmail = _iBuildingRepository.GetAllFlats().
            Where(flat => flat.OwnerEmail.ToLower() == existingFlat.OwnerEmail.ToLower()).ToList();

        foreach (Flat flat in flatsWithPreviousOwnerEmail)
        {
            flat.OwnerEmail = newFlat.OwnerEmail;
            flat.OwnerName = newFlat.OwnerName;
            flat.OwnerSurname = newFlat.OwnerSurname;
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
