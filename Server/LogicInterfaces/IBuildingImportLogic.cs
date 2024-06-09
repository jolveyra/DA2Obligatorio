using Domain;

namespace LogicInterfaces
{
    public interface IBuildingImportLogic
    {
        List<Building> ImportBuildings(string dllName, string fileName, Guid userId);
    }
}
