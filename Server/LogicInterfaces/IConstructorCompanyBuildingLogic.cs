using Domain;

namespace LogicInterfaces
{
    public interface IConstructorCompanyBuildingLogic
    {
        Building CreateConstructorCompanyBuilding(Building building, int amountOfFlats, Guid userId);
        void DeleteConstructorCompanyBuilding(Guid guid1, Guid guid2);
        IEnumerable<Building> GetAllConstructorCompanyBuildings(Guid userId);
        Building GetConstructorCompanyBuildingById(Guid buildingId, Guid userId);
        Building UpdateConstructorCompanyBuilding(Building building, Guid buildingId, Guid userId);
    }
}