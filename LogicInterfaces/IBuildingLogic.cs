using Domain;

namespace LogicInterfaces
{
    public interface IBuildingLogic
    {
        Building CreateBuilding(Building building, int amountOfFlats, Guid userId);
        void DeleteBuilding(Guid guid);
        IEnumerable<Flat> GetAllBuildingFlats(Guid id);
        IEnumerable<Building> GetAllBuildings(Guid userId);
        Building GetBuildingById(Guid id);
        Flat GetFlatByFlatId(Guid flatId);
        Building UpdateBuilding(Guid guid, float v, List<Guid> maintenanceEmployeeIds);
        Flat UpdateFlat(Guid flatId, Flat flat);
    }
}