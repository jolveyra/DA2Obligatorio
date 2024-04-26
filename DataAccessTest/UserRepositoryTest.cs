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
        private Mock<BuildingBossContext> _contextMock;
        private UserRepository _userRepository;

        [TestInitialize]
        public void Initialize()
        {
            _contextMock = new Mock<BuildingBossContext>();
            _userRepository = new UserRepository(_contextMock.Object);
        }

        [TestMethod]
        public void GetAllUsersTest()
        {
            List<User> users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Email = "juan@gmail.com", Name = "Juan" }
            };

            _contextMock.Setup(context => context.Users).ReturnsDbSet(users);

            IEnumerable<User> result = _userRepository.GetAllUsers();

            _contextMock.Verify(context => context.Users, Times.Once());
            Assert.IsTrue(result.SequenceEqual(users));
        }

        [TestMethod]
        public void GetUserByIdTest()
        {
            User user = new User { Id = Guid.NewGuid(), Email = "juan@gmail.com", Name = "Juan" };

            _contextMock.Setup(context => context.Users).ReturnsDbSet(new List<User> { user });
            
            User result = _userRepository.GetUserById(user.Id);

            _contextMock.Verify(context => context.Users, Times.Once());
            Assert.AreEqual(user, result);
        }
    }
}
