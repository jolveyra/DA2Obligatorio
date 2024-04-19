using Microsoft.AspNetCore.Mvc;
using LogicInterfaces;
using WebModels.BuildingModels;
using ManagementApi.Filters;
using System.Security.Cryptography;
using System.Reflection.Metadata;

namespace ManagementApi.Controllers
{
    [Route("api/v1/buildings")]
    [ApiController]
    [ExceptionFilter]
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

        [HttpGet("{buildingId}")]
        public IActionResult GetBuildingById([FromRoute] Guid buildingId)
        {
            
            return Ok(new BuildingResponseModel(_iBuildingLogic.GetBuildingById(buildingId)));
            
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
