using Domain;

namespace LogicInterfaces
{
    public interface IBuildingLogic
    {
        IEnumerable<Flat> GetAllBuildingFlats(Guid id);
        IEnumerable<Building> GetAllBuildings(Guid userId);
        Building GetBuildingById(Guid id);
        Flat GetFlatByBuildingAndFlatId(Guid buildingId, Guid flatId);
        Building UpdateBuilding(Guid guid, Building building);
        Flat UpdateFlat(Guid buildingId, Guid flatId, Flat flat, bool changeOwner);
    }
}