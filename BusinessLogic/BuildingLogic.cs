using RepositoryInterfaces;
using LogicInterfaces;
using CustomExceptions.BusinessLogic;
using Domain;

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
        CheckUniqueBuildingName(building);

        return _iBuildingRepository.CreateBuilding(building);
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
        CheckUniqueFlatNumberInBuilding(buildingId, flatId, flat);

        return _iBuildingRepository.UpdateFlat(buildingId, flatId, flat);
    }

    private void CheckUniqueFlatNumberInBuilding(Guid buildingId, Guid flatId, Flat flat)
    {
        if (GetBuildingById(buildingId).Flats.Exists(x => x.Id != flatId && x.Number == flat.Number))
        {
            throw new BuildingException("Flat with same number already exists");
        }
    }
}
