using BusinessLogic;
using Domain;
using Moq;
using RepositoryInterfaces;

namespace BusinessLogicTest
{
    [TestClass]
    public class UserLogicTest
    {
        private Mock<IUserRepository> userRepositoryMock;
        private UserLogic userLogic;

        [TestInitialize]
        public void TestInitialize()
        {
            userRepositoryMock = new Mock<IUserRepository>();
            userLogic = new UserLogic(userRepositoryMock.Object);
        }

        [TestMethod]
        public void GetAllAdministratorsTest()
        {
            var users = new List<User>
            {
                new User { Role = Role.Administrator },
                new User { Role = Role.MaintenanceEmployee }
            };

            userRepositoryMock.Setup(u => u.GetAllUsers()).Returns(users);
            
            var result = userLogic.GetAllAdministrators();

            Assert.IsTrue(result.Count() == 1 && result.First().Equals(users.First()));
        }

        [TestMethod]
        public void GetAllMaintenanceEmployeesTest()
        {
            var users = new List<User>
            {
                new User { Role = Role.Administrator },
                new User { Role = Role.MaintenanceEmployee }
            };

            userRepositoryMock.Setup(u => u.GetAllUsers()).Returns(users);
            
            var result = userLogic.GetAllMaintenanceEmployees();

            Assert.IsTrue(result.Count() == 1 && result.First().Equals(users.Last()));
        }
    }
}
