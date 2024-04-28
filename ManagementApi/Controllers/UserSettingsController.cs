using LogicInterfaces;
using ManagementApi.Filters;
using Microsoft.AspNetCore.Mvc;
using WebModels.UserResponseModel;

namespace ManagementApi.Controllers
{
    [Route("api/v1/userSettings")]
    [ExceptionFilter]
    [ApiController]
    public class UserSettingsController : ControllerBase
    {
        private IUserSettingsLogic _userSettingsLogic;

        public UserSettingsController(IUserSettingsLogic iUserSettingsLogic)
        {
            _userSettingsLogic = iUserSettingsLogic;
        }

        [HttpGet]
        public IActionResult GetUserSettings([FromHeader] Guid userId)
        {
            return Ok(new UserResponseModel(_userSettingsLogic.GetUserById(userId)));
        }

        [HttpPut]
        public IActionResult UpdateUserSettings([FromHeader] Guid userId, [FromBody] UserUpdateRequestModel userUpdateRequestModel)
        {
            UserResponseModel response = new UserResponseModel(_userSettingsLogic.UpdateUserSettings(userId, userUpdateRequestModel.ToEntity()));
            return Ok(response);
        }
    }
}
