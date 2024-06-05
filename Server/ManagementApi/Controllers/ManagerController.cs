using LogicInterfaces;
using ManagementApi.Filters;
using Microsoft.AspNetCore.Mvc;
using WebModels.UserModels;

namespace ManagementApi.Controllers
{
    [Route("api/v2/managers")]
    [ExceptionFilter]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private IManagerLogic _managerLogic;

        public ManagerController(IManagerLogic iManagerLogic)
        {
            _managerLogic = iManagerLogic;
        }

        [HttpGet]
        [AuthenticationFilter(["Administrator", "ConstructorCompanyAdmin"])]
        public IActionResult GetAllManagers()
        {
            return Ok(_managerLogic.GetAllManagers().Select(m => new UserResponseModel(m)).ToList());
        }
    }
}
