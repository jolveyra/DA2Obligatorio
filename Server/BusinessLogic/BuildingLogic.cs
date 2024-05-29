using RepositoryInterfaces;
using LogicInterfaces;
using CustomExceptions;
using Domain;

namespace BusinessLogic;

public class BuildingLogic : IBuildingLogic
{
    private const int maximumCharactersForConstructorCompany = 100;
    private IBuildingRepository _iBuildingRepository;
    private IUserRepository _iUserRepository;
    private IPeopleRepository _iPeopleRepository;

    public BuildingLogic(IBuildingRepository iBuildingRepository, IUserRepository iUserRepository, IPeopleRepository iPeopleRepository)
    {
        _iBuildingRepository = iBuildingRepository;
        _iUserRepository = iUserRepository;
        _iPeopleRepository = iPeopleRepository;
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
        if (_iBuildingRepository.GetAllBuildings().ToList().Exists(x => x.Address.Latitude == building.Address.Latitude && x.Address.Longitude == building.Address.Longitude))
        {
            throw new BuildingException("Building with same coordinates already exists");
        }
    }

    private void CheckUniqueBuildingAddress(Building building)
    {
        if (_iBuildingRepository.GetAllBuildings().ToList().Exists(x => x.Address.Equals(building.Address)))
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
        }
        else if (building.ConstructorCompany.Length > maximumCharactersForConstructorCompany)
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
        Building building = _iBuildingRepository.GetAllBuildings().ToList().FirstOrDefault(b => b.Id == guid);

        if(building is null)
        {
            throw new DeleteException();
        }

        DeleteFlatsFromBuilding(building);
        _iBuildingRepository.DeleteBuilding(building);
    }

    private void DeleteFlatsFromBuilding(Building building)
    {
        List<Flat> flats = _iBuildingRepository.GetAllBuildingFlats(building.Id).ToList();

        List<Guid> ownersToDelete = new List<Guid>();
        GetOwnersToDeleteIds(flats, ownersToDelete);

        _iBuildingRepository.DeleteFlats(flats);
        DeleteOwners(ownersToDelete);
    }

    private static void GetOwnersToDeleteIds(List<Flat> flats, List<Guid> ownersToDelete)
    {
        foreach (Flat flat in flats)
        {
            if (string.IsNullOrEmpty(flat.Owner.Email))
            {
                ownersToDelete.Add(flat.OwnerId);
            }
        }
    }

    private void DeleteOwners(List<Guid> ids)
    {
        foreach (Guid id in ids)
        {
            _iPeopleRepository.DeletePerson(id);
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

    public Flat GetFlatByBuildingAndFlatId(Guid buildingId, Guid flatId)
    {

        Flat existingFlat = _iBuildingRepository.GetAllBuildingFlats(buildingId).FirstOrDefault(f => f.Id == flatId);

        if (existingFlat is null)
        {
            throw new BuildingException("Flat not found in building");
        }

        return existingFlat;
    }

    public Building UpdateBuilding(Guid buildingId, Building buildingData)
    {
        CheckNotNegativeSharedExpenses(buildingData.SharedExpenses);

        CheckNotRepetitiveMaintenanceEmployeeIds(buildingData.MaintenanceEmployees);

        Building building = _iBuildingRepository.GetBuildingById(buildingId);

        building.SharedExpenses = buildingData.SharedExpenses;
        building.ConstructorCompany = buildingData.ConstructorCompany;

        CheckMaintenanceEmployeeList(building, buildingData.MaintenanceEmployees);

        building.MaintenanceEmployees = buildingData.MaintenanceEmployees;

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

    public Flat UpdateFlat(Guid buildingId, Guid flatId, Flat flat, bool changeOwner)
    {
        ValidateFlat(flat);
        ValidateOwner(flat.Owner);
        Flat existingFlat = GetFlatFromGivenBuilding(buildingId, flatId);

        CheckUniqueFlatNumberInBuilding(existingFlat, flat);

        OverrideFlatInfo(flat, existingFlat);
        ChangeOwnerOrUpdateOwnerInfo(flat, changeOwner, existingFlat);

        return _iBuildingRepository.UpdateFlat(existingFlat);
    }

    private void ChangeOwnerOrUpdateOwnerInfo(Flat flat, bool changeOwner, Flat existingFlat)
    {
        if (changeOwner)
        {
            ChangeOwner(flat, existingFlat);
        }
        else
        {
            UpdateOwnerInfo(flat, existingFlat);
        }
    }

    private Flat GetFlatFromGivenBuilding(Guid buildingId, Guid flatId)
    {
        Flat existingFlat = _iBuildingRepository.GetAllBuildingFlats(buildingId).FirstOrDefault(f => f.Id == flatId);

        if (existingFlat is null)
        {
            throw new BuildingException("Flat not found in building");
        }

        return existingFlat;
    }

    private void UpdateOwnerInfo(Flat flat, Flat existingFlat)
    {
        if (_iPeopleRepository.GetPeople().Any(p => !string.IsNullOrEmpty(p.Email) && p.Email.Equals(flat.Owner.Email) && p.Id != existingFlat.OwnerId))
        {
            throw new BuildingException("Another owner with same email already exists");
        }
        existingFlat.Owner.Name = flat.Owner.Name;
        existingFlat.Owner.Surname = flat.Owner.Surname;
        existingFlat.Owner.Email = flat.Owner.Email;
        _iPeopleRepository.UpdatePerson(existingFlat.Owner);
    }

    private void ChangeOwner(Flat flat, Flat existingFlat)
    {
        Person? newOwner = null;

        if (string.IsNullOrEmpty(existingFlat.Owner.Email))
        {
            _iPeopleRepository.DeletePerson(existingFlat.Id);
        }
        newOwner = _iPeopleRepository.GetPeople().FirstOrDefault(p => p.Email.Equals(flat.Owner.Email));

        existingFlat.Owner = newOwner ?? _iPeopleRepository.CreatePerson(flat.Owner);
    }

    private void OverrideFlatInfo(Flat flat, Flat existingFlat)
    {
        existingFlat.Number = flat.Number;
        existingFlat.Bathrooms = flat.Bathrooms;
        existingFlat.HasBalcony = flat.HasBalcony;
        existingFlat.Rooms = flat.Rooms;
        existingFlat.Floor = flat.Floor;
    }

    private void ValidateFlat(Flat flat)
    {
        CheckFlatDigit(flat);
        CheckNotNegativeBathrooms(flat.Bathrooms);
        CheckNotNegativeRooms(flat.Rooms);
    }

    private void ValidateOwner(Person owner)
    {
        CheckNotEmptyFLatOwnerName(owner.Name);
        CheckFlatOwnerEmail(owner.Email);
        CheckNotEmptyFLatOwnerSurname(owner.Surname);
    }

    private void CheckNotNegativeRooms(int rooms)
    {
        if (rooms <= 0)
        {
            throw new BuildingException("Number of rooms cannot be negative or zero");
        }
    }

    private void CheckNotNegativeBathrooms(int bathrooms)
    {
        if (bathrooms <= 0)
        {
            throw new BuildingException("Number of bathrooms cannot be negative or zero");
        }
    }

    private void CheckNotEmptyFLatOwnerSurname(string surname)
    {
        if (string.IsNullOrEmpty(surname))
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

    private static void CheckNotEmptyFLatOwnerName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new BuildingException("Owner name cannot be empty");
        }
    }

    private void CheckFlatDigit(Flat flat)
    {
        if (!flat.Number.ToString().StartsWith(flat.Floor.ToString()))
        {
            throw new BuildingException("Invalid flat number, first digit must be same as in floor number");
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
