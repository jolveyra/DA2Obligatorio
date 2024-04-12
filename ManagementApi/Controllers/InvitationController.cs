using LogicInterfaces;
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
            try
            {
                return Ok(new InvitationResponseModel(_invitationLogic.GetInvitationById(id)));
            }
            catch (ArgumentException)
            {
                return NotFound("There is no invitation with that specific id");
            }
        }

        [HttpPost]
        public IActionResult CreateInvitation([FromBody] InvitationRequestModel invitationRequestModel)
        {
            // TODO: Validar que el email que se esta invitando no exista en la base de datos
            InvitationResponseModel response = new InvitationResponseModel(_invitationLogic.CreateInvitation(invitationRequestModel.ToEntity()));
            return CreatedAtAction("CreateInvitation", new { Id = response.Id }, response);
        }
    }
}
