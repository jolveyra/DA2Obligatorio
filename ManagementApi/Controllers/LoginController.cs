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

        [HttpPut] // La dejo como post? o put esta bien?
        public IActionResult LogIn([FromBody] LoginRequestModel request)
        {
            LoginResponseModel response = new LoginResponseModel(_loginLogic.Login(request.ToEntity()));
            return Ok(response);
        }
    }
}
