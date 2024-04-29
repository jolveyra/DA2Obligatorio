using LogicInterfaces;
using ManagementApi.Filters;
using Microsoft.AspNetCore.Mvc;
using WebModels.ManagerModels;

namespace ManagementApi.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/v1/managers")]
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
        [AuthenticationFilter(["Administrator"])]
        public IActionResult GetAllManagers()
        {
            return Ok(_managerLogic.GetAllManagers().Select(m => new ManagerResponseModel(m)).ToList());
        }
    }
}
