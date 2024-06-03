using LogicInterfaces;
using ManagementApi.Filters;
using Microsoft.AspNetCore.Mvc;
using WebModels.UserModels;

namespace ManagementApi.Controllers
{
    [Route("api/v2/userSettings")]
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
        [AuthenticationFilter(["Administrator", "Manager", "MaintenanceEmployee"])]
        public IActionResult GetUserSettings()
        {
            return Ok(new UserResponseModel(_userSettingsLogic.GetUserById(Guid.Parse(HttpContext.Items["UserId"] as string))));
        }

        [HttpPut]
        [AuthenticationFilter(["Administrator", "Manager", "MaintenanceEmployee"])]
        public IActionResult UpdateUserSettings([FromBody] UserUpdateRequestModel userUpdateRequestModel)
        {
            UserResponseModel response = new UserResponseModel(_userSettingsLogic.UpdateUserSettings(Guid.Parse(HttpContext.Items["UserId"] as string), userUpdateRequestModel.ToEntity()));
            return Ok(response);
        }
    }
}
