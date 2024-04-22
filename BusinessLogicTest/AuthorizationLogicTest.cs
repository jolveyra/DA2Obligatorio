using Domain;
using BusinessLogic;
using RepositoryInterfaces;
using Moq;
using RepositoryInterfaces;

namespace BusinessLogicTest
{
    [TestClass]
    public class AuthorizationLogicTest
    {

        private Mock<ITokenRepository> tokenRepositoryMock;
        private Mock<IUserRepository> userRepositoryMock;
        private AuthorizationLogic _authorizationLogic;

        [TestInitialize]
        public void TestInitialize()
        {
            userRepositoryMock = new Mock<IUserRepository>();
            tokenRepositoryMock = new Mock<ITokenRepository>();
            _authorizationLogic = new AuthorizationLogic(userRepositoryMock.Object, tokenRepositoryMock.Object);
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
            
            IEnumerable<User> result = _authorizationLogic.GetAllAdministrators();

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
            string result = _authorizationLogic.GetUserRoleByToken(It.IsAny<Guid>());

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

            IEnumerable<User> result = _authorizationLogic.GetAllMaintenanceEmployees();
            
            userRepositoryMock.VerifyAll();
            Assert.IsTrue(result.Count() == 1 && result.First().Equals(users.Last()));
        }
    }
}