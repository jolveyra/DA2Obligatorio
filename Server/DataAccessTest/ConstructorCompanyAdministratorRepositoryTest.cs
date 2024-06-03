using DataAccess.Context;
using DataAccess.Repositories;
using Domain;
using Moq;
using Moq.EntityFrameworkCore;

namespace DataAccessTest
{
    [TestClass]
    public class ConstructorCompanyAdministratorRepositoryTest
    {
        private Mock<BuildingBossContext> _contextMock;
        private ConstructorCompanyAdministratorRepository _constructorCompanyAdministratorRepository;

        [TestInitialize]
        public void Initialize()
        {
            _contextMock = new Mock<BuildingBossContext>();
            _constructorCompanyAdministratorRepository = new ConstructorCompanyAdministratorRepository(_contextMock.Object);
        }

        [TestMethod]
        public void GetConstructorCompanyAdministratorByUserIdTest()
        {
            Guid userId = Guid.NewGuid();

            ConstructorCompanyAdministrator administrator = new ConstructorCompanyAdministrator { Id = userId };

            _contextMock.Setup(context => context.Users).ReturnsDbSet(new List<ConstructorCompanyAdministrator>() { administrator });

            ConstructorCompanyAdministrator result = _constructorCompanyAdministratorRepository.GetConstructorCompanyAdministratorByUserId(userId);

            _contextMock.Verify(context => context.Users, Times.Once());

            Assert.AreEqual(administrator, result);
        }

        [TestMethod]
        public void GetNonExistingConstructorCompanyAdministratorByUserIdTest()
        {
            Guid userId = Guid.NewGuid();

            _contextMock.Setup(context => context.Users).ReturnsDbSet(new List<ConstructorCompanyAdministrator>());

            Exception exception = null;

            try
            {
                _constructorCompanyAdministratorRepository.GetConstructorCompanyAdministratorByUserId(userId);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.AreEqual("Constructor company administrator not found", exception.Message);
        }

        [TestMethod]
        public void UpdateConstructorCompanyAdministratorTest()
        {
            ConstructorCompanyAdministrator administrator = new ConstructorCompanyAdministrator { Id = Guid.NewGuid(), ConstructorCompanyId = Guid.NewGuid() };

            _contextMock.Setup(context => context.Users.Update(administrator));
            _contextMock.Setup(context => context.SaveChanges());

            ConstructorCompanyAdministrator result = _constructorCompanyAdministratorRepository.UpdateConstructorCompanyAdministrator(administrator);

            _contextMock.Verify(context => context.Users.Update(administrator), Times.Once());
            _contextMock.Verify(context => context.SaveChanges(), Times.Once());
            Assert.AreEqual(administrator, result);
        }

        [TestMethod]
        public void CreateUserConstructorCompanyAdministratorTest()
        {
            ConstructorCompanyAdministrator administrator = new ConstructorCompanyAdministrator { Id = Guid.NewGuid(), Email = "juan@gmail.com", Name = "Juan" };

            _contextMock.Setup(context => context.Users.Add(administrator));
            _contextMock.Setup(context => context.SaveChanges()).Returns(1);

            ConstructorCompanyAdministrator result = _constructorCompanyAdministratorRepository.CreateConstructorCompanyAdministrator(administrator);

            _contextMock.Verify(context => context.Users.Add(administrator), Times.Once());
            _contextMock.Verify(context => context.SaveChanges(), Times.Once());
            Assert.IsTrue(administrator.Equals(result) && administrator.ConstructorCompanyId.Equals(result.ConstructorCompanyId));
        }

        [TestMethod]
        public void GetAllUsersConstructorCompanyAdministratorsTest()
        {
            List<ConstructorCompanyAdministrator> users = new List<ConstructorCompanyAdministrator>
            {
                new ConstructorCompanyAdministrator { Id = Guid.NewGuid(), Email = "juan@gmail.com", Name = "Juan" }
            };

            _contextMock.Setup(context => context.Users).ReturnsDbSet(users);

            IEnumerable<ConstructorCompanyAdministrator> result = _constructorCompanyAdministratorRepository.GetAllConstructorCompanyAdministrators();

            _contextMock.Verify(context => context.Users, Times.Once());
            Assert.IsTrue(result.SequenceEqual(users));
        }
    }
}
