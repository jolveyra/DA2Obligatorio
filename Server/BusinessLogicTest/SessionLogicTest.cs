using BusinessLogic;
using Domain;
using Moq;
using RepositoryInterfaces;

namespace BusinessLogicTest
{
    [TestClass]
    public class SessionLogicTest
    {
        private Mock<ISessionRepository> sessionRepositoryMock;
        private Mock<IUserRepository> userRepositoryMock;
        private SessionLogic _sessionLogic;

        [TestInitialize]
        public void TestInitialize()
        {
            userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            sessionRepositoryMock = new Mock<ISessionRepository>(MockBehavior.Strict);
            _sessionLogic = new SessionLogic(sessionRepositoryMock.Object, userRepositoryMock.Object);
        }

        [TestMethod]
        public void GetUserRoleTestOk()
        {
            User user = new User() { Role = Role.Administrator };
            Session session = new Session() { UserId = user.Id };

            sessionRepositoryMock.Setup(x => x.GetSessionByToken(It.IsAny<Guid>())).Returns(session);
            userRepositoryMock.Setup(x => x.GetUserById(It.IsAny<Guid>())).Returns(user);

            string expected = user.Role.ToString();
            string result = _sessionLogic.GetUserRoleByToken(session.UserId);

            sessionRepositoryMock.VerifyAll();
            userRepositoryMock.VerifyAll();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetUserIdByTokenTestOk()
        {
            Guid token = Guid.NewGuid();

            Session session = new Session() { UserId = Guid.NewGuid(), Id = token };

            sessionRepositoryMock.Setup(s => s.GetSessionByToken(It.IsAny<Guid>())).Returns(session);

            Guid result = _sessionLogic.GetUserIdByToken(token);

            sessionRepositoryMock.VerifyAll();
            Assert.AreEqual(session.UserId, result);
        }
    }
}
