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
        public IActionResult GetAllManagerRequests([FromQuery] string? category)
        {
            var requests = _requestLogic.GetAllRequests();
            
            if (category is not null)
            {
                requests = requests.Where(req => req.Category.Name == category);
            }
            
            return Ok(requests.Select(req => new RequestResponseModel(req)).ToList());
        }

        // TODO: employee requests, report requests

        [HttpGet("{id}")]
        public IActionResult GetRequestById([FromRoute] Guid id)
        {
            return Ok(new RequestResponseModel(_requestLogic.GetRequestById(id)));
        }
    }
}
