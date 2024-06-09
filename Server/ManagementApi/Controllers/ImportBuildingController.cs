using Azure;
using LogicInterfaces;
using ManagementApi.Filters;
using Microsoft.AspNetCore.Mvc;
using WebModels.BuildingModels;
using WebModels.ImportBuildingModels;

namespace ManagementApi.Controllers
{
    [Route("api/v2/importBuildings")]
    [ExceptionFilter]
    [ApiController]
    public class ImportBuildingController : ControllerBase
    {
        private IImportBuildingLogic _importBuildingLogic;

        public ImportBuildingController(IImportBuildingLogic iImportBuildingLogic)
        {
            _importBuildingLogic = iImportBuildingLogic;
        }

        [HttpPost]
        [AuthenticationFilter(["ConstructorCompanyAdmin"])]
        public IActionResult ImportBuildings(ImportBuildingRequestModel importBuildingRequestModel)
        {
            List<BuildingWithoutFlatsResponseModel> response = _importBuildingLogic.ImportBuildings(importBuildingRequestModel.DllName, importBuildingRequestModel.FileName, Guid.Parse(HttpContext.Items["UserId"] as string))
                .Select(b => new BuildingWithoutFlatsResponseModel(b)).ToList();
            return CreatedAtAction("ImportBuildings", new { Id = "", route = "buildings" }, response);
        }
    }
}
