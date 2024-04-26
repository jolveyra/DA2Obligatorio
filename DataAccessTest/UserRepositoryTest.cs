using DataAccess.Context;
using DataAccess.Repositories;
using Domain;
using Moq;
using Moq.EntityFrameworkCore;

namespace DataAccessTest
{
    [TestClass]
    public class UserRepositoryTest
    {
        [TestMethod]
        public void GetAllUsersTest()
        {
            List<User> users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Email = "juan@gmail.com", Name = "Juan" }
            };

            Mock<BuildingBossContext> contextMock = new Mock<BuildingBossContext>();
            contextMock.Setup(context => context.Users).ReturnsDbSet(users);
            UserRepository userRepository = new UserRepository(contextMock.Object);

            IEnumerable<User> result = userRepository.GetAllUsers();

            contextMock.Verify(context => context.Users, Times.Once());
            Assert.IsTrue(result.SequenceEqual(users));
        }

        [TestMethod]
        public void GetUserByIdTest()
        {
            User user = new User { Id = Guid.NewGuid(), Email = "juan@gmail.com", Name = "Juan" };

            Mock<BuildingBossContext> contextMock = new Mock<BuildingBossContext>();
            contextMock.Setup(context => context.Users).ReturnsDbSet(new List<User> { user });
            UserRepository userRepository = new UserRepository(contextMock.Object);

            User result = userRepository.GetUserById(user.Id);

            contextMock.Verify(context => context.Users, Times.Once());
            Assert.AreEqual(user, result);
        }
    }
}
