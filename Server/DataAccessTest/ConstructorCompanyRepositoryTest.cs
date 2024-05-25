using DataAccess.Context;
using DataAccess.Repositories;
using Moq;
using RepositoryInterfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq.EntityFrameworkCore;

namespace DataAccessTest
{
    [TestClass]
    public class ConstructorCompanyRepositoryTest
    {

        private ConstructorCompanyRepository constructorCompanyRepository;
        private Mock<BuildingBossContext> mockContext;

        [TestInitialize]
        public void Initialize()
        {
            mockContext = new Mock<BuildingBossContext>();

            constructorCompanyRepository = new ConstructorCompanyRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetAllConstructorCompaniesTestOk()
        {
            IEnumerable<ConstructorCompany> constructorCompanies = new List<ConstructorCompany>
            {
                new ConstructorCompany() { Id = Guid.NewGuid(), Name = "ConstructorCompany 1" },
                new ConstructorCompany() { Id = Guid.NewGuid(), Name = "ConstructorCompany 2" }
            };

            mockContext.Setup(c => c.ConstructorCompanies).ReturnsDbSet(constructorCompanies);

            IEnumerable<ConstructorCompany> result = constructorCompanyRepository.GetAllConstructorCompanies();

            CollectionAssert.AreEqual(constructorCompanies.ToList(), result.ToList());
        }

        [TestMethod]
        public void CreateConstructorCompanyTestOk()
        {
            ConstructorCompany constructorCompany = new ConstructorCompany()
            {
                Id = Guid.NewGuid(),
                Name = "ConstructorCompany1"
            };
            ConstructorCompany expected = new ConstructorCompany()
            {
                Id = constructorCompany.Id,
                Name = "ConstructorCompany1"
            };

            mockContext.Setup(x => x.ConstructorCompanies).ReturnsDbSet(new List<ConstructorCompany> { });
            mockContext.Setup(x => x.Add(constructorCompany));
            mockContext.Setup(x => x.SaveChanges()).Returns(1);

            ConstructorCompany result = constructorCompanyRepository.CreateConstructorCompany(constructorCompany);

            mockContext.Verify(x => x.ConstructorCompanies, Times.Once());
            mockContext.Verify(x => x.ConstructorCompanies.Add(constructorCompany), Times.Once());
            mockContext.Verify(x => x.SaveChanges(), Times.Once());

            Assert.AreEqual(expected, result);
        }

    }
}
