using LogicInterfaces;
using ManagementApi.Filters;
using Microsoft.AspNetCore.Mvc;
using WebModels.AdministratorModels;

namespace ManagementApi.Controllers
{
    [Route("api/v1/administrators")]
    [ExceptionFilter]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        private IAdministratorLogic _administratorLogic;

        public AdministratorController(IAdministratorLogic iAdministratorLogic)
        {
            _administratorLogic = iAdministratorLogic;
        }

        [HttpGet]
        public IActionResult GetAllAdministrators()
        {
            return Ok(_administratorLogic.GetAllAdministrators().Select(admin => new AdministratorResponseModel(admin)).ToList());
        }
    }
}