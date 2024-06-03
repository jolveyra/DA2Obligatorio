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
            return Ok(_requestLogic.GetAllRequestsByEmployeeId(Guid.Parse(HttpContext.Items["UserId"] as string)).Select(r => new RequestResponseModel(r)).ToList());
        }

        [HttpPut("{id}")]
        [AuthenticationFilter(["MaintenanceEmployee"])]
        public IActionResult UpdateRequestStatusById([FromRoute] Guid id, [FromBody] RequestUpdateStatusModel requestUpdateStatusModel)
        {
            RequestResponseModel response = new RequestResponseModel(_requestLogic.UpdateRequestStatusById(id, requestUpdateStatusModel.ToEntity()));
            return Ok(response);
        }
    }
}
