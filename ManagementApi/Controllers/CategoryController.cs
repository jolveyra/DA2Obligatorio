using LogicInterfaces;
using ManagementApi.Filters;
using Microsoft.AspNetCore.Mvc;
using WebModels.CategoryModels;

namespace ManagementApi.Controllers
{

    [Route("api/v1/buildings")]
    [ApiController]
    [ExceptionFilter]
    public class CategoryController : ControllerBase
    {
        private ICategoryLogic _iCategoryLogic;

        public CategoryController(ICategoryLogic iCategoryLogic)
        {
            this._iCategoryLogic = iCategoryLogic;
        }

        [HttpGet]
        [AuthenticationFilter(["Administrator", "Manager"])]
        public IActionResult GetAllCategories()
        {
            return Ok(_iCategoryLogic.GetAllCategories().Select(category => new CategoryResponseModel(category)).ToList());
        }

    }
}
