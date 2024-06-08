using LogicInterfaces;
using ManagementApi.Filters;
using Microsoft.AspNetCore.Mvc;
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
            _importBuildingLogic.ImportBuildings(importBuildingRequestModel.DllName, importBuildingRequestModel.FileName,  Guid.Parse(HttpContext.Items["UserId"] as string));
            return Ok(new ImportBuildingResponseModel("Imported successfully"));
        }
    }
}
