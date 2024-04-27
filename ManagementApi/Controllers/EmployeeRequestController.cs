using LogicInterfaces;
using ManagementApi.Filters;
using Microsoft.AspNetCore.Mvc;
using WebModels.RequestsModels;

namespace ManagementApi.Controllers
{
    [Route("api/v1/employeeRequests")]
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
        // Casi seguro que no es [FromHeader] ya que no va el userId en el header sino que su token,
        // no deberiamos guardar en el authorization filter de alguna forma para pasarselo a las func??
        public IActionResult GetAllEmployeeRequests([FromHeader] Guid userId)
        {
            return Ok(_requestLogic.GetAllRequestsByEmployeeId(userId).Select(r => new RequestResponseModel(r)).ToList());
        }
    }
}
