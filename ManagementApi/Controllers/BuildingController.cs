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
        [AuthenticationFilter(["Manager"])]
        public IActionResult CreateBuilding([FromBody] BuildingRequestModel buildingRequest)
        {

            BuildingResponseModel response = new BuildingResponseModel(_iBuildingLogic.CreateBuilding(buildingRequest.ToEntity(), 
                buildingRequest.Flats, 
                Guid.Parse(HttpContext.Items["UserId"] as string)));

            response.Flats = _iBuildingLogic.GetAllBuildingFlats(response.Id).Select(flat => new FlatResponseModel(flat)).ToList();

            return CreatedAtAction("CreateBuilding", new { Id = response.Id }, response);
        }

        [HttpGet]
        [AuthenticationFilter(["Manager"])]
        public IActionResult GetAllBuildings()
        {
            return Ok(_iBuildingLogic.GetAllBuildings(Guid.Parse(HttpContext.Items["UserId"] as string)).
                Select(building => new BuildingResponseModel(building)).ToList());
        
        }

        [HttpGet("{buildingId}")]
        [AuthenticationFilter(["Manager"])]
        public IActionResult GetBuildingById([FromRoute] Guid buildingId)
        {
            
            return Ok(new BuildingResponseModel(_iBuildingLogic.GetBuildingById(buildingId)));
            
        }

        [HttpPut("{id}")]
        [AuthenticationFilter(["Manager"])]
        public IActionResult UpdateBuildingById([FromRoute] Guid id, [FromBody] UpdateBuildingRequestModel updateBuildingRequest)
        {
            return Ok(new BuildingResponseModel(_iBuildingLogic.UpdateBuilding(id, updateBuildingRequest.ToEntity(), updateBuildingRequest.MaintenanceEmployees)));
            
        }

        [HttpDelete("{id}")]
        [AuthenticationFilter(["Manager"])]
        public IActionResult DeleteBuilding([FromRoute] Guid id)
        {
            _iBuildingLogic.DeleteBuilding(id);
            return Ok();
         
        }

        [HttpPut("{buildingId}/flats/{flatId}")]
        [AuthenticationFilter(["Manager"])]
        public IActionResult UpdateFlatByFlatId([FromRoute] Guid flatId, [FromBody] UpdateFlatRequestModel updateFlatRequest)
        {

            return Ok(new FlatResponseModel(_iBuildingLogic.UpdateFlat(flatId, updateFlatRequest.ToEntity())));

        }

        [HttpGet("{buildingId}/flats/{flatId}")]
        [AuthenticationFilter(["Manager"])]
        public IActionResult GetFlatByFlatId([FromRoute] Guid flatId)
        {
            return Ok(new FlatResponseModel(_iBuildingLogic.GetFlatByFlatId(flatId)));

        }
    }
}
