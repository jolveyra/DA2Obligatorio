using Domain;

namespace LogicInterfaces
{
    public interface IBuildingLogic
    {
        Building CreateBuilding(Building building);
        void DeleteBuilding(Guid guid);
        IEnumerable<Building> GetAllBuildings();
        Building UpdateBuilding(Guid guid, float v);
    }
}