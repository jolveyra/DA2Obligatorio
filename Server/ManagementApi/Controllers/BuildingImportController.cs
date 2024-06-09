using LogicInterfaces;
using ManagementApi.Filters;
using Microsoft.AspNetCore.Mvc;
using WebModels.BuildingModels;
using WebModels.BuildingImportModels;

namespace ManagementApi.Controllers
{
    [Route("api/v2/buildingImports")]
    [ExceptionFilter]
    [ApiController]
    public class BuildingImportController : ControllerBase
    {
        private IBuildingImportLogic buildingImportLogic;

        public BuildingImportController(IBuildingImportLogic iBuildingImportLogic)
        {
            buildingImportLogic = iBuildingImportLogic;
        }

        [HttpPost]
        [AuthenticationFilter(["ConstructorCompanyAdmin"])]
        public IActionResult ImportBuildings(BuildingImportRequestModel buildingImportRequestModel)
        {
            List<BuildingWithoutFlatsResponseModel> response = buildingImportLogic.ImportBuildings(buildingImportRequestModel.DllName, buildingImportRequestModel.FileName, Guid.Parse(HttpContext.Items["UserId"] as string))
                .Select(b => new BuildingWithoutFlatsResponseModel(b)).ToList();
            return CreatedAtAction("ImportBuildings", new { Id = "", route = "buildings" }, response);
        }
    }
}
