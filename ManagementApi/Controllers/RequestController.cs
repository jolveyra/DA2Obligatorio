using LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using WebModels.RequestsModels;

namespace ManagementApi.Controllers
{
    [Route("api/v1/requests")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private IRequestLogic _requestLogic;

        public RequestController(IRequestLogic requestLogic)
        {
            _requestLogic = requestLogic;
        }

        [HttpGet]
        public IActionResult GetAllManagerRequests()
        {
            return Ok(_requestLogic.GetAllRequests().Select(req => new RequestResponseModel(req)).ToList());
        }

         
    }
}
