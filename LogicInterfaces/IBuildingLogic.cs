using Domain;

namespace LogicInterfaces
{
    public interface IBuildingLogic
    {
        Building CreateBuilding(Building building, int amountOfFlats, Guid userId);
        void DeleteBuilding(Guid guid);
        IEnumerable<Flat> GetAllBuildingFlats(Guid id);
        IEnumerable<Building> GetAllBuildings();
        Building GetBuildingById(Guid id);
        Flat GetFlatByFlatId(Guid flatId);
        Building UpdateBuilding(Guid guid, float v);
        Flat UpdateFlat(Guid flatId, Flat flat);
    }
}