using LogicInterfaces;
using ManagementApi.Filters;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(_reportLogic.GetReport(filter));
        }
    }
}
