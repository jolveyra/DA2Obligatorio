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
        Building UpdateBuilding(Guid guid, Building building);
        Flat UpdateFlat(Guid flatId, Flat flat, bool changeOwner);
    }
}