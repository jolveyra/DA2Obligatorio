using Domain;
using BusinessLogic;
using RepositoryInterfaces;
using Moq;
using RepositoryInterfaces;

namespace BusinessLogicTest
{
    [TestClass]
    public class UserLogicTest
    {

        private Mock<ITokenRepository> tokenRepositoryMock;
        private Mock<IUserRepository> userRepositoryMock;
        private UserLogic userLogic;

        [TestInitialize]
        public void TestInitialize()
        {
            userRepositoryMock = new Mock<IUserRepository>();
            tokenRepositoryMock = new Mock<ITokenRepository>();
            userLogic = new UserLogic(userRepositoryMock.Object, tokenRepositoryMock.Object);
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
            
            IEnumerable<User> result = userLogic.GetAllAdministrators();

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
            string result = userLogic.GetUserRoleByToken(It.IsAny<Guid>());

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

            IEnumerable<User> result = userLogic.GetAllMaintenanceEmployees();
            
            userRepositoryMock.VerifyAll();
            Assert.IsTrue(result.Count() == 1 && result.First().Equals(users.Last()));
        }
    }
}