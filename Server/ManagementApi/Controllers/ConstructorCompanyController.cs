using Microsoft.AspNetCore.Mvc;
using LogicInterfaces;
using WebModels.ConstructorCompanyModels;


namespace ManagementApi.Controllers
{
    [Route("api/v2/constructorCompanies")]
    [ApiController]
    public class ConstructorCompanyController : ControllerBase
    {

        private IConstructorCompanyLogic _iConstructorCompanyLogic;

        public ConstructorCompanyController(IConstructorCompanyLogic iConstructorCompanyLogic)
        {
            _iConstructorCompanyLogic = iConstructorCompanyLogic;
        }

        public IActionResult GetAllConstructorCompanies()
        {
            return Ok(_iConstructorCompanyLogic.GetAllConstructorCompanies().Select(constructorCompany => new ConstructorCompanyResponseModel(constructorCompany)).ToList());
        }
    }
}
