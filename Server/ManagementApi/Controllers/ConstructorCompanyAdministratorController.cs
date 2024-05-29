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
        [AuthenticationFilter(["ConstructorCompanyAdmin"])]
        public OkObjectResult GetAdminConstructorCompany()
        {
            return Ok(new ConstructorCompanyResponseModel(
                               _iConstructorCompanyAdministratorLogic.
                                              GetAdminConstructorCompany(Guid.Parse(HttpContext.Items["UserId"] as string)
                                                             )));
        }

        [HttpPut]
        [AuthenticationFilter(["ConstructorCompanyAdmin"])]
        public IActionResult SetConstructorCompanyAdministrator([FromBody] SetConstructorCompanyAdministratorRequestModel setConstructorCompanyAdministratorRequestModel)
        {
            return Ok(new ConstructorCompanyAdministratorResponseModel(
                _iConstructorCompanyAdministratorLogic.
                SetConstructorCompanyAdministrator(Guid.Parse(HttpContext.Items["UserId"] as string), setConstructorCompanyAdministratorRequestModel.ConstructorCompanyId)
                ));
        }
    }
}