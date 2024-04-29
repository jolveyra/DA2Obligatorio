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
    private IBuildingRepository _iBuildingRepository;

    public BuildingLogic(IBuildingRepository iBuildingRepository)
    {
        this._iBuildingRepository = iBuildingRepository;
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
        if(GetAllBuildings().ToList().Exists(x => x.Latitude == building.Latitude && x.Longitude == building.Longitude))
        {
            throw new BuildingException("Building with same coordinates already exists");
        }
    }

    private void CheckUniqueBuildingAddress(Building building)
    {
        if(GetAllBuildings().ToList().Exists(x => x.Street == building.Street && x.DoorNumber == building.DoorNumber))
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
        CheckNotEmptyConstructorCompany(building);
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

    private void CheckNotEmptyConstructorCompany(Building building)
    {
        if (string.IsNullOrEmpty(building.ConstructorCompany))
        {
            throw new BuildingException("Building constructor company cannot be empty");
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
        if (GetAllBuildings().ToList().Exists(x => x.Name == building.Name))
        {
            throw new BuildingException("Building with same name already exists");
        }
    }

    public void DeleteBuilding(Guid guid)
    {
        Building building = _iBuildingRepository.GetBuildingById(guid);
        _iBuildingRepository.DeleteBuilding(building);
    }

    public IEnumerable<Building> GetAllBuildings()
    {
        return _iBuildingRepository.GetAllBuildings();
    }

    public Building GetBuildingById(Guid id)
    {
        return _iBuildingRepository.GetBuildingById(id);
        
    }

    public Flat GetFlatByFlatId(Guid flatId)
    {
        return _iBuildingRepository.GetFlatByFlatId(flatId);
    }

    public Building UpdateBuilding(Guid buildingId, float sharedExpenses)
    {
        CheckNotNegativeSharedExpenses(sharedExpenses);

        Building building = _iBuildingRepository.GetBuildingById(buildingId);

        building.SharedExpenses = sharedExpenses;

        return _iBuildingRepository.UpdateBuilding(building);
    }

    public Flat UpdateFlat(Guid flatId, Flat flat)
    {
        ValidateFlat(flat);

        Flat existingFlat = _iBuildingRepository.GetFlatByFlatId(flatId);
        CheckUniqueFlatNumberInBuilding(existingFlat, flat);


        existingFlat.Number = flat.Number;
        existingFlat.Bathrooms = flat.Bathrooms;
        existingFlat.OwnerName = flat.OwnerName;
        existingFlat.OwnerSurname = flat.OwnerSurname;
        existingFlat.OwnerEmail = flat.OwnerEmail;
        existingFlat.HasBalcony = flat.HasBalcony;

        return _iBuildingRepository.UpdateFlat(existingFlat);
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
