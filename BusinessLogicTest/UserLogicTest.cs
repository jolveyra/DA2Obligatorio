using Domain;
using BusinessLogic;
using RepositoryInterfaces;
using Moq;
using CustomExceptions.BusinessLogic;

namespace BusinessLogicTest
{
    [TestClass]
    public class UserLogicTest
    {

        private Mock<ITokenRepository> tokenRepositoryMock;
        private Mock<IUserRepository> userRepositoryMock;
        private UserLogic _userLogic;

        [TestInitialize]
        public void TestInitialize()
        {
            userRepositoryMock = new Mock<IUserRepository>();
            tokenRepositoryMock = new Mock<ITokenRepository>();
            _userLogic = new UserLogic(userRepositoryMock.Object, tokenRepositoryMock.Object);
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
        public void GetUserRoleTestOk()
        {
            User user = new User() { Role = Role.Administrator };
            Guid id = user.Id;

            tokenRepositoryMock.Setup(x => x.GetUserIdByToken(It.IsAny<Guid>())).Returns(id);
            userRepositoryMock.Setup(x => x.GetUserById(It.IsAny<Guid>())).Returns(user);

            string expected = user.Role.ToString();
            string result = _userLogic.GetUserRoleByToken(It.IsAny<Guid>());

            tokenRepositoryMock.VerifyAll();
            userRepositoryMock.VerifyAll();
            Assert.AreEqual(expected, result);
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
            
            userRepositoryMock.Setup(u => u.CreateUser(It.IsAny<User>())).Returns(user);

            user.Id = Guid.NewGuid();
            User result = _userLogic.CreateAdministrator(user);

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

            _userLogic.ValidateUser(user);
        }
        
        [TestMethod]
        public void ValidateUserWithNoAtInEmailTest()
        {
            User user = new User { Name = "Juan", Surname = "Perez", Email = "juangmail.com", Password = "Juan1234", Role = Role.Administrator }; 
            Exception exception = null;

            try
            {
                _userLogic.ValidateUser(user);
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
                _userLogic.ValidateUser(user);
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
            User user = new User { Name = "Juan", Surname = "Perez", Email = "juan@gmailcom", Password = "Juan1234", Role = Role.Administrator }; 
            Exception exception = null;

            try
            {
                _userLogic.ValidateUser(user);
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
                _userLogic.ValidateUser(user);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(UserException));
            Assert.IsTrue(exception.Message.Equals("A Password must an uppercase and lowercase letter, a number and must be longer than 7 characters long"));
        }

        [TestMethod]
        public void ValidateUserWithNoUpperCaseCharacterInPasswordTest()
        {
            User user = new User { Name = "Juan", Surname = "Perez", Email = "juan@gmail.com", Password = "juan1234", Role = Role.Administrator };
            Exception exception = null;

            try
            {
                _userLogic.ValidateUser(user);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(UserException));
            Assert.IsTrue(exception.Message.Equals("A Password must an uppercase and lowercase letter, a number and must be longer than 7 characters long"));
        }

        [TestMethod]
        public void ValidateUserWithNoLowerCaseCharacterInPasswordTest()
        {
            User user = new User { Name = "Juan", Surname = "Perez", Email = "juan@gmail.com", Password = "JUAN1234", Role = Role.Administrator };
            Exception exception = null;

            try
            {
                _userLogic.ValidateUser(user);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(UserException));
            Assert.IsTrue(exception.Message.Equals("A Password must an uppercase and lowercase letter, a number and must be longer than 7 characters long"));
        }

        [TestMethod]
        public void ValidateUserWithNoNumberInPasswordTest()
        {
            User user = new User { Name = "Juan", Surname = "Perez", Email = "juan@gmail.com", Password = "juanJUAN", Role = Role.Administrator };
            Exception exception = null;

            try
            {
                _userLogic.ValidateUser(user);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(UserException));
            Assert.IsTrue(exception.Message.Equals("A Password must an uppercase and lowercase letter, a number and must be longer than 7 characters long"));
        }

        [TestMethod]
        public void ValidateUserWithNoNameTest()
        {
            User user = new User { Name = "", Surname = "Perez", Email = "juan@gmail.com", Password = "Juan1234", Role = Role.Administrator };
            Exception exception = null;

            try
            {
                _userLogic.ValidateUser(user);
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
                _userLogic.ValidateUser(user);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(UserException));
            Assert.IsTrue(exception.Message.Equals("The Surname field cannot be empty"));
        }
        
        [TestMethod]
        public void CreateMaintenanceEmployeeTest()
        {
            User user = new User { Name = "Juan", Surname = "Perez", Email = "juan@gmail.com", Password = "Juan1234", Role = Role.MaintenanceEmployee };
            
            userRepositoryMock.Setup(u => u.CreateUser(It.IsAny<User>())).Returns(user);

            user.Id = Guid.NewGuid();
            User result = _userLogic.CreateAdministrator(user);

            userRepositoryMock.VerifyAll();
            Assert.AreEqual(user, result);
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
    }
}