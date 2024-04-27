using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using Domain;
using LogicInterfaces;
using Moq;
using RepositoryInterfaces;

namespace BusinessLogicTest
{

    [TestClass]
    public class CategoryLogicTest
    {
        private Mock<ICategoryRepository> categoryRepositoryMock;
        private CategoryLogic categoryLogic;

        [TestInitialize]
        public void Initialize()
        {
            categoryRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            categoryLogic = new CategoryLogic(categoryRepositoryMock.Object);
        }

        [TestMethod]
        public void CreateCategoryTestOk()
        {
            Category category = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Category1"
            };
            Category expected = new Category()
            {
                Id = category.Id,
                Name = "Category1"
            };
            categoryRepositoryMock.Setup(x => x.CreateCategory(category)).Returns(expected);

            Category result = categoryLogic.CreateCategory(category);

            categoryRepositoryMock.VerifyAll();

            Assert.AreEqual(expected, result);
        }
    }

    
}
