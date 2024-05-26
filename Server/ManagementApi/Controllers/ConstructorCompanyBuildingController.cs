using Microsoft.AspNetCore.Mvc;
using LogicInterfaces;
using WebModels.BuildingModels;
using ManagementApi.Filters;


namespace ManagementApi.Controllers
{
    [Route("api/v2/constructorCompanyBuildings")]
    [ApiController]
    [ExceptionFilter]
    public class ConstructorCompanyBuildingController : ControllerBase
    {
        private IConstructorCompanyBuildingLogic _iConstructorCompanyBuildingLogic;

        public ConstructorCompanyBuildingController(IConstructorCompanyBuildingLogic iConstructorCompanyBuildingLogic)
        {
            _iConstructorCompanyBuildingLogic = iConstructorCompanyBuildingLogic;
        }

        [HttpPost]
        [AuthenticationFilter(["ConstructorCompanyAdmin"])]
        public OkObjectResult CreateConstructorCompanyBuilding(BuildingRequestModel buildingRequestModel)
        {
            return Ok(new BuildingResponseModel(_iConstructorCompanyBuildingLogic.CreateConstructorCompanyBuilding(buildingRequestModel.ToEntity(),
                Guid.Parse(HttpContext.Items["UserId"] as string))));
        }

        [HttpGet]
        [AuthenticationFilter(["ConstructorCompanyAdmin"])]
        public OkObjectResult GetAllConstructorCompanyBuildings()
        {
            return Ok(_iConstructorCompanyBuildingLogic.GetAllConstructorCompanyBuildings(
                Guid.Parse(HttpContext.Items["UserId"] as string))
                .Select(constructorCompanyBuilding => new BuildingResponseModel(constructorCompanyBuilding)).ToList());
        }

        [HttpGet]
        [AuthenticationFilter(["ConstructorCompanyAdmin"])]
        public OkObjectResult GetConstructorCompanyBuildingById(Guid buildingId)
        {
            return Ok(new BuildingResponseModel(_iConstructorCompanyBuildingLogic.GetConstructorCompanyBuildingById(buildingId, Guid.Parse(HttpContext.Items["UserId"] as string))));
        }
    }
}
