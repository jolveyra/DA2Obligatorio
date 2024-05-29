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

        [TestMethod]
        public void GetNonExistingUserByIdTest()
        {
            Guid id = Guid.NewGuid();
            _contextMock.Setup(context => context.Users).ReturnsDbSet(new List<User>());

            Exception exception = null;

            try
            {
                _userRepository.GetUserById(id);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.AreEqual("User not found", exception.Message);
        }

        [TestMethod]
        public void CreateUserTest()
        {
            User user = new User { Id = Guid.NewGuid(), Email = "juan@gmail.com", Name = "Juan" };

            _contextMock.Setup(context => context.Users.Add(user));
            _contextMock.Setup(context => context.SaveChanges()).Returns(1);

            User result = _userRepository.CreateUser(user);

            _contextMock.Verify(context => context.Users.Add(user), Times.Once());
            _contextMock.Verify(context => context.SaveChanges(), Times.Once());
            Assert.AreEqual(user, result);
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            User user = new User { Id = Guid.NewGuid(), Email = "juan@gmail.com", Name = "Juan" };

            _contextMock.Setup(context => context.Users.Update(user));
            _contextMock.Setup(context => context.SaveChanges()).Returns(1);

            User result = _userRepository.UpdateUser(user);

            _contextMock.Verify(context => context.Users.Update(user), Times.Once());
            _contextMock.Verify(context => context.SaveChanges(), Times.Once());
            Assert.AreEqual(user, result);
        }

        [TestMethod]
        public void GetConstructorCompanyAdministratorByUserIdTest()
        {
            Guid userId = Guid.NewGuid();

            ConstructorCompanyAdministrator administrator = new ConstructorCompanyAdministrator { Id = userId };

            _contextMock.Setup(context => context.ConstructorCompanyAdministrators).ReturnsDbSet(new List<ConstructorCompanyAdministrator>() { administrator });

            ConstructorCompanyAdministrator result = _userRepository.GetConstructorCompanyAdministratorByUserId(userId);

            _contextMock.Verify(context => context.ConstructorCompanyAdministrators, Times.Once());

            Assert.AreEqual(administrator, result);
        }

        [TestMethod]
        public void GetNonExistingConstructorCompanyAdministratorByUserIdTest()
        {
            Guid userId = Guid.NewGuid();

            _contextMock.Setup(context => context.ConstructorCompanyAdministrators).ReturnsDbSet(new List<ConstructorCompanyAdministrator>());

            Exception exception = null;

            try
            {
                _userRepository.GetConstructorCompanyAdministratorByUserId(userId);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.AreEqual("Constructor company administrator not found", exception.Message);
        }
    }
}
