using Microsoft.AspNetCore.Mvc;
using LogicInterfaces;
using WebModels;
using System.Security.Cryptography;
using ManagementApiTest;

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
            try
            {
                BuildingResponseModel response = new BuildingResponseModel(_iBuildingLogic.CreateBuilding(buildingRequest.ToEntity()));

                return CreatedAtAction("CreateBuilding", new { Id = response.Id }, response);
            }
            catch (ArgumentException)
            {
                BadRequestObjectResult badRequestResult = new BadRequestObjectResult("Building already exists.");
                return badRequestResult;
            }
            catch(Exception e)
            {
                StatusCodeResult statusCodeResult = new StatusCodeResult(StatusCodes.Status500InternalServerError);

                return statusCodeResult;
            }
        }

        [HttpGet]
        public IActionResult GetAllBuildings()
        {
            return Ok(_iBuildingLogic.GetAllBuildings().Select(building => new BuildingResponseModel(building)).ToList());
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBuildingById([FromRoute] Guid id, [FromBody] UpdateBuildingRequestModel updateBuildingRequest)
        {
            try
            {
                return Ok(new BuildingResponseModel(_iBuildingLogic.UpdateBuilding(id, updateBuildingRequest.SharedExpenses)));
            }
            catch(ArgumentException e)
            {
                return NotFound("There is no building with that specific id.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBuilding([FromRoute] Guid id)
        {
            try
            {
                _iBuildingLogic.DeleteBuilding(id);
                return Ok();
            }
            catch(ArgumentException e)
            {
                return NotFound("There is no building with that specific id.");
            }
        }

        [HttpPut("{buildingId}/{flatId}")]
        public IActionResult UpdateFlatByBuildingAndFlatId([FromRoute] Guid buildingId, [FromRoute] Guid flatId, [FromBody] UpdateFlatRequestModel updateFlatRequest)
        {
            return Ok(new FlatResponseModel(_iBuildingLogic.UpdateFlat(buildingId, flatId, updateFlatRequest.ToEntity())));
        }
    }
}
