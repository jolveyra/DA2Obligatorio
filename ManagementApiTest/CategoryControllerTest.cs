using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using LogicInterfaces;
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModels.CategoryModels;

namespace ManagementApiTest
{
    [TestClass]
    public class CategoryControllerTest
    {
        private Mock<ICategoryLogic> categoryLogicMock;
        private CategoryController categoryController;

        [TestInitialize]
        public void TestInitialize()
        {
            categoryLogicMock = new Mock<ICategoryLogic>(MockBehavior.Strict);
            categoryController = new CategoryController(categoryLogicMock.Object);
        }


        [TestMethod]
        public void GetAllCategoriesTestOk()
        {
            IEnumerable<Category> categories = new List<Category>
            {
                new Category() { Id = Guid.NewGuid(), Name = "Category 1" },
                new Category() { Id = Guid.NewGuid(), Name = "Category 2" }
            };

            categoryLogicMock.Setup(c => c.GetAllCategories()).Returns(categories);

            OkObjectResult expected = new OkObjectResult(new List<CategoryResponseModel>
            {
                new CategoryResponseModel(categories.First()),
                new CategoryResponseModel(categories.Last())
            });

            List<CategoryResponseModel> expectedObject = expected.Value as List<CategoryResponseModel>;

            OkObjectResult result = categoryController.GetAllCategories() as OkObjectResult;
            List<CategoryResponseModel> objectResult = result.Value as List<CategoryResponseModel>;

            categoryLogicMock.VerifyAll();
            Assert.AreEqual(expected.StatusCode, result.StatusCode);
            CollectionAssert.AreEqual(expectedObject, objectResult);
        }

    }
}
