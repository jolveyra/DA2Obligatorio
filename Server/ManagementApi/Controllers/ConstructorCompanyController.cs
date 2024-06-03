using Microsoft.AspNetCore.Mvc;
using LogicInterfaces;
using WebModels.ConstructorCompanyModels;
using Domain;
using ManagementApi.Filters;

namespace ManagementApi.Controllers
{
    [Route("api/v2/constructorCompanies")]

    [ApiController]
    [ExceptionFilter]
    public class ConstructorCompanyController : ControllerBase
    {

        private IConstructorCompanyLogic _iConstructorCompanyLogic;

        public ConstructorCompanyController(IConstructorCompanyLogic iConstructorCompanyLogic)
        {
            _iConstructorCompanyLogic = iConstructorCompanyLogic;
        }

        [HttpPost]
        [AuthenticationFilter(["ConstructorCompanyAdmin"])]
        public IActionResult CreateConstructorCompany(CreateConstructorCompanyRequestModel constructorCompanyRequestModel)
        {
            ConstructorCompany constructorCompany = constructorCompanyRequestModel.ToEntity();
            return Ok(new ConstructorCompanyResponseModel(_iConstructorCompanyLogic.CreateConstructorCompany(constructorCompany, Guid.Parse(HttpContext.Items["UserId"] as string))));
        }

        [HttpGet]
        [AuthenticationFilter(["ConstructorCompanyAdmin"])]
        public IActionResult GetAllConstructorCompanies()
        {
            return Ok(_iConstructorCompanyLogic.GetAllConstructorCompanies().Select(constructorCompany => new ConstructorCompanyResponseModel(constructorCompany)).ToList());
        }

        [HttpGet("{id}")]
        [AuthenticationFilter(["ConstructorCompanyAdmin"])]
        public IActionResult GetConstructorCompanyById(Guid id)
        {
            return Ok(new ConstructorCompanyResponseModel(_iConstructorCompanyLogic.GetConstructorCompanyById(id)));
        }

        [HttpPut("{constructorCompanyId}")]
        [AuthenticationFilter(["ConstructorCompanyAdmin"])]
        public OkObjectResult UpdateConstructorCompany([FromBody] UpdateConstructorCompanyRequestModel request, [FromRoute] Guid constructorCompanyId)
        {
            return Ok(new ConstructorCompanyResponseModel(_iConstructorCompanyLogic.
                UpdateConstructorCompany(request.Name,
                Guid.Parse(HttpContext.Items["UserId"] as string),
                constructorCompanyId)));
        }
    }
}
