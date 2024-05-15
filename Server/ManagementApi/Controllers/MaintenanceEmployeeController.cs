using LogicInterfaces;
using ManagementApi.Filters;
using Microsoft.AspNetCore.Mvc;
using WebModels.UserModels;

namespace ManagementApi.Controllers
{
    [Route("api/v1/maintenanceEmployees")]
    [ExceptionFilter]
    [ApiController]
    public class MaintenanceEmployeeController : ControllerBase
    {
        private IMaintenanceEmployeeLogic _maintenanceEmployeeLogic;

        public MaintenanceEmployeeController(IMaintenanceEmployeeLogic iMaintenanceEmployeeLogic)
        {
            _maintenanceEmployeeLogic = iMaintenanceEmployeeLogic;
        }

        [HttpGet]
        [AuthenticationFilter(["Manager"])]
        public IActionResult GetAllMaintenanceEmployees()
        {
            return Ok(_maintenanceEmployeeLogic.GetAllMaintenanceEmployees().Select(employee => new UserResponseModel(employee)).ToList());
        }

        [HttpPost]
        [AuthenticationFilter(["Manager"])]
        public IActionResult CreateMaintenanceEmployee([FromBody] CreateUserRequestModel createMaintenanceEmployeeRequestModel)
        {
            UserResponseModel response = new UserResponseModel(_maintenanceEmployeeLogic.CreateMaintenanceEmployee(createMaintenanceEmployeeRequestModel.ToEntity()));
            return CreatedAtAction("CreateMaintenanceEmployee", new { response.Id }, response);
        }
    }
}