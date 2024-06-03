using Moq;
using BusinessLogic;
using RepositoryInterfaces;
using Domain;
using CustomExceptions;
using LogicInterfaces;

namespace BusinessLogicTest
{
    [TestClass]
    public class ConstructorCompanyAdministratorLogicTest
    {
        Mock<IConstructorCompanyAdministratorRepository> constructorCompanyAdministratorRepositoryMock;
        Mock<IConstructorCompanyRepository> constructorCompanyRepositoryMock;
        Mock<ISessionRepository> sessionRepositoryMock;
        Mock<IUserRepository> userRepositoryMock;
        ConstructorCompanyAdministratorLogic constructorCompanyAdministratorLogic;

        [TestInitialize]
        public void TestInitialize()
        {
            constructorCompanyAdministratorRepositoryMock = new Mock<IConstructorCompanyAdministratorRepository>(MockBehavior.Strict);
            constructorCompanyRepositoryMock = new Mock<IConstructorCompanyRepository>(MockBehavior.Strict);
            sessionRepositoryMock = new Mock<ISessionRepository>(MockBehavior.Strict);
            userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            constructorCompanyAdministratorLogic = new ConstructorCompanyAdministratorLogic(constructorCompanyRepositoryMock.Object, constructorCompanyAdministratorRepositoryMock.Object);
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


            constructorCompanyAdministratorRepositoryMock.Setup(c => c.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>())).Returns(constructorCompanyAdministrator);
            constructorCompanyRepositoryMock.Setup(c => c.GetConstructorCompanyById(It.IsAny<Guid>())).Returns(constructorCompany);
            constructorCompanyRepositoryMock.Setup(c => c.DeleteConstructorCompany(It.IsAny<ConstructorCompany>()));
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

            ConstructorCompany adminConstructorCompany = new ConstructorCompany()
            {
                Id = Guid.NewGuid(),
                Name = "A Name"
            };

            ConstructorCompanyAdministrator constructorCompanyAdministrator = new ConstructorCompanyAdministrator()
            {
                Id = Guid.NewGuid(),
                Name = "Name",
                Surname = "Surname",
                Email = "email@mail.com",
                Password = "passworD123",
                Role = Role.ConstructorCompanyAdmin,
                ConstructorCompany = adminConstructorCompany,
                ConstructorCompanyId = adminConstructorCompany.Id
            };

            constructorCompanyAdministratorRepositoryMock.Setup(c => c.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>())).Returns(constructorCompanyAdministrator);
            constructorCompanyRepositoryMock.Setup(c => c.GetConstructorCompanyById(It.IsAny<Guid>())).Returns(adminConstructorCompany);
            
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
            constructorCompanyRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(ConstructorCompanyAdministratorException));
            Assert.AreEqual(exception.Message, "Administrator already belongs to a constructor company");

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

            constructorCompanyAdministratorRepositoryMock.Setup(c => c.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>())).Returns(constructorCompanyAdministrator);

            ConstructorCompanyAdministrator result = constructorCompanyAdministratorLogic.GetConstructorCompanyAdministrator(userId);

            Assert.AreEqual(constructorCompanyAdministrator, result);

        }



        [TestMethod]
        public void GetConstructorCompanyByUserIdTestOk()
        {
            Guid userId = Guid.NewGuid();
            ConstructorCompanyAdministrator user = new ConstructorCompanyAdministrator
            {
                Id = userId,
                Name = "Juan",
                Surname = "Perez",
                Email = "",
                Role = Role.ConstructorCompanyAdmin,
                ConstructorCompanyId = Guid.NewGuid()
            };

            constructorCompanyAdministratorRepositoryMock.Setup(u => u.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>())).Returns(user);

            Guid result = constructorCompanyAdministratorLogic.GetConstructorCompanyByUserId(userId);

            constructorCompanyAdministratorRepositoryMock.VerifyAll();

            Assert.AreEqual(user.ConstructorCompanyId, result);

        }



        [TestMethod]
        public void CreateConstructorCompanyAdminTest()
        {
            ConstructorCompanyAdministrator user = new ConstructorCompanyAdministrator { Name = "Juan", Surname = "Perez", Email = "juan@gmail.com", Password = "Juan1234", Role = Role.ConstructorCompanyAdmin, };
            Session session = new Session() { UserId = user.Id };

            constructorCompanyAdministratorRepositoryMock.Setup(u => u.GetAllConstructorCompanyAdministrators()).Returns(new List<ConstructorCompanyAdministrator>());
            constructorCompanyAdministratorRepositoryMock.Setup(u => u.CreateConstructorCompanyAdministrator(It.IsAny<ConstructorCompanyAdministrator>())).Returns(user);
            sessionRepositoryMock.Setup(s => s.CreateSession(It.IsAny<Session>())).Returns(session);
            userRepositoryMock.Setup(u => u.GetAllUsers()).Returns(new List<User>());

            user.Id = Guid.NewGuid();
            ConstructorCompanyAdministrator result = ConstructorCompanyAdministratorLogic.CreateConstructorCompanyAdmin(userRepositoryMock.Object, sessionRepositoryMock.Object, constructorCompanyAdministratorRepositoryMock.Object, user);

            constructorCompanyAdministratorRepositoryMock.VerifyAll();
            sessionRepositoryMock.VerifyAll();
            userRepositoryMock.VerifyAll();
            Assert.AreEqual(user, result);
        }

        [TestMethod]
        public void CreateConstructorCompanyAdminWithAlreadyExistingEmailTest()
        {
            ConstructorCompanyAdministrator user = new ConstructorCompanyAdministrator { Name = "Juan", Surname = "Perez", Email = "juan@gmail.com", Password = "Juan1234", Role = Role.ConstructorCompanyAdmin, };
            Session session = new Session() { UserId = user.Id };

            constructorCompanyAdministratorRepositoryMock.Setup(u => u.GetAllConstructorCompanyAdministrators()).Returns(new List<ConstructorCompanyAdministrator>() { new ConstructorCompanyAdministrator() { Email = "juan@gmail.com" } });
            userRepositoryMock.Setup(u => u.GetAllUsers()).Returns(new List<User>());

            user.Id = Guid.NewGuid();

            Exception exception = null;

            try
            {
                ConstructorCompanyAdministrator result = ConstructorCompanyAdministratorLogic.CreateConstructorCompanyAdmin(userRepositoryMock.Object, sessionRepositoryMock.Object, constructorCompanyAdministratorRepositoryMock.Object, user);
            }
            catch (Exception e)
            {
                exception = e;
            }

            constructorCompanyAdministratorRepositoryMock.VerifyAll();
            userRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(UserException));
            Assert.IsTrue(exception.Message.Equals("A user with the same email already exists"));
        }
    }
}
