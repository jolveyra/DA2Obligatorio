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
        try
        {
            return _iBuildingRepository.CreateBuilding(building);
        }
        catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message);
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

    public Building UpdateBuilding(Guid guid, float v)
    {
        return _iBuildingRepository.UpdateBuilding(guid, v);
    }

    public Flat UpdateFlat(Guid buildingId, Guid flatId, Flat flat)
    {
        return _iBuildingRepository.UpdateFlat(buildingId, flatId, flat);
    }
}
