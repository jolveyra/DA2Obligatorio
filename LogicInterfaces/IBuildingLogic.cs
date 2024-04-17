using Domain;

namespace LogicInterfaces
{
    public interface IBuildingLogic
    {
        Building CreateBuilding(Building building);
        void DeleteBuilding(Guid guid);
        IEnumerable<Building> GetAllBuildings();
        Building GetBuildingById(Guid id);
        Flat GetFlatByBuildingAndFlatId(Guid buildingId, Guid flatId);
        Building UpdateBuilding(Guid guid, float v);
        Flat UpdateFlat(Guid buildingId, Guid flatId, Flat flat);
    }
}