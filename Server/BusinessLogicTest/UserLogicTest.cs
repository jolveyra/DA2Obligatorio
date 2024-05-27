using Domain;
using BusinessLogic;
using RepositoryInterfaces;
using Moq;
using CustomExceptions;

namespace BusinessLogicTest
{
    [TestClass]
    public class UserLogicTest
    {

        private Mock<ISessionRepository> sessionRepositoryMock;
        private Mock<IUserRepository> userRepositoryMock;
        private UserLogic _userLogic;

        [TestInitialize]
        public void TestInitialize()
        {
            userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            sessionRepositoryMock = new Mock<ISessionRepository>(MockBehavior.Strict);
            _userLogic = new UserLogic(userRepositoryMock.Object, sessionRepositoryMock.Object);
        }

        [TestMethod]
        public void GetAllAdministratorsTest()
        {
            IEnumerable<User> users = new List<User>
            {
                new User { Role = Role.Administrator },
                new User { Role = Role.MaintenanceEmployee }
            };

            userRepositoryMock.Setup(u => u.GetAllUsers()).Returns(users);
            
            IEnumerable<User> result = _userLogic.GetAllAdministrators();

            userRepositoryMock.VerifyAll();
            Assert.IsTrue(result.Count() == 1 && result.First().Equals(users.First()));
        }
        
        [TestMethod]
        public void GetAllMaintenanceEmployeesTest()
        {
            IEnumerable<User> users = new List<User>
            {
                new User { Role = Role.Administrator },
                new User { Role = Role.MaintenanceEmployee }
            };

            userRepositoryMock.Setup(u => u.GetAllUsers()).Returns(users);

            IEnumerable<User> result = _userLogic.GetAllMaintenanceEmployees();
            
            userRepositoryMock.VerifyAll();
            Assert.IsTrue(result.Count() == 1 && result.First().Equals(users.Last()));
        }

        [TestMethod]
        public void CreateAdministratorTest()
        {
            User user = new User { Name = "Juan", Surname = "Perez", Email = "juan@gmail.com", Password = "Juan1234", Role = Role.Administrator };
            Session session = new Session() { UserId = user.Id };

            userRepositoryMock.Setup(u => u.GetAllUsers()).Returns(new List<User>());
            userRepositoryMock.Setup(u => u.CreateUser(It.IsAny<User>())).Returns(user);
            sessionRepositoryMock.Setup(s => s.CreateSession(It.IsAny<Session>())).Returns(session);

            user.Id = Guid.NewGuid();
            User result = _userLogic.CreateAdministrator(user);

            userRepositoryMock.VerifyAll();
            Assert.AreEqual(user, result);
        }

        [TestMethod]
        public void CreateMaintenanceEmployeeTest()
        {
            User user = new User { Name = "Juan", Surname = "Perez", Email = "juan@gmail.com", Password = "Juan1234", Role = Role.MaintenanceEmployee };
            Session session = new Session() { UserId = user.Id };

            userRepositoryMock.Setup(u => u.GetAllUsers()).Returns(new List<User>());
            userRepositoryMock.Setup(u => u.CreateUser(It.IsAny<User>())).Returns(user);
            sessionRepositoryMock.Setup(s => s.CreateSession(It.IsAny<Session>())).Returns(session);

            user.Id = Guid.NewGuid();
            User result = _userLogic.CreateMaintenanceEmployee(user);

            userRepositoryMock.VerifyAll();
            Assert.AreEqual(user, result);
        }

        [TestMethod]
        public void CreateAdministratorWithExistingEmailTest()
        {
            User user = new User { Name = "Juan", Surname = "Perez", Email = "juan@gmail.com", Password = "Juan1234", Role = Role.Administrator };
            IEnumerable<User> users = new List<User> { user };

            userRepositoryMock.Setup(u => u.GetAllUsers()).Returns(users);
            Exception exception = null;

            try
            {
                _userLogic.CreateAdministrator(user);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(UserException));
            Assert.IsTrue(exception.Message.Equals("A user with the same email already exists"));
        }

        [TestMethod]
        public void ValidateUserTest()
        {
            User user = new User { Name = "Juan", Surname = "Perez", Email = "juan@gmail.com", Password = "Juan1234", Role = Role.Administrator };

            UserLogic.ValidateUser(user);
        }
        
        [TestMethod]
        public void ValidateUserWithNoAtInEmailTest()
        {
            User user = new User { Name = "Juan", Surname = "Perez", Email = "juangmail.com", Password = "Juan1234", Role = Role.Administrator }; 
            Exception exception = null;

            try
            {
                UserLogic.ValidateUser(user);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(UserException));
            Assert.IsTrue(exception.Message.Equals("An Email must contain '@', '.' and be longer than 4 characters long"));
        }

        [TestMethod]
        public void ValidateUserWithNoDotInEmailTest()
        {
            User user = new User { Name = "Juan", Surname = "Perez", Email = "juan@gmailcom", Password = "Juan1234", Role = Role.Administrator }; 
            Exception exception = null;

            try
            {
                UserLogic.ValidateUser(user);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(UserException));
            Assert.IsTrue(exception.Message.Equals("An Email must contain '@', '.' and be longer than 4 characters long"));
        }

        [TestMethod]
        public void ValidateUserWithShortEmailTest()
        {
            User user = new User { Name = "Juan", Surname = "Perez", Email = "@.", Password = "Juan1234", Role = Role.Administrator }; 
            Exception exception = null;

            try
            {
                UserLogic.ValidateUser(user);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(UserException));
            Assert.IsTrue(exception.Message.Equals("An Email must contain '@', '.' and be longer than 4 characters long"));
        }

        [TestMethod]
        public void ValidateUserWithNoPasswordTest()
        {
            User user = new User { Name = "Juan", Surname = "Perez", Email = "juan@gmail.com", Password = "", Role = Role.Administrator };
            Exception exception = null;

            try
            {
                UserLogic.ValidateUser(user);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(UserException));
            Assert.IsTrue(exception.Message.Equals("A Password must an uppercase and lowercase letter, a number and must be longer than 5 characters long"));
        }

        [TestMethod]
        public void ValidateUserWithNoUpperCaseCharacterInPasswordTest()
        {
            User user = new User { Name = "Juan", Surname = "Perez", Email = "juan@gmail.com", Password = "juan1234", Role = Role.Administrator };
            Exception exception = null;

            try
            {
                UserLogic.ValidateUser(user);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(UserException));
            Assert.IsTrue(exception.Message.Equals("A Password must an uppercase and lowercase letter, a number and must be longer than 5 characters long"));
        }

        [TestMethod]
        public void ValidateUserWithNoLowerCaseCharacterInPasswordTest()
        {
            User user = new User { Name = "Juan", Surname = "Perez", Email = "juan@gmail.com", Password = "JUAN1234", Role = Role.Administrator };
            Exception exception = null;

            try
            {
                UserLogic.ValidateUser(user);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(UserException));  
            Assert.IsTrue(exception.Message.Equals("A Password must an uppercase and lowercase letter, a number and must be longer than 5 characters long"));
        }

        [TestMethod]
        public void ValidateUserWithNoNumberInPasswordTest()
        {
            User user = new User { Name = "Juan", Surname = "Perez", Email = "juan@gmail.com", Password = "juanJUAN", Role = Role.Administrator };
            Exception exception = null;

            try
            {
                UserLogic.ValidateUser(user);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(UserException));
            Assert.IsTrue(exception.Message.Equals("A Password must an uppercase and lowercase letter, a number and must be longer than 5 characters long"));
        }

        [TestMethod]
        public void ValidateUserWithNoNameTest()
        {
            User user = new User { Name = "", Surname = "Perez", Email = "juan@gmail.com", Password = "Juan1234", Role = Role.Administrator };
            Exception exception = null;

            try
            {
                UserLogic.ValidateUser(user);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(UserException));
            Assert.IsTrue(exception.Message.Equals("The Name field cannot be empty"));
        }

        [TestMethod]
        public void ValidateUserWithNoSurnameTest()
        {
            User user = new User { Name = "Juan", Surname = "", Email = "juan@gmail.com", Password = "Juan1234", Role = Role.Administrator };
            Exception exception = null;

            try
            {
                UserLogic.ValidateUser(user);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(UserException));
            Assert.IsTrue(exception.Message.Equals("The Surname field cannot be empty for non manager users"));
        }

        [TestMethod]
        public void CreateMaintenanceEmployeeWithExistingEmailTest()
        {
            User user = new User { Name = "Juan", Surname = "Perez", Email = "juan@gmail.com", Password = "Juan1234", Role = Role.MaintenanceEmployee };
            IEnumerable<User> users = new List<User> { user };

            userRepositoryMock.Setup(u => u.GetAllUsers()).Returns(users);
            Exception exception = null;

            try
            {
                _userLogic.CreateAdministrator(user);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(UserException));
            Assert.IsTrue(exception.Message.Equals("A user with the same email already exists"));
        }

        [TestMethod]
        public void GetUserByIdTest()
        {
            Guid userId = Guid.NewGuid();
            User user = new User { Id = userId, Name = "Juan", Surname = "Perez", Email = "juan@gmail.com", Password = "JuanPerez123"};

            userRepositoryMock.Setup(u => u.GetUserById(It.IsAny<Guid>())).Returns(user);

            User result = _userLogic.GetUserById(userId);

            userRepositoryMock.VerifyAll();
            Assert.AreEqual(user, result);
        }

        [TestMethod]
        public void UpdateUserSettingsTest()
        {
            Guid userId = Guid.NewGuid();
            User userParameter = new User() { Name = "Juan1", Surname = "Perez1", Password = "Juan123"};
            User user = new User() { Id = userId, Name = "Juan", Surname = "Perez", Email = "juan@gmail.com", Password = "JuanPerez123"};
            User expected = new User() { Id = userId, Name = "Juan1", Surname = "Perez1", Email = "juan@gmail.com", Password = "Juan123"};

            userRepositoryMock.Setup(u => u.GetUserById(It.IsAny<Guid>())).Returns(user);
            userRepositoryMock.Setup(u => u.UpdateUser(It.IsAny<User>())).Returns(expected);

            User result = _userLogic.UpdateUserSettings(userId, user);

            userRepositoryMock.VerifyAll();
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void GetSessionByUserIdTest()
        {
            Guid userId = Guid.NewGuid();
            Guid token = Guid.NewGuid();
            Session session = new Session() { Id = token, UserId = userId };

            IEnumerable<User> users = new List<User> { new User { Id = userId, Email = "juan@gmail.com", Password = "Juan1234" } };

            userRepositoryMock.Setup(u => u.GetAllUsers()).Returns(users);
            sessionRepositoryMock.Setup(s => s.GetSessionByUserId(It.IsAny<Guid>())).Returns(session);

            UserLogged result = _userLogic.Login(users.First());

            sessionRepositoryMock.VerifyAll();
            Assert.AreEqual(token, result.Token);
        }

        [TestMethod]
        public void CreateManagerTest()
        {
            User user = new User { Name = "Juan", Email = "juan@gmail.com", Password = Invitation.DefaultPassword };
            User expected = new User { Id = Guid.NewGuid(), Name = "Juan", Email = "juan@gmail.com", Role = Role.Manager, Password = Invitation.DefaultPassword };

            userRepositoryMock.Setup(u => u.GetAllUsers()).Returns(new List<User>());
            userRepositoryMock.Setup(u => u.CreateUser(It.IsAny<User>())).Returns(expected);
            sessionRepositoryMock.Setup(repository => repository.CreateSession(It.IsAny<Session>())).Returns(new Session());

            User result = UserLogic.CreateManager(userRepositoryMock.Object, sessionRepositoryMock.Object, user);

            userRepositoryMock.VerifyAll();
            sessionRepositoryMock.VerifyAll();
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void GetAllManagersTest()
        {
            IEnumerable<User> users = new List<User>
            {
                new User { Role = Role.Manager },
                new User { Role = Role.MaintenanceEmployee }
            };

            userRepositoryMock.Setup(u => u.GetAllUsers()).Returns(users);
            
            IEnumerable<User> result = _userLogic.GetAllManagers();

            userRepositoryMock.VerifyAll();
            Assert.IsTrue(result.Count() == 1 && result.First().Equals(users.First()));
        }

        [TestMethod]
        public void LoginTestInvalidPassword()
        {
            Guid userId = Guid.NewGuid();
            Guid token = Guid.NewGuid();
            Session session = new Session() { Id = token, UserId = userId };

            IEnumerable<User> users = new List<User> { new User { Id = userId, Email = "juan@gmail.com", Password = "Juan1234" } };

            userRepositoryMock.Setup(u => u.GetAllUsers()).Returns(new List<User> { new User { Id = userId, Email = "juan@gmail.com", Password = "Juan84" } });

            try
            {
                UserLogged result = _userLogic.Login(users.First());
            }catch(Exception e)
            {
                sessionRepositoryMock.VerifyAll();
                Assert.IsInstanceOfType(e, typeof(UserException));
                Assert.AreEqual("Invalid email or password", e.Message);
            }
        }

        [TestMethod]
        public void CreateConstructorCompanyAdminTest()
        {
            User user = new User { Name = "Juan", Surname = "Perez", Email = "juan@gmail.com", Password = "Juan1234", Role = Role.ConstructorCompanyAdmin };
            Session session = new Session() { UserId = user.Id };

            userRepositoryMock.Setup(u => u.GetAllUsers()).Returns(new List<User>());
            userRepositoryMock.Setup(u => u.CreateUser(It.IsAny<User>())).Returns(user);
            sessionRepositoryMock.Setup(s => s.CreateSession(It.IsAny<Session>())).Returns(session);

            user.Id = Guid.NewGuid();
            User result = _userLogic.CreateAdministrator(user);

            userRepositoryMock.VerifyAll();
            Assert.AreEqual(user, result);
        }
    }
}