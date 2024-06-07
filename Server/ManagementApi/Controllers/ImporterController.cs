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

        [HttpGet]
        [AuthenticationFilter(["ConstructorCompanyAdmin"])]
        public OkObjectResult GetAllImporters()
        {
            return Ok(_importerLogic.GetAllImporters()
                .Select(importer => new ImporterResponseModel(importer))
                .ToList());
        }
    }
}
