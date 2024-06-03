using Microsoft.AspNetCore.Mvc;

namespace ManagementApi.Controllers
{
    [Route("api/v2/teapot")]
    [ApiController]
    public class TeapotController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetTeapot()
        {
            return StatusCode(418, "I got tired of buildings and managers, I'm just gonna be a teapot for a while");
        }
    }
}
