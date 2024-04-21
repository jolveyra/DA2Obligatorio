using RepositoryInterfaces;
using LogicInterfaces;
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
        List<Building> buildings = _iBuildingRepository.GetAllBuildings().ToList();

        if (buildings.FirstOrDefault(x => x.Name == building.Name) != null)
        {
            throw new ArgumentException("Building with same name already exists");
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
        CheckSharedExpenses(sharedExpenses);

        return _iBuildingRepository.UpdateBuilding(buildingId, sharedExpenses);

    }

    private static void CheckSharedExpenses(float sharedExpenses)
    {
        if (sharedExpenses < 0)
        {
            throw new ArgumentException("Shared expenses cannot be negative");
        }
    }

    public Flat UpdateFlat(Guid buildingId, Guid flatId, Flat flat)
    {
        return _iBuildingRepository.UpdateFlat(buildingId, flatId, flat);
    }
}
