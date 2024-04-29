using LogicInterfaces;
using ManagementApi.Filters;
using Microsoft.AspNetCore.Mvc;
using WebModels.LoginModels;

namespace ManagementApi.Controllers
{
    [Route("api/v1/login")]
    [ExceptionFilter]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginLogic _loginLogic;

        public LoginController(ILoginLogic loginLogic)
        {
            _loginLogic = loginLogic;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequestModel request)
        {
            LoginResponseModel response = new LoginResponseModel(_loginLogic.Login(request.ToEntity()));
            return Ok(response);
        }
    }
}
