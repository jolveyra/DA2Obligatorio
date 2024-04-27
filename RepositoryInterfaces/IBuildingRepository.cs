
using Domain;

namespace RepositoryInterfaces;

public interface IBuildingRepository
{
    public Building CreateBuilding(Building building);
    void DeleteBuilding(Building building);
    public IEnumerable<Building> GetAllBuildings();
    Building GetBuildingById(Guid buildingId);
    Flat GetFlatByBuildingAndFlatId(Guid buildingId, Guid flatId);
    Building UpdateBuilding(Building building);
    Flat UpdateFlat(Flat flat);
}
