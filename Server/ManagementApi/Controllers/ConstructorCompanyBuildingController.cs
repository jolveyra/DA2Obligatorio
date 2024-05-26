using Microsoft.AspNetCore.Mvc;
using LogicInterfaces;
using WebModels.BuildingModels;

namespace ManagementApi.Controllers
{
    public class ConstructorCompanyBuildingController : ControllerBase
    {
        private IConstructorCompanyBuildingLogic _iConstructorCompanyBuildingLogic;

        public ConstructorCompanyBuildingController(IConstructorCompanyBuildingLogic iConstructorCompanyBuildingLogic)
        {
            _iConstructorCompanyBuildingLogic = iConstructorCompanyBuildingLogic;
        }

        public OkObjectResult GetAllConstructorCompanyBuildings()
        {
            return Ok(_iConstructorCompanyBuildingLogic.GetAllConstructorCompanyBuildings().Select(constructorCompanyBuilding => new BuildingResponseModel(constructorCompanyBuilding)).ToList());
        }
    }
}
