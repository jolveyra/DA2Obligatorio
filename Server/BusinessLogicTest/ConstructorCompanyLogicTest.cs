using Domain;
using RepositoryInterfaces;
using BusinessLogic;
using Moq;
using CustomExceptions;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace BusinessLogicTest
{
    [TestClass]
    public class ConstructorCompanyLogicTest
    {
        private Mock<IConstructorCompanyRepository> constructorCompanyRepositoryMock;
        private Mock<IConstructorCompanyAdministratorRepository> constructorCompanyAdministratorRepositoryMock;
        private ConstructorCompanyLogic constructorCompanyLogic;

        [TestInitialize]
        public void TestInitialize()
        {
            constructorCompanyRepositoryMock = new Mock<IConstructorCompanyRepository>(MockBehavior.Strict);
            constructorCompanyAdministratorRepositoryMock = new Mock<IConstructorCompanyAdministratorRepository>(MockBehavior.Strict);
            constructorCompanyLogic = new ConstructorCompanyLogic(constructorCompanyRepositoryMock.Object, constructorCompanyAdministratorRepositoryMock.Object);
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
                Name = ""
            };

            ConstructorCompanyAdministrator administrator = new ConstructorCompanyAdministrator()             {
                Id = Guid.NewGuid(),
                ConstructorCompanyId = constructorCompany.Id
            };

            ConstructorCompany expected = new ConstructorCompany()
            {
                Id = constructorCompany.Id,
                Name = "ConstructorCompany1"
            };

            constructorCompanyRepositoryMock.Setup(x => x.GetAllConstructorCompanies()).Returns(new List<ConstructorCompany> { constructorCompany });
            constructorCompanyRepositoryMock.Setup(x => x.GetConstructorCompanyById(It.IsAny<Guid>())).Returns(constructorCompany);
            constructorCompanyRepositoryMock.Setup(x => x.CreateConstructorCompany(It.IsAny<ConstructorCompany>())).Returns(expected);
            constructorCompanyAdministratorRepositoryMock.Setup(u => u.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>())).Returns(administrator);

            ConstructorCompany result = constructorCompanyLogic.CreateConstructorCompany(expected, administrator.Id);

            constructorCompanyRepositoryMock.VerifyAll();
            constructorCompanyAdministratorRepositoryMock.VerifyAll();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CreateConstructorCompanyTestNameAlreadyExists()
        {
            ConstructorCompany constructorCompany = new ConstructorCompany()
            {
                Id = Guid.NewGuid(),
                Name = ""
            };

            ConstructorCompanyAdministrator administrator = new ConstructorCompanyAdministrator()             {
                Id = Guid.NewGuid(),
                ConstructorCompanyId = constructorCompany.Id
            };
            ConstructorCompany newConstructorCompany = new ConstructorCompany()
            {
                Id = Guid.NewGuid(),
                Name = "ConstructorCompany1"
            };
            constructorCompanyRepositoryMock.Setup(x => x.GetAllConstructorCompanies()).Returns(new List<ConstructorCompany> { newConstructorCompany });
            constructorCompanyRepositoryMock.Setup(x => x.GetConstructorCompanyById(It.IsAny<Guid>())).Returns(constructorCompany);
            constructorCompanyAdministratorRepositoryMock.Setup(u => u.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>())).Returns(administrator);


            Exception exception = null;

            try
            {
                ConstructorCompany result = constructorCompanyLogic.CreateConstructorCompany(newConstructorCompany, administrator.Id);
            }
            catch (Exception e)
            {
                exception = e;
            }

            constructorCompanyRepositoryMock.VerifyAll();
            constructorCompanyAdministratorRepositoryMock.VerifyAll();

            Assert.IsInstanceOfType(exception, typeof(ConstructorCompanyException));
            Assert.AreEqual(exception.Message, "Constructor company with same name already exists");
        }

        [TestMethod]
        public void CreateConstructorCompanyTestNullName()
        {
            ConstructorCompany constructorCompany = new ConstructorCompany()
            {
                Id = Guid.NewGuid(),
                Name = ""
            };

            ConstructorCompanyAdministrator administrator = new ConstructorCompanyAdministrator()             {
                Id = Guid.NewGuid(),
                ConstructorCompanyId = constructorCompany.Id
            };

            ConstructorCompany constructorCompanyWithNoName = new ConstructorCompany()
            {
                Id = Guid.NewGuid(),
                Name = null
            };

            constructorCompanyRepositoryMock.Setup(x => x.GetConstructorCompanyById(It.IsAny<Guid>())).Returns(constructorCompany);
            constructorCompanyAdministratorRepositoryMock.Setup(u => u.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>())).Returns(administrator);

            Exception exception = null;

            try
            {
                ConstructorCompany result = constructorCompanyLogic.CreateConstructorCompany(constructorCompanyWithNoName, administrator.Id);
            }
            catch (Exception e)
            {
                exception = e;
            }

            constructorCompanyRepositoryMock.VerifyAll();
            constructorCompanyAdministratorRepositoryMock.VerifyAll();
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
            
            constructorCompanyAdministratorRepositoryMock
                .Setup(u => u.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>()))
                .Returns(user);
            
            constructorCompanyRepositoryMock.Setup(constructorCompanyRepositoryMock =>
                constructorCompanyRepositoryMock.GetConstructorCompanyById(It.IsAny<Guid>())).Returns(constructorCompany);
            constructorCompanyRepositoryMock
                .Setup(constructorCompanyRepositoryMock => constructorCompanyRepositoryMock.GetAllConstructorCompanies())
                .Returns(new List<ConstructorCompany> { new ConstructorCompany() { Id = constructorCompany.Id, Name = "asdasd" } });

            ConstructorCompany result = constructorCompanyLogic.UpdateConstructorCompany(constructorCompany.Name, user.Id, constructorCompany.Id);

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

            constructorCompanyAdministratorRepositoryMock.Setup(u => u.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>()))
                .Returns(new ConstructorCompanyAdministrator()
                {
                    Id = userId,
                    ConstructorCompanyId = Guid.NewGuid()
                });

            Exception exception = null;

            try
            {
                ConstructorCompany result = constructorCompanyLogic.UpdateConstructorCompany(constructorCompany.Name, userId, constructorCompanyId);
            }
            catch (Exception e)
            {
                exception = e;
            }

            constructorCompanyAdministratorRepositoryMock.VerifyAll();

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

            constructorCompanyAdministratorRepositoryMock.Setup(u => u.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>()))
                .Returns(new ConstructorCompanyAdministrator()
                {
                    Id = userId,
                    ConstructorCompanyId = constructorCompanyId
                });

            Exception exception = null;

            try
            {
                ConstructorCompany result = constructorCompanyLogic.UpdateConstructorCompany(constructorCompany.Name, userId, constructorCompanyId);
            }
            catch (Exception e)
            {
                exception = e;
            }

            constructorCompanyAdministratorRepositoryMock.VerifyAll();
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

            constructorCompanyAdministratorRepositoryMock.Setup(u => u.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>())).Returns(new ConstructorCompanyAdministrator()
                {
                    Id = userId,
                    ConstructorCompanyId = constructorCompanyId
                });

            constructorCompanyRepositoryMock.Setup(constructorCompanyRepositoryMock => constructorCompanyRepositoryMock.GetAllConstructorCompanies()).Returns(new List<ConstructorCompany> { new ConstructorCompany() { Id = Guid.NewGuid(), Name = "ConstructorCompanyId 1" } });

            Exception exception = null;

            try
            {
                ConstructorCompany result = constructorCompanyLogic.UpdateConstructorCompany(constructorCompany.Name, userId, constructorCompanyId);
            }
            catch (Exception e)
            {
                exception = e;
            }

            constructorCompanyRepositoryMock.VerifyAll();
            constructorCompanyAdministratorRepositoryMock.VerifyAll();

            Assert.IsInstanceOfType(exception, typeof(ConstructorCompanyException));
            Assert.AreEqual(exception.Message, "Constructor company with same name already exists");
        }

        [TestMethod]
        public void UpdateConstructorCompanyWithNameAlreadyUsedTest()
        {
            ConstructorCompany constructorCompany = new ConstructorCompany()
            {
                Id = Guid.NewGuid(),
                Name = "ConstructorCompanyId 1"
            };

            Guid userId = Guid.NewGuid();

            constructorCompanyAdministratorRepositoryMock.Setup(u => u.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>()))
                .Returns(new ConstructorCompanyAdministrator()
                {
                    Id = userId,
                    ConstructorCompanyId = constructorCompany.Id
                });
            constructorCompanyRepositoryMock.Setup(constructorCompanyRepositoryMock => constructorCompanyRepositoryMock.GetAllConstructorCompanies()).Returns(new List<ConstructorCompany>
            {
                new ConstructorCompany() { Id = Guid.NewGuid(), Name = "ConstructorCompanyId 1" },
                new ConstructorCompany() { Id = constructorCompany.Id, Name = "ConstructorCompanyId 2" },
            });

            Exception exception = null;

            try
            {
                ConstructorCompany result = constructorCompanyLogic.UpdateConstructorCompany(constructorCompany.Name, userId, constructorCompany.Id);
            }
            catch (Exception e)
            {
                exception = e;
            }

            constructorCompanyRepositoryMock.VerifyAll();
            constructorCompanyAdministratorRepositoryMock.VerifyAll();

            Assert.IsInstanceOfType(exception, typeof(ConstructorCompanyException));
            Assert.AreEqual(exception.Message, "Constructor company with same name already exists");
        }

        [TestMethod]
        public void CreateConstructorCompanyWithAdministratorAlreadyAssignedToOneTest()
        {
            ConstructorCompany constructorCompany = new ConstructorCompany()
            {
                Id = Guid.NewGuid(),
                Name = "A constructor company"
            };

            ConstructorCompanyAdministrator administrator = new ConstructorCompanyAdministrator()             {
                Id = Guid.NewGuid(),
                ConstructorCompanyId = constructorCompany.Id
            };

            ConstructorCompany companyToBeAdded = new ConstructorCompany()
            {
                Id = constructorCompany.Id,
                Name = "ConstructorCompany1"
            };

            constructorCompanyRepositoryMock.Setup(x => x.GetConstructorCompanyById(It.IsAny<Guid>())).Returns(constructorCompany);
            constructorCompanyAdministratorRepositoryMock.Setup(u => u.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>())).Returns(administrator);
            Exception exception = null;

            try
            {
                constructorCompanyLogic.CreateConstructorCompany(companyToBeAdded, administrator.Id);
            }
            catch (Exception e)
            {
                exception = e;
            }

            constructorCompanyRepositoryMock.VerifyAll();
            constructorCompanyAdministratorRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(ConstructorCompanyException));
            Assert.AreEqual(exception.Message, "Administrator already belongs to a constructor company");
        }
    }
}
