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

        [HttpPost]
        public IActionResult CreateConstructorCompany(CreateConstructorCompanyRequestModel constructorCompanyRequestModel)
        {
            ConstructorCompany constructorCompany = constructorCompanyRequestModel.ToEntity();
            return Ok(new ConstructorCompanyResponseModel(_iConstructorCompanyLogic.CreateConstructorCompany(constructorCompany)));
        }

        [HttpGet]
        public IActionResult GetAllConstructorCompanies()
        {
            return Ok(_iConstructorCompanyLogic.GetAllConstructorCompanies().Select(constructorCompany => new ConstructorCompanyResponseModel(constructorCompany)).ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetConstructorCompanyById(Guid id)
        {
            return Ok(new ConstructorCompanyResponseModel(_iConstructorCompanyLogic.GetConstructorCompanyById(id)));
        }
    }
}
