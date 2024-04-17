using Microsoft.AspNetCore.Mvc;
using LogicInterfaces;
using WebModels;
using System.Security.Cryptography;
using ManagementApiTest;
using System.Reflection.Metadata;

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
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating the building");
            }
        }

        [HttpGet]
        public IActionResult GetAllBuildings()
        {
            return Ok(_iBuildingLogic.GetAllBuildings().Select(building => new BuildingResponseModel(building)).ToList());
        }

        [HttpGet("{buildingId}")]
        public IActionResult GetBuildingById([FromRoute] Guid buildingId)
        {
            try
            {
                return Ok(new BuildingResponseModel(_iBuildingLogic.GetBuildingById(buildingId)));
            }catch(ArgumentException)
            {
                return NotFound("There is no building with that specific id");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving the building");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBuildingById([FromRoute] Guid id, [FromBody] UpdateBuildingRequestModel updateBuildingRequest)
        {
            try
            {
                return Ok(new BuildingResponseModel(_iBuildingLogic.UpdateBuilding(id, updateBuildingRequest.SharedExpenses)));
            }
            catch(ArgumentException)
            {
                return NotFound("There is no building with that specific id");
            }catch(Exception)
            {
                return StatusCode(500, "An error occurred while updating the building");
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
            catch(Exception e)
            {
                return StatusCode(500, "An error occurred while deleting the building");
            }
        }

        [HttpPut("{buildingId}/{flatId}")]
        public IActionResult UpdateFlatByBuildingAndFlatId([FromRoute] Guid buildingId, [FromRoute] Guid flatId, [FromBody] UpdateFlatRequestModel updateFlatRequest)
        {
            return Ok(new FlatResponseModel(_iBuildingLogic.UpdateFlat(buildingId, flatId, updateFlatRequest.ToEntity())));
        }

        [HttpGet("{buildingId}/{flatId}")]
        public IActionResult GetFlatByBuildingAndFlatId([FromRoute] Guid buildingId, [FromRoute] Guid flatId)
        {
            return Ok(new FlatResponseModel(_iBuildingLogic.GetFlatByBuildingAndFlatId(buildingId, flatId)));
        }
    }
}
