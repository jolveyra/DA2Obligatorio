using Microsoft.AspNetCore.Mvc;
using LogicInterfaces;
using WebModels.BuildingModels;
using ManagementApi.Filters;
using WebModels.ConstructorCompanyBuildingModels;


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
        public IActionResult CreateConstructorCompanyBuilding(BuildingRequestModel buildingRequestModel)
        {
            int amountOfFlats = buildingRequestModel.Flats;
            return Ok(new BuildingResponseModel(_iConstructorCompanyBuildingLogic.CreateConstructorCompanyBuilding(buildingRequestModel.ToEntity(), amountOfFlats,
                Guid.Parse(HttpContext.Items["UserId"] as string))));
        }

        [HttpDelete("{buildingId}")]
        [AuthenticationFilter(["ConstructorCompanyAdmin"])]
        public IActionResult DeleteConstructorCompanyBuilding([FromRoute] Guid buildingId)
        {
            _iConstructorCompanyBuildingLogic.DeleteConstructorCompanyBuilding(buildingId, Guid.Parse(HttpContext.Items["UserId"] as string));
            return Ok();
        }

        [HttpGet]
        [AuthenticationFilter(["ConstructorCompanyAdmin"])]
        public IActionResult GetAllConstructorCompanyBuildings()
        {
            return Ok(_iConstructorCompanyBuildingLogic.GetAllConstructorCompanyBuildings(
                Guid.Parse(HttpContext.Items["UserId"] as string))
                .Select(constructorCompanyBuilding => new BuildingResponseModel(constructorCompanyBuilding)).ToList());
        }

        [HttpGet("{buildingId}")]
        [AuthenticationFilter(["ConstructorCompanyAdmin"])]
        public IActionResult GetConstructorCompanyBuildingById([FromRoute] Guid buildingId)
        {
            return Ok(new BuildingResponseModel(_iConstructorCompanyBuildingLogic.GetConstructorCompanyBuildingById(buildingId, Guid.Parse(HttpContext.Items["UserId"] as string))));
        }

        [HttpPut("{buildingId}")]
        
        public IActionResult UpdateConstructorCompanyBuilding([FromRoute] Guid buildingId, UpdateConstructorCompanyBuildingRequestModel request)
        {
            return Ok(new BuildingResponseModel(_iConstructorCompanyBuildingLogic.UpdateConstructorCompanyBuilding(request.ToEntity(), buildingId,
                Guid.Parse(HttpContext.Items["UserId"] as string))));
        }
    }
}
