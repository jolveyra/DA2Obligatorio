using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using RepositoryInterfaces;
using BusinessLogic;
using Moq;
using CustomExceptions;
using LogicInterfaces;

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

            constructorCompanyRepositoryMock.Setup(x => x.GetAllConstructorCompanies()).Returns(new List<ConstructorCompany> { });

            constructorCompanyRepositoryMock.Setup(x => x.CreateConstructorCompany(It.IsAny<ConstructorCompany>())).Returns(expected);

            ConstructorCompany result = constructorCompanyLogic.CreateConstructorCompany(constructorCompany);

            constructorCompanyRepositoryMock.VerifyAll();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CreateConstructorCompanyTestNameAlreadyExists()
        {
            ConstructorCompany constructorCompany = new ConstructorCompany()
            {
                Id = Guid.NewGuid(),
                Name = "ConstructorCompany1"
            };
            constructorCompanyRepositoryMock.Setup(x => x.GetAllConstructorCompanies()).Returns(new List<ConstructorCompany> { constructorCompany });


            Exception exception = null;

            try
            {
                ConstructorCompany result = constructorCompanyLogic.CreateConstructorCompany(constructorCompany);
            }
            catch (Exception e)
            {
                exception = e;
            }

            constructorCompanyRepositoryMock.VerifyAll();

            Assert.IsInstanceOfType(exception, typeof(ConstructorCompanyException));
            Assert.AreEqual(exception.Message, "Constructor company with same name already exists");
        }

        [TestMethod]
        public void CreateConstructorCompanyTestNullName()
        {
            ConstructorCompany constructorCompany = new ConstructorCompany()
            {
                Id = Guid.NewGuid(),
                Name = null
            };

            constructorCompanyRepositoryMock.Setup(x => x.GetAllConstructorCompanies()).Returns(new List<ConstructorCompany> { constructorCompany });

            Exception exception = null;

            try
            {
                ConstructorCompany result = constructorCompanyLogic.CreateConstructorCompany(constructorCompany);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(ConstructorCompanyException));
            Assert.AreEqual(exception.Message, "Constructor company name cannot be empty");
        }

    }
}
