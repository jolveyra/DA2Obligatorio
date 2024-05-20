using BusinessLogic;
using LogicInterfaces;
using ManagementApi.Filters;
using Microsoft.AspNetCore.Mvc;
using WebModels.BuildingModels;
using WebModels.CategoryModels;

namespace ManagementApi.Controllers
{

    [Route("api/v1/categories")]
    [ApiController]
    [ExceptionFilter]
    public class CategoryController : ControllerBase
    {
        private ICategoryLogic _iCategoryLogic;

        public CategoryController(ICategoryLogic iCategoryLogic)
        {
            this._iCategoryLogic = iCategoryLogic;
        }

        [HttpPost]
        [AuthenticationFilter(["Administrator"])]
        public IActionResult CreateCategory(CreateCategoryRequestModel categoryRequestModel)
        {

            CategoryResponseModel response = new CategoryResponseModel(_iCategoryLogic.CreateCategory(categoryRequestModel.ToEntity()));
            return CreatedAtAction("CreateCategory", new { Id = response.Id }, response);
        }

        [HttpGet]
        [AuthenticationFilter(["Administrator", "Manager"])]
        public IActionResult GetAllCategories()
        {
            return Ok(_iCategoryLogic.GetAllCategories().Select(category => new CategoryResponseModel(category)).ToList());
        }

    }
}
