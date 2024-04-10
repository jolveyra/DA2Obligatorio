using Domain;

namespace LogicInterfaces
{
    public interface IBuildingLogic
    {
        Building CreateBuilding(Building building);
        IEnumerable<Building> GetAllBuildings();
    }
}