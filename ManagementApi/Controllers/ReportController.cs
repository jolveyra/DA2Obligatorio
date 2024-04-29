using LogicInterfaces;
using ManagementApi.Filters;
using Microsoft.AspNetCore.Mvc;
using WebModels.ReportModels.Out;

namespace ManagementApi.Controllers
{
    [Route("api/v1/reports")]
    [ExceptionFilter]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportLogic _reportLogic;

        public ReportController(IReportLogic reportLogic)
        {
            _reportLogic = reportLogic;
        }

        [HttpGet]
        public IActionResult GetReport([FromQuery] string filter)
        {
            return Ok(_reportLogic.GetReport(Guid.Parse(HttpContext.Items["UserId"] as string), filter).Select(t => new ReportResponseModel(t)));
        }
    }
}
