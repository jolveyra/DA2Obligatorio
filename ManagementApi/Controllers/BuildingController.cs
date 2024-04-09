using Microsoft.AspNetCore.Mvc;
using LogicInterfaces;
using WebModels;

namespace ManagementApi.Controllers
{
    [Route("api/v1/buildings")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private IBuildingLogic _iBuildingLogic;

        public BuildingController(IBuildingLogic iBuildingLogic)
        {
            this._iBuildingLogic = iBuildingLogic;
        }

        [HttpGet]
        public IActionResult GetAllBuildings()
        {
            return Ok(_iBuildingLogic.GetAllBuildings().Select(building => new BuildingResponseModel(building)).ToList());
        }
    }
}
