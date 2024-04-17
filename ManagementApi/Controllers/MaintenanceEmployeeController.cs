using LogicInterfaces;
using ManagementApi.Filters;
using Microsoft.AspNetCore.Mvc;
using WebModels.MaintenanceEmployeeModels;

namespace ManagementApi.Controllers
{
    [Route("api/v1/administrators")]
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
        public IActionResult GetAllMaintenanceEmployees()
        {
            return Ok(_maintenanceEmployeeLogic.GetAllMaintenanceEmployees().Select(employee => new MaintenanceEmployeeResponseModel(employee)).ToList());
        }

        [HttpPost]
        public IActionResult CreateMaintenanceEmployee([FromBody] CreateMaintenanceEmployeeRequestModel createMaintenanceEmployeeRequestModel)
        {
            MaintenanceEmployeeResponseModel response = new MaintenanceEmployeeResponseModel(_maintenanceEmployeeLogic.CreateMaintenanceEmployee(createMaintenanceEmployeeRequestModel.ToEntity()));
            return CreatedAtAction("CreateMaintenanceEmployee", new { response.Id }, response);
        }
    }
}