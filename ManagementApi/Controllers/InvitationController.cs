using LogicInterfaces;
using ManagementApi.Filters;
using Microsoft.AspNetCore.Mvc;
using WebModels.InvitationModels;

namespace ManagementApi.Controllers
{
    [Route("api/v1/invitations")]
    [ExceptionFilter]
    [ApiController]
    public class InvitationController : ControllerBase
    {
        private IInvitationLogic _invitationLogic;

        public InvitationController(IInvitationLogic iInvitationLogic)
        {
            _invitationLogic = iInvitationLogic;
        }

        [HttpGet]
        [AuthenticationFilter([])]
        public IActionResult GetAllInvitations()
        {
            return Ok(_invitationLogic.GetAllInvitations().Select(invitation => new InvitationResponseModel(invitation)).ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetInvitationById([FromRoute] Guid id)
        {
            return Ok(new InvitationResponseModel(_invitationLogic.GetInvitationById(id)));
        }

        [HttpPost]
        [AuthenticationFilter(["Administrator"])]
        public IActionResult CreateInvitation([FromBody] CreateInvitationRequestModel createInvitationRequestModel)
        {
            InvitationResponseModel response = new InvitationResponseModel(_invitationLogic.CreateInvitation(createInvitationRequestModel.ToEntity()));
            return CreatedAtAction("CreateInvitation", new { Id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateInvitationStatusById([FromRoute] Guid id, [FromBody] UpdateInvitationRequestModel updateInvitationRequestModel)
        {
            InvitationResponseModel response = new InvitationResponseModel(_invitationLogic.UpdateInvitationStatus(id, updateInvitationRequestModel.IsAccepted));
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [AuthenticationFilter(["Administrator"])]
        public IActionResult DeleteInvitationById([FromRoute] Guid id)
        {
            _invitationLogic.DeleteInvitation(id);
            return Ok();
        }
    }
}
