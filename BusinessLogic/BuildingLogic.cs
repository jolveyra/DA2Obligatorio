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

    public Building CreateBuilding(Building building)
    {
        ValidateBuilding(building);

        CheckUniqueBuildingName(building);

        return _iBuildingRepository.CreateBuilding(building);
    }

    private void ValidateBuilding(Building building)
    {
        CheckNotEmptyBuildingName(building);
        CheckNotNegativeSharedExpenses(building);
        CheckNotEmptyBuildingDirection(building);
        CheckNotEmptyConstructorCompany(building);
    }

    private void CheckNotEmptyConstructorCompany(Building building)
    {
        if (building.ConstructorCompany == "")
        {
            throw new BuildingException("Building constructor company cannot be empty");
        }
    }

    private void CheckNotEmptyBuildingDirection(Building building)
    {
        if (building.Direction == "")
        {
            throw new BuildingException("Building direction cannot be empty");
        }
    }

    private static void CheckNotNegativeSharedExpenses(Building building)
    {
        if (building.SharedExpenses < 0)
        {
            throw new BuildingException("Shared expenses cannot be negative");
        }
    }

    private static void CheckNotEmptyBuildingName(Building building)
    {
        if (building.Name == "")
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
        _iBuildingRepository.DeleteBuilding(guid);
    }

    public IEnumerable<Building> GetAllBuildings()
    {
        return _iBuildingRepository.GetAllBuildings();
    }

    public Building GetBuildingById(Guid id)
    {
        return _iBuildingRepository.GetBuildingById(id);
        
    }

    public Flat GetFlatByBuildingAndFlatId(Guid buildingId, Guid flatId)
    {
        return _iBuildingRepository.GetFlatByBuildingAndFlatId(buildingId, flatId);
    }

    public Building UpdateBuilding(Guid buildingId, float sharedExpenses)
    {
        return _iBuildingRepository.UpdateBuilding(buildingId, sharedExpenses);
    }

    public Flat UpdateFlat(Guid buildingId, Guid flatId, Flat flat)
    {
        CheckFlatDigit(flat);

        CheckUniqueFlatNumberInBuilding(buildingId, flatId, flat);

        return _iBuildingRepository.UpdateFlat(buildingId, flatId, flat);
    }

    private void CheckFlatDigit(Flat flat)
    {
        if (!flat.Number.ToString().StartsWith(flat.Floor.ToString()))
        {
            throw new BuildingException("Invalid flat number, first digit must be floor number");
        }
    }

    private void CheckUniqueFlatNumberInBuilding(Guid buildingId, Guid flatId, Flat flat)
    {
        if (GetBuildingById(buildingId).Flats.Exists(x => x.Id != flatId && x.Number == flat.Number))
        {
            throw new BuildingException("Flat with same number already exists");
        }
    }
}
