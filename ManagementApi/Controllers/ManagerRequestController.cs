using LogicInterfaces;
using ManagementApi.Filters;
using Microsoft.AspNetCore.Mvc;
using WebModels.RequestsModels;

namespace ManagementApi.Controllers
{
    [Route("api/v1/requests")]
    [ExceptionFilter]
    [ApiController]
    public class ManagerRequestController : ControllerBase
    {
        private IManagerRequestLogic _managerRequestLogic;

        public ManagerRequestController(IManagerRequestLogic managerRequestLogic)
        {
            _managerRequestLogic = managerRequestLogic;
        }

        [HttpGet]
        [AuthenticationFilter(["Manager"])]
        public IActionResult GetAllManagerRequests([FromQuery] string? category)
        {
            var requests = _managerRequestLogic.GetAllRequests();
            
            if (category is not null)
            {
                requests = requests.Where(req => req.Category.Name == category);
            }
            
            return Ok(requests.Select(req => new RequestResponseModel(req)).ToList());
        }

        [HttpGet("{id}")]
        [AuthenticationFilter(["Manager"])]
        public IActionResult GetRequestById([FromRoute] Guid id)
        {
            return Ok(new RequestResponseModel(_managerRequestLogic.GetRequestById(id)));
        }

        [HttpPost]
        [AuthenticationFilter(["Manager"])]
        public IActionResult CreateRequest([FromBody] RequestCreateModel requestCreateModel)
        {
            var request = _managerRequestLogic.CreateRequest(requestCreateModel.ToEntity());
            return CreatedAtAction(nameof(GetRequestById), new { id = request.Id }, new RequestResponseModel(request));
        }

        [HttpPut("{id}")]
        [AuthenticationFilter(["Manager", "MaintenanceEmployee"])]
        public IActionResult UpdateRequestById([FromRoute] Guid id, [FromBody] RequestUpdateModel requestUpdateModel)
        {
            var request = _managerRequestLogic.UpdateRequest(requestUpdateModel.ToEntity(id));
            return Ok(new RequestResponseModel(request));
        }
    }
}