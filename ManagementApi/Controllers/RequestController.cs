using LogicInterfaces;
using ManagementApi.Filters;
using Microsoft.AspNetCore.Mvc;
using WebModels.RequestsModels;

namespace ManagementApi.Controllers
{
    [Route("api/v1/requests")]
    [ExceptionFilter]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private IRequestLogic _requestLogic;

        public RequestController(IRequestLogic requestLogic)
        {
            _requestLogic = requestLogic;
        }

        [HttpGet]
        [AuthenticationFilter([])]
        public IActionResult GetAllManagerRequests([FromQuery] string? category)
        {
            var requests = _requestLogic.GetAllRequests();
            
            if (category is not null)
            {
                requests = requests.Where(req => req.Category.Name == category);
            }
            
            return Ok(requests.Select(req => new RequestResponseModel(req)).ToList());
        }

        [HttpGet("{id}")]
        [AuthenticationFilter([])]
        public IActionResult GetRequestById([FromRoute] Guid id)
        {
            return Ok(new RequestResponseModel(_requestLogic.GetRequestById(id)));
        }

        [HttpPost]
        [AuthenticationFilter([])]
        public IActionResult CreateRequest([FromBody] RequestCreateModel requestCreateModel)
        {
            var request = _requestLogic.CreateRequest(requestCreateModel.ToEntity());
            return CreatedAtAction(nameof(GetRequestById), new { id = request.Id }, new RequestResponseModel(request));
        }

        [HttpPut("{id}")]
        [AuthenticationFilter([])]
        public IActionResult UpdateRequestById([FromRoute] Guid id, [FromBody] RequestUpdateModel requestUpdateModel)
        {
            var request = _requestLogic.UpdateRequest(requestUpdateModel.ToEntity(id));
            return Ok(new RequestResponseModel(request));
        }
    }
}