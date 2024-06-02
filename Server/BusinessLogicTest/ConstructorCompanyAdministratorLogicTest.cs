using Moq;
using BusinessLogic;
using RepositoryInterfaces;
using Domain;
using CustomExceptions;

namespace BusinessLogicTest
{
    [TestClass]
    public class ConstructorCompanyAdministratorLogicTest
    {
        Mock<IConstructorCompanyAdministratorRepository> constructorCompanyAdministratorRepositoryMock;
        Mock<IConstructorCompanyRepository> constructorCompanyRepositoryMock;
        Mock<IUserRepository> userRepositoryMock;
        ConstructorCompanyAdministratorLogic constructorCompanyAdministratorLogic;

        [TestInitialize]
        public void TestInitialize()
        {
            constructorCompanyAdministratorRepositoryMock = new Mock<IConstructorCompanyAdministratorRepository>(MockBehavior.Strict);
            constructorCompanyRepositoryMock = new Mock<IConstructorCompanyRepository>(MockBehavior.Strict);
            userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            constructorCompanyAdministratorLogic = new ConstructorCompanyAdministratorLogic(userRepositoryMock.Object,  constructorCompanyRepositoryMock.Object, constructorCompanyAdministratorRepositoryMock.Object);
        }

        [TestMethod]
        public void UpdateConstructorCompanyAdministratorTestOk()
        {
            Guid userId = Guid.NewGuid();
            Guid constructorCompanyId = Guid.NewGuid();

            ConstructorCompany constructorCompany = new ConstructorCompany()
            {
                Id = constructorCompanyId
            };

            ConstructorCompanyAdministrator constructorCompanyAdministrator = new ConstructorCompanyAdministrator()
            {
                Id = Guid.NewGuid(),
                Name = "Name",
                Surname = "Surname",
                Email = "email@mail.com",
                Password = "passworD123",
                Role = Role.ConstructorCompanyAdmin
            };

            ConstructorCompanyAdministrator resultConstructorCompanyAdministrator = new ConstructorCompanyAdministrator()
            {
                Id = constructorCompanyAdministrator.Id,
                Name = constructorCompanyAdministrator.Name,
                Surname = constructorCompanyAdministrator.Surname,
                Email = constructorCompanyAdministrator.Email,
                Password = constructorCompanyAdministrator.Password,
                Role = constructorCompanyAdministrator.Role,
                ConstructorCompanyId = constructorCompany.Id
            };


            userRepositoryMock.Setup(c => c.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>())).Returns(constructorCompanyAdministrator);
            constructorCompanyRepositoryMock.Setup(c => c.GetConstructorCompanyById(It.IsAny<Guid>())).Returns(constructorCompany);
            constructorCompanyAdministratorRepositoryMock.Setup(c => c.UpdateConstructorCompanyAdministrator(It.IsAny<ConstructorCompanyAdministrator>())).Returns(resultConstructorCompanyAdministrator);

            ConstructorCompanyAdministrator result = constructorCompanyAdministratorLogic.UpdateConstructorCompanyAdministrator(userId, constructorCompanyId);

            constructorCompanyAdministratorRepositoryMock.VerifyAll();
            Assert.AreEqual(constructorCompanyAdministrator, result);
        }

        [TestMethod]
        public void UpdateConstructorCompanyAdministratorTestUserAlreadyHasAConstructorCompany()
        {
            Guid userId = Guid.NewGuid();
            Guid constructorCompanyId = Guid.NewGuid();

            ConstructorCompany constructorCompany = new ConstructorCompany()
            {
                Id = constructorCompanyId
            };

            ConstructorCompanyAdministrator constructorCompanyAdministrator = new ConstructorCompanyAdministrator()
            {
                Id = Guid.NewGuid(),
                Name = "Name",
                Surname = "Surname",
                Email = "email@mail.com",
                Password = "passworD123",
                Role = Role.ConstructorCompanyAdmin,
                ConstructorCompanyId = Guid.NewGuid()
            };

            userRepositoryMock.Setup(c => c.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>())).Returns(constructorCompanyAdministrator);
            constructorCompanyRepositoryMock.Setup(r => r.GetConstructorCompanyById(It.IsAny<Guid>())).Returns(constructorCompany);

            Exception exception = null;

            try
            {
                ConstructorCompanyAdministrator result = constructorCompanyAdministratorLogic.UpdateConstructorCompanyAdministrator(userId, constructorCompanyId);
            }
            catch (Exception e)
            {
                exception = e;
            }

            constructorCompanyAdministratorRepositoryMock.VerifyAll();

            Assert.IsInstanceOfType(exception, typeof(ConstructorCompanyAdministratorException));
            Assert.AreEqual(exception.Message, "Administrator is already a member from a constructor company");

        }

        [TestMethod]
        public void GetConstructorCompanyAdministratorTestOk()
        {
            Guid userId = Guid.NewGuid();
            Guid constructorCompanyId = Guid.NewGuid();

            ConstructorCompany constructorCompany = new ConstructorCompany()
            {
                Id = constructorCompanyId
            };

            ConstructorCompanyAdministrator constructorCompanyAdministrator = new ConstructorCompanyAdministrator()
            {
                Id = Guid.NewGuid(),
                Name = "Name",
                Surname = "Surname",
                Email = "mail@mail.com",
                Password = "passworD123",
                Role = Role.ConstructorCompanyAdmin,
                ConstructorCompanyId = constructorCompany.Id
            };

            userRepositoryMock.Setup(c => c.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>())).Returns(constructorCompanyAdministrator);

            ConstructorCompanyAdministrator result = constructorCompanyAdministratorLogic.GetConstructorCompanyAdministrator(userId);

            Assert.AreEqual(constructorCompanyAdministrator, result);

        }
    }
}
