
using Domain;

namespace RepositoryInterfaces;

public interface IBuildingRepository
{
    public Building CreateBuilding(Building building);
    public IEnumerable<Building> GetAllBuildings();
    Building GetBuildingById(Guid guid);
}
