using LogicInterfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebModels;

namespace ManagementApi.Controllers
{
    [Route("api/v1/invitations")]
    [ApiController]
    public class InvitationController : ControllerBase
    {
        private IInvitationLogic _invitationLogic;

        public InvitationController(IInvitationLogic iInvitationLogic)
        {
            _invitationLogic = iInvitationLogic;
        }

        [HttpGet]
        public IActionResult GetAllInvitations()
        {
            return Ok(_invitationLogic.GetAllInvitations().Select(invitation => new InvitationResponseModel(invitation)).ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetInvitationById([FromRoute] Guid id)
        {
            return Ok(new InvitationResponseModel(_invitationLogic.GetInvitationById(id)));
        }
    }
}
