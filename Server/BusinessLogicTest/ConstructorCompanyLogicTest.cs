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
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace BusinessLogicTest
{
    [TestClass]
    public class ConstructorCompanyLogicTest
    {
        private Mock<IConstructorCompanyRepository> constructorCompanyRepositoryMock;
        private Mock<IUserRepository> userRepositoryMock;
        private ConstructorCompanyLogic constructorCompanyLogic;

        [TestInitialize]
        public void TestInitialize()
        {
            constructorCompanyRepositoryMock = new Mock<IConstructorCompanyRepository>(MockBehavior.Strict);
            userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            constructorCompanyLogic = new ConstructorCompanyLogic(constructorCompanyRepositoryMock.Object, userRepositoryMock.Object);
        }

        [TestMethod]
        public void GetAllConstructorCompaniesTestOk()
        {
            IEnumerable<ConstructorCompany> constructorCompanies = new List<ConstructorCompany>
            {
                new ConstructorCompany() { Id = Guid.NewGuid(), Name = "ConstructorCompanyId 1" },
                new ConstructorCompany() { Id = Guid.NewGuid(), Name = "ConstructorCompanyId 2" }
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

        [TestMethod]
        public void GetConstructorCompanyByIdTestOk()
        {
            Guid id = Guid.NewGuid();
            ConstructorCompany constructorCompany = new ConstructorCompany()
            {
                Id = id,
                Name = "ConstructorCompanyId 1"
            };

            constructorCompanyRepositoryMock.Setup(c => c.GetConstructorCompanyById(It.IsAny<Guid>())).Returns(constructorCompany);

            ConstructorCompany result = constructorCompanyLogic.GetConstructorCompanyById(id);

            constructorCompanyRepositoryMock.VerifyAll();
            Assert.AreEqual(constructorCompany, result);
        }

        [TestMethod]
        public void UpdateConstructorCompanyTestOk()
        {
            ConstructorCompany constructorCompany = new ConstructorCompany()
            {
                Id = Guid.NewGuid(),
                Name = "ConstructorCompanyId 1"
            };

            ConstructorCompanyAdministrator user = new ConstructorCompanyAdministrator()
            {
                Id = Guid.NewGuid(),
                ConstructorCompanyId = constructorCompany.Id
            };

            constructorCompanyRepositoryMock
                .Setup(c => c.UpdateConstructorCompany(It.IsAny<ConstructorCompany>()))
                .Returns(constructorCompany);
            
            userRepositoryMock
                .Setup(u => u.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>()))
                .Returns(user);
            
            constructorCompanyRepositoryMock
                .Setup(constructorCompanyRepositoryMock => constructorCompanyRepositoryMock.GetAllConstructorCompanies())
                .Returns(new List<ConstructorCompany> { new ConstructorCompany() { Id = constructorCompany.Id, Name = "asdasd" } });

            ConstructorCompany result = constructorCompanyLogic.UpdateConstructorCompany(constructorCompany, user.Id, constructorCompany.Id);

            constructorCompanyRepositoryMock.VerifyAll();

            Assert.AreEqual(constructorCompany, result);
        }

        [TestMethod]
        public void UpdateConstructorCompanyTestConstructorCompanyNotFromUser()
        {
            ConstructorCompany constructorCompany = new ConstructorCompany()
            {
                Id = Guid.NewGuid(),
                Name = "ConstructorCompanyId 1"
            };

            Guid userId = Guid.NewGuid();
            Guid constructorCompanyId = Guid.NewGuid();

            userRepositoryMock.Setup(u => u.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>()))
                .Returns(new ConstructorCompanyAdministrator()
                {
                    Id = userId,
                    ConstructorCompanyId = Guid.NewGuid()
                });

            Exception exception = null;

            try
            {
                ConstructorCompany result = constructorCompanyLogic.UpdateConstructorCompany(constructorCompany, userId, constructorCompanyId);
            }
            catch (Exception e)
            {
                exception = e;
            }

            userRepositoryMock.VerifyAll();

            Assert.IsInstanceOfType(exception, typeof(ConstructorCompanyException));
            Assert.AreEqual(exception.Message, "User is not an administrator of the constructor company");
        }

        [TestMethod]
        public void UpdateConstructorCompanyTestEmptyName()
        {
            ConstructorCompany constructorCompany = new ConstructorCompany()
            {
                Id = Guid.NewGuid(),
                Name = ""
            };

            Guid userId = Guid.NewGuid();
            Guid constructorCompanyId = Guid.NewGuid();

            userRepositoryMock.Setup(u => u.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>()))
                .Returns(new ConstructorCompanyAdministrator()
                {
                    Id = userId,
                    ConstructorCompanyId = constructorCompanyId
                });

            Exception exception = null;

            try
            {
                ConstructorCompany result = constructorCompanyLogic.UpdateConstructorCompany(constructorCompany, userId, constructorCompanyId);
            }
            catch (Exception e)
            {
                exception = e;
            }

            userRepositoryMock.VerifyAll();
            constructorCompanyRepositoryMock.VerifyAll();

            Assert.IsInstanceOfType(exception, typeof(ConstructorCompanyException));
            Assert.AreEqual(exception.Message, "Constructor company name cannot be empty");
        }

        [TestMethod]
        public void UpdateConstructorCompanyTestAlreadyExistingName()
        {
            ConstructorCompany constructorCompany = new ConstructorCompany()
            {
                Id = Guid.NewGuid(),
                Name = "ConstructorCompanyId 1"
            };

            Guid userId = Guid.NewGuid();
            Guid constructorCompanyId = Guid.NewGuid();

            userRepositoryMock.Setup(u => u.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>()))
                .Returns(new ConstructorCompanyAdministrator()
                {
                    Id = userId,
                    ConstructorCompanyId = constructorCompanyId
                });
            constructorCompanyRepositoryMock.Setup(constructorCompanyRepositoryMock => constructorCompanyRepositoryMock.GetAllConstructorCompanies()).Returns(new List<ConstructorCompany> { new ConstructorCompany() { Id = Guid.NewGuid(), Name = "ConstructorCompanyId 1" } });

            constructorCompanyRepositoryMock.Setup(c => c.GetAllConstructorCompanies()).Returns(new List<ConstructorCompany> { constructorCompany });

            Exception exception = null;

            try
            {
                ConstructorCompany result = constructorCompanyLogic.UpdateConstructorCompany(constructorCompany, userId, constructorCompanyId);
            }
            catch (Exception e)
            {
                exception = e;
            }

            constructorCompanyRepositoryMock.VerifyAll();
            userRepositoryMock.VerifyAll();

            Assert.IsInstanceOfType(exception, typeof(ConstructorCompanyException));
            Assert.AreEqual(exception.Message, "Constructor company with same name already exists");
        }

    }
}
