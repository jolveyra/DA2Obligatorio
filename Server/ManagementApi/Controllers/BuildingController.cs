using Microsoft.AspNetCore.Mvc;
using LogicInterfaces;
using WebModels.BuildingModels;
using ManagementApi.Filters;

namespace ManagementApi.Controllers
{
    [Route("api/v2/buildings")]
    [ApiController]
    [ExceptionFilter]
    public class BuildingController : ControllerBase
    {
        private IBuildingLogic _iBuildingLogic;

        public BuildingController(IBuildingLogic iBuildingLogic)
        {
            _iBuildingLogic = iBuildingLogic;
        }

        [HttpGet]
        [AuthenticationFilter(["Manager"])]
        public IActionResult GetAllBuildings()
        {
            List<BuildingResponseModel> response = _iBuildingLogic.GetAllBuildings(Guid.Parse(HttpContext.Items["UserId"] as string)).ToList().
                Select(building => new BuildingResponseModel(building)
                {
                    Flats = _iBuildingLogic.GetAllBuildingFlats(building.Id).Select(f => new FlatResponseModel(f)).ToList()
                }).ToList();

            return Ok(response);
        }

        [HttpGet("{buildingId}")]
        [AuthenticationFilter(["Manager"])]
        public IActionResult GetBuildingById([FromRoute] Guid buildingId)
        {
            
            return Ok(new BuildingResponseModel(_iBuildingLogic.GetBuildingById(buildingId))
            {
                Flats = _iBuildingLogic.GetAllBuildingFlats(buildingId).Select(f => new FlatResponseModel(f)).ToList()
            });
            
        }

        [HttpPut("{id}")]
        [AuthenticationFilter(["Manager"])]
        public IActionResult UpdateBuildingById([FromRoute] Guid id, [FromBody] UpdateBuildingRequestModel updateBuildingRequest)
        {
            return Ok(new BuildingResponseModel(_iBuildingLogic.UpdateBuilding(id, updateBuildingRequest.ToEntity()))
            {
                Flats = _iBuildingLogic.GetAllBuildingFlats(id).Select(f => new FlatResponseModel(f)).ToList()
            });
            
        }

        [HttpPut("{buildingId}/flats/{flatId}")]
        [AuthenticationFilter(["Manager"])]
        public IActionResult UpdateFlatByFlatId([FromRoute] Guid buildingId, [FromRoute] Guid flatId, [FromBody] UpdateFlatRequestModel updateFlatRequest)
        {
            return Ok(new FlatResponseModel(_iBuildingLogic.UpdateFlat(buildingId, flatId, updateFlatRequest.ToEntity(), updateFlatRequest.ChangeOwner)));
        }

        [HttpGet("{buildingId}/flats/{flatId}")]
        [AuthenticationFilter(["Manager"])]
        public IActionResult GetFlatByBuildingAndFlatId([FromRoute] Guid buildingId, [FromRoute] Guid flatId)
        {
            return Ok(new FlatResponseModel(_iBuildingLogic.GetFlatByBuildingAndFlatId(buildingId, flatId)));

        }
    }
}
