using Domain;

namespace LogicInterfaces
{
    public interface IBuildingLogic
    {
        IEnumerable<Building> GetAllBuildings();
    }
}