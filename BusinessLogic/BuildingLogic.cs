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
        return _iBuildingRepository.CreateBuilding(building);
    }

    public void DeleteBuilding(Guid guid)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    public Building UpdateBuilding(Guid guid, float v)
    {
        throw new NotImplementedException();
    }

    public Flat UpdateFlat(Guid buildingId, Guid flatId, Flat flat)
    {
        throw new NotImplementedException();
    }
}
