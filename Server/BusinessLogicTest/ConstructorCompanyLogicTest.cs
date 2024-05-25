using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using RepositoryInterfaces;
using BusinessLogic;
using Moq;

namespace BusinessLogicTest
{
    [TestClass]
    public class ConstructorCompanyLogicTest
    {
        private Mock<IConstructorCompanyRepository> constructorCompanyRepositoryMock;
        private ConstructorCompanyLogic constructorCompanyLogic;

        [TestInitialize]
        public void TestInitialize()
        {
            constructorCompanyRepositoryMock = new Mock<IConstructorCompanyRepository>(MockBehavior.Strict);
            constructorCompanyLogic = new ConstructorCompanyLogic(constructorCompanyRepositoryMock.Object);
        }

        [TestMethod]
        public void GetAllConstructorCompaniesTestOk()
        {
            IEnumerable<ConstructorCompany> constructorCompanies = new List<ConstructorCompany>
            {
                new ConstructorCompany() { Id = Guid.NewGuid(), Name = "ConstructorCompany 1" },
                new ConstructorCompany() { Id = Guid.NewGuid(), Name = "ConstructorCompany 2" }
            };

            constructorCompanyRepositoryMock.Setup(c => c.GetAllConstructorCompanies()).Returns(constructorCompanies);

            IEnumerable<ConstructorCompany> result = constructorCompanyLogic.GetAllConstructorCompanies();

            constructorCompanyRepositoryMock.VerifyAll();
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
            constructorCompanyRepositoryMock.Setup(x => x.CreateConstructorCompany(It.IsAny<ConstructorCompany>())).Returns(expected);

            ConstructorCompany result = constructorCompanyLogic.CreateConstructorCompany(constructorCompany);

            constructorCompanyRepositoryMock.VerifyAll();

            Assert.AreEqual(expected, result);
        }

    }
}
