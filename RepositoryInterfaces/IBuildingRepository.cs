
using Domain;

namespace RepositoryInterfaces;

public interface IBuildingRepository
{
    public Building CreateBuilding(Building building);
    Flat CreateFlat(Flat flat);
    void DeleteBuilding(Building building);
    IEnumerable<Flat> GetAllBuildingFlats(Guid buildingId);
    public IEnumerable<Building> GetAllBuildings();
    Building GetBuildingById(Guid buildingId);
    Flat GetFlatByFlatId(Guid flatId);
    Building UpdateBuilding(Building building);
    Flat UpdateFlat(Flat flat);
}
