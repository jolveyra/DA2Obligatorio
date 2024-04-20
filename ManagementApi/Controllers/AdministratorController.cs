using LogicInterfaces;
using ManagementApi.Filters;
using Microsoft.AspNetCore.Mvc;
using WebModels.AdministratorModels;

namespace ManagementApi.Controllers
{
    [Route("api/v1/administrators")]
    [ExceptionFilter]
    [AuthenticationFilter]
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

        [HttpPost]
        public IActionResult CreateAdministrator([FromBody] CreateAdministratorRequestModel administratorCreateModel)
        {
            AdministratorResponseModel response = new AdministratorResponseModel(_administratorLogic.CreateAdministrator(administratorCreateModel.ToEntity()));
            return CreatedAtAction("CreateAdministrator", new { response.Id }, response);
        }
    }
}