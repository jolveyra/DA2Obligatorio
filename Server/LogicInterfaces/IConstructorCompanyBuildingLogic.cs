using Domain;

namespace ManagementApi.Controllers
{
    public interface IConstructorCompanyBuildingLogic
    {
        Building CreateConstructorCompanyBuilding(Building building, Guid userId);
        IEnumerable<Building> GetAllConstructorCompanyBuildings(Guid userId);
    }
}