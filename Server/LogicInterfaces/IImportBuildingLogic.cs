using Domain;

namespace LogicInterfaces
{
    public interface IImportBuildingLogic
    {
        List<Building> ImportBuildings(string dllName, string fileName, Guid userId);
    }
}
