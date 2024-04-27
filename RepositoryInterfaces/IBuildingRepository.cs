
using Domain;

namespace RepositoryInterfaces;

public interface IBuildingRepository
{
    public Building CreateBuilding(Building building);
    void DeleteBuilding(Building building);
    public IEnumerable<Building> GetAllBuildings();
    Building GetBuildingById(Guid guid);
    Flat GetFlatByBuildingAndFlatId(Guid guid1, Guid guid2);
    Building UpdateBuilding(Building building);
    Flat UpdateFlat(Guid guid1, Guid guid2, Flat flat);
}
