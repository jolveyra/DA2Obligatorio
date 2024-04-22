
using Domain;

namespace RepositoryInterfaces;

public interface IBuildingRepository
{
    public Building CreateBuilding(Building building);
    void DeleteBuilding(Guid guid);
    public IEnumerable<Building> GetAllBuildings();
    Building GetBuildingById(Guid guid);
    Flat GetFlatByBuildingAndFlatId(Guid guid1, Guid guid2);
    Building UpdateBuilding(Guid guid, float v);
    Flat UpdateFlat(Guid guid1, Guid guid2, Flat flat);
}
