using Microsoft.AspNetCore.Mvc;
using LogicInterfaces;
using WebModels.ConstructorCompanyAdministratorModels;
using ManagementApi.Filters;

namespace ManagementApi.Controllers
{
    [ApiController]
    [ExceptionFilter]
    [Route("api/v2/constructorCompanyAdministrators")]
    public class ConstructorCompanyAdministratorController: ControllerBase
    {
        private IConstructorCompanyAdministratorLogic _iConstructorCompanyAdministratorLogic;

        public ConstructorCompanyAdministratorController(IConstructorCompanyAdministratorLogic iConstructorCompanyAdministratorLogic)
        {
            _iConstructorCompanyAdministratorLogic = iConstructorCompanyAdministratorLogic;
        }

        [HttpGet]
        [AuthenticationFilter(["Administrator", "ConstructorCompanyAdmin"])]
        public IActionResult GetAllConstructorCompanyAdministrators()
        {
            return Ok(_iConstructorCompanyAdministratorLogic.GetAllConstructorCompanyAdministrators()
                .Select(admin => new ConstructorCompanyAdministratorResponseModel(admin)).ToList());
        }

        [HttpGet("{id}")]
        [AuthenticationFilter(["ConstructorCompanyAdmin"])]
        public IActionResult GetConstructorCompanyAdministrator()
        {
            return Ok(new ConstructorCompanyAdministratorResponseModel(_iConstructorCompanyAdministratorLogic.GetConstructorCompanyAdministrator(Guid.Parse(HttpContext.Items["UserId"] as string))));
        }

        [HttpPut("{id}")]
        [AuthenticationFilter(["ConstructorCompanyAdmin"])]
        public IActionResult UpdateConstructorCompanyAdministrator([FromBody] UpdateConstructorCompanyAdministratorRequestModel updateConstructorCompanyAdministratorRequestModel)
        {
            return Ok(new ConstructorCompanyAdministratorResponseModel(
                _iConstructorCompanyAdministratorLogic.
                UpdateConstructorCompanyAdministrator(Guid.Parse(HttpContext.Items["UserId"] as string), updateConstructorCompanyAdministratorRequestModel.ConstructorCompanyId)
                ));
        }
    }
}