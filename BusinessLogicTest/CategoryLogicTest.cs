using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BusinessLogic;
using Domain;
using LogicInterfaces;
using Moq;
using RepositoryInterfaces;
using CustomExceptions;

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
            categoryRepositoryMock.Setup(x => x.GetAllCategories()).Returns(new List<Category> { });
            categoryRepositoryMock.Setup(x => x.CreateCategory(category)).Returns(expected);

            Category result = categoryLogic.CreateCategory(category);

            categoryRepositoryMock.VerifyAll();

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void GetAllCategoriesTestOk()
        {
            List<Category> categories = new List<Category>
            {
                new Category() { Id = Guid.NewGuid(), Name = "Category 1" },
                new Category() { Id = Guid.NewGuid(), Name = "Category 2" }
            };

            categoryRepositoryMock.Setup(c => c.GetAllCategories()).Returns(categories);

            IEnumerable<Category> result = categoryLogic.GetAllCategories();

            categoryRepositoryMock.VerifyAll();
            CollectionAssert.AreEqual(categories, result.ToList());

        }

        [TestMethod]
        public void CreateCategoryTestCategoryAlreadyExists()
        {

            Category category = new Category()
            {
                Name = "Category1"
            };

            categoryRepositoryMock.Setup(x => x.GetAllCategories()).Returns(new List<Category> { new Category() { Name = "Category1" } });

            Exception exception = null;

            try
            {
                Category result = categoryLogic.CreateCategory(category);
            }
            catch (Exception e)
            {
                exception = e;
            }

            categoryRepositoryMock.VerifyAll();

            Assert.IsInstanceOfType(exception, typeof(CategoryException));
            Assert.AreEqual(exception.Message, "Category with same name already exists");
        }



        [TestMethod]
        public void CreateCategoryTestCategoryWithAnEmptyName()
        {

            Category category = new Category()
            {
                Name = ""
            };
            Exception exception = null;

            try
            {
                Category result = categoryLogic.CreateCategory(category);
            }
            catch (Exception e)
            {
                exception = e;
            }

            categoryRepositoryMock.VerifyAll();

            Assert.IsInstanceOfType(exception, typeof(CategoryException));
            Assert.AreEqual(exception.Message, "Category name cannot be empty");
        }


    }
}
