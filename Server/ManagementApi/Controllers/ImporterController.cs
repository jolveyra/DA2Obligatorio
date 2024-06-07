using BusinessLogic;
using LogicInterfaces;
using ManagementApi.Filters;
using Microsoft.AspNetCore.Mvc;
using WebModels.ConstructorCompanyModels;
using WebModels.ImporterModels;
using WebModels.InvitationModels;

namespace ManagementApi.Controllers
{
    [Route("api/v2/importers")]
    [ExceptionFilter]
    [ApiController]
    public class ImporterController: ControllerBase
    {
        private IImporterLogic _importerLogic;

        public ImporterController(IImporterLogic iImporterLogic)
        {
            _importerLogic = iImporterLogic;
        }

        [HttpPost]
        [AuthenticationFilter(["ConstructorCompanyAdmin"])]
        public IActionResult CreateImporter(CreateImporterRequestModel importerRequestModel)
        {
            return Ok(new ImporterResponseModel(_importerLogic.CreateImporter(importerRequestModel.ToEntity())));
        }

        [HttpGet]
        [AuthenticationFilter(["ConstructorCompanyAdmin"])]
        public IActionResult GetAllImporters()
        {
            return Ok(_importerLogic.GetAllImporters()
                .Select(importer => new ImporterResponseModel(importer))
                .ToList());
        }
    }
}
