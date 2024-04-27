using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DataAccess.Repositories;
using DataAccess.Context;
using DataAccess;
using Moq;
using RepositoryInterfaces;
using Moq.EntityFrameworkCore;


namespace DataAccessTest
{
    [TestClass]
    public class CategoryRepositoryTest
    {

        private ICategoryRepository categoryRepository;
        private Mock<BuildingBossContext> mockContext;

        [TestInitialize]
        public void Initialize()
        {
            mockContext = new Mock<BuildingBossContext>();

            categoryRepository = new CategoryRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetAllCategoriesTestOk()
        {
            List<Category> categories = new List<Category>()
            {
                new Category() { Id = Guid.NewGuid(), Name = "Category 1" },
                new Category() { Id = Guid.NewGuid(), Name = "Category 2" }

            };

            mockContext.Setup(x => x.Categories).ReturnsDbSet(categories);

            IEnumerable<Category> result = categoryRepository.GetAllCategories();

            CollectionAssert.AreEqual(categories, result.ToList());
        }

    }
}   
