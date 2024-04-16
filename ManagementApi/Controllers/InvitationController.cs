using LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using WebModels;
using WebModels.InvitationModels;

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
            try
            {
                return Ok(_invitationLogic.GetAllInvitations().Select(invitation => new InvitationResponseModel(invitation)).ToList());
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving all the invitations");
            }
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
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving the invitation");
            }
        }

        [HttpPost]
        public IActionResult CreateInvitation([FromBody] CreateInvitationRequestModel createInvitationRequestModel)
        {
            // TODO: Validar que el email que se esta invitando no exista en la base de datos
            InvitationResponseModel response = new InvitationResponseModel(_invitationLogic.CreateInvitation(createInvitationRequestModel.ToEntity()));
            return CreatedAtAction("CreateInvitation", new { Id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateInvitationById([FromRoute] Guid id, [FromBody] UpdateInvitationRequestModel updateInvitationRequestModel)
        {
            try
            {
                InvitationResponseModel response = new InvitationResponseModel(_invitationLogic.UpdateInvitation(id, updateInvitationRequestModel.IsAccepted));
                return Ok(response);
            }
            catch (ArgumentException)
            {
                return NotFound("There is no invitation with that specific id");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the invitation");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteInvitationById([FromRoute] Guid id)
        {
            try
            {
                _invitationLogic.DeleteInvitation(id);
                return Ok();
            }
            catch (ArgumentException)
            {
                return NoContent();
            }
        }
    }
}
