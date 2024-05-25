using Microsoft.AspNetCore.Mvc;
using LogicInterfaces;
using WebModels.ConstructorCompanyModels;
using Domain;

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

        public OkObjectResult CreateConstructorCompany(CreateConstructorCompanyRequestModel constructorCompanyRequestModel)
        {
            ConstructorCompany constructorCompany = constructorCompanyRequestModel.ToEntity();
            return Ok(new ConstructorCompanyResponseModel(_iConstructorCompanyLogic.CreateConstructorCompany(constructorCompany)));
        }

        public IActionResult GetAllConstructorCompanies()
        {
            return Ok(_iConstructorCompanyLogic.GetAllConstructorCompanies().Select(constructorCompany => new ConstructorCompanyResponseModel(constructorCompany)).ToList());
        }
    }
}
