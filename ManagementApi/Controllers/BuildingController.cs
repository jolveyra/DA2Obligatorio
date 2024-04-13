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

        [HttpPost]
        public IActionResult CreateBuilding([FromBody] BuildingRequestModel buildingRequest)
        {
            BuildingResponseModel response = new BuildingResponseModel(_iBuildingLogic.CreateBuilding(buildingRequest.ToEntity()));

            return CreatedAtAction("CreateBuilding", new { Id = response.Id }, response);
        }

        [HttpGet]
        public IActionResult GetAllBuildings()
        {
            return Ok(_iBuildingLogic.GetAllBuildings().Select(building => new BuildingResponseModel(building)).ToList());
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBuildingById([FromRoute] Guid id, [FromBody] UpdateBuildingRequestModel updateBuildingRequest)
        {
            return Ok(new BuildingResponseModel(_iBuildingLogic.UpdateBuilding(id, updateBuildingRequest.SharedExpenses)));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBuilding([FromRoute] Guid id)
        {
            _iBuildingLogic.DeleteBuilding(id);
            return Ok();
        }
    }
}
