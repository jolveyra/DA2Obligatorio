using Domain;

namespace ManagementApi.Controllers
{
    public interface IConstructorCompanyBuildingLogic
    {
        IEnumerable<Building> GetAllConstructorCompanyBuildings();
    }
}