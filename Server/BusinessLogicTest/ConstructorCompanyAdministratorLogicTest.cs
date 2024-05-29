using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            constructorCompanyAdministratorLogic = new ConstructorCompanyAdministratorLogic(constructorCompanyAdministratorRepositoryMock.Object, constructorCompanyRepositoryMock.Object, userRepositoryMock.Object);
        }

        [TestMethod]
        public void SetConstructorCompanyAdministratorTestOk()
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
                ConstructorCompany = constructorCompany
            };


            userRepositoryMock.Setup(c => c.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>())).Returns(constructorCompanyAdministrator);
            constructorCompanyRepositoryMock.Setup(c => c.GetConstructorCompanyById(It.IsAny<Guid>())).Returns(constructorCompany);
            constructorCompanyAdministratorRepositoryMock.Setup(c => c.SetConstructorCompanyAdministrator(It.IsAny<ConstructorCompanyAdministrator>())).Returns(resultConstructorCompanyAdministrator);

            ConstructorCompanyAdministrator result = constructorCompanyAdministratorLogic.SetConstructorCompanyAdministrator(userId, constructorCompanyId);

            constructorCompanyAdministratorRepositoryMock.VerifyAll();
            Assert.AreEqual(constructorCompanyAdministrator, result);
        }

        [TestMethod]
        public void SetConstructorCompanyAdministratorTestUserAlreadyHasAConstructorCompany()
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
                ConstructorCompany = new ConstructorCompany() { Id = Guid.NewGuid() }
            };

            userRepositoryMock.Setup(c => c.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>())).Returns(constructorCompanyAdministrator);

            Exception exception = null;

            try
            {
                ConstructorCompanyAdministrator result = constructorCompanyAdministratorLogic.SetConstructorCompanyAdministrator(userId, constructorCompanyId);
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
        public void GetAdminConstructorCompanyTestOk()
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
                ConstructorCompany = constructorCompany
            };

            userRepositoryMock.Setup(c => c.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>())).Returns(constructorCompanyAdministrator);

            ConstructorCompany result = constructorCompanyAdministratorLogic.GetAdminConstructorCompany(userId);

            Assert.AreEqual(constructorCompany, result);

        }

        [TestMethod]
        public void GetAdminConstructorCompanyTestUserDoesNotHaveAConstructorCompany()
        {
            Guid userId = Guid.NewGuid();

            ConstructorCompanyAdministrator constructorCompanyAdministrator = new ConstructorCompanyAdministrator()
            {
                Id = Guid.NewGuid(),
                Name = "Name",
                Surname = "Surname",
                Email = "mail@mail.com",
                Password = "passworD123",
                Role = Role.ConstructorCompanyAdmin
            };

            userRepositoryMock.Setup(c => c.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>())).Returns(constructorCompanyAdministrator);

            Exception exception = null;

            try
            {
                ConstructorCompany result = constructorCompanyAdministratorLogic.GetAdminConstructorCompany(userId);
            }
            catch (Exception e)
            {
                exception = e;
            }

            constructorCompanyAdministratorRepositoryMock.VerifyAll();

            Assert.IsInstanceOfType(exception, typeof(ConstructorCompanyAdministratorException));
            Assert.AreEqual(exception.Message, "Administrator is not a member from a constructor company");
        }
    }
}
