using BusinessLogic;
using Domain;
using Moq;
using RepositoryInterfaces;

namespace BusinessLogicTest
{
    [TestClass]
    public class UserLogicTest
    {
        [TestMethod]
        public void GetAllAdministratorsTest()
        {
            var users = new List<User>
            {
                new User { Role = Role.Administrator },
                new User { Role = Role.MaintenanceEmployee }
            };
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(u => u.GetAllUsers()).Returns(users);
            var userLogic = new UserLogic(userRepositoryMock.Object);

            var result = userLogic.GetAllAdministrators();

            Assert.IsTrue(result.Count() == 1 && result.First().Equals(users.First()));
        }
    }
}
