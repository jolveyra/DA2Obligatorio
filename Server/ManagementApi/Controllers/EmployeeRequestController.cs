using LogicInterfaces;
using ManagementApi.Filters;
using Microsoft.AspNetCore.Mvc;
using WebModels.RequestsModels;

namespace ManagementApi.Controllers
{
    [Route("api/v2/employeeRequests")]
    [ExceptionFilter]
    [ApiController]
    public class EmployeeRequestController : ControllerBase
    {
        private readonly IEmployeeRequestLogic _requestLogic;

        public EmployeeRequestController(IEmployeeRequestLogic requestLogic)
        {
            _requestLogic = requestLogic;
        }

        [HttpGet]
        [AuthenticationFilter(["MaintenanceEmployee"])]
        public IActionResult GetAllEmployeeRequests()
        {
            return Ok(_requestLogic.GetAllRequestsByEmployeeId(Guid.Parse(HttpContext.Items["UserId"] as string)).Select(r => new RequestResponseWithoutEmployeeModel(r)).ToList());
        }

        [HttpPut("{id}")]
        [AuthenticationFilter(["MaintenanceEmployee"])]
        public IActionResult UpdateRequestStatusById([FromRoute] Guid id, [FromBody] RequestUpdateStatusModel requestUpdateStatusModel)
        {
            RequestResponseWithoutEmployeeModel response = new RequestResponseWithoutEmployeeModel(_requestLogic.UpdateRequestStatusById(id, requestUpdateStatusModel.ToEntity()));
            return Ok(response);
        }
    }
}
