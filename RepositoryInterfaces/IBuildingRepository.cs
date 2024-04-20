
using Domain;

namespace RepositoryInterfaces;

public interface IBuildingRepository
{
    public Building CreateBuilding(Building building);
    void DeleteBuilding(Guid guid);
    public IEnumerable<Building> GetAllBuildings();
    Building GetBuildingById(Guid guid);
    Building UpdateBuilding(Guid guid, float v);
}
