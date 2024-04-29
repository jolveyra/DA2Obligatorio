using DataAccess.Context;
using DataAccess.Repositories;
using Domain;
using Moq;
using Moq.EntityFrameworkCore;

namespace DataAccessTest
{
    [TestClass]
    public class SessionRepositoryTest
    {
        private Mock<BuildingBossContext> _contextMock;
        private SessionRepository _sessionRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _contextMock = new Mock<BuildingBossContext>();
            _sessionRepository = new SessionRepository(_contextMock.Object);
        }

        [TestMethod]
        public void GetSessionByTokenTest()
        {
            Guid token = Guid.NewGuid();
            Session session = new Session { Id = token, UserId = Guid.NewGuid() };

            _contextMock.Setup(context => context.Sessions).ReturnsDbSet(new List<Session>() { session });
            
            Session result = _sessionRepository.GetSessionByToken(token);

            _contextMock.Verify(context => context.Sessions, Times.Once);
            Assert.AreEqual(session, result);
        }

        [TestMethod]
        public void CreateSessionTest()
        {
            Session session = new Session { Id = Guid.NewGuid(), UserId = Guid.NewGuid() };

            _contextMock.Setup(context => context.Sessions.Add(session));
            _contextMock.Setup(context => context.SaveChanges()).Returns(1);
            
            Session newSession = _sessionRepository.CreateSession(session);

            _contextMock.Verify(context => context.Sessions.Add(session), Times.Once);
            _contextMock.Verify(context => context.SaveChanges(), Times.Once);
            Assert.AreEqual(session, newSession);
        }

        [TestMethod]
        public void GetSessionByUserIdTest()
        {
            Guid userId = Guid.NewGuid();
            Session session = new Session { Id = Guid.NewGuid(), UserId = userId };

            _contextMock.Setup(context => context.Sessions).ReturnsDbSet(new List<Session>() { session });

            Session result = _sessionRepository.GetSessionByUserId(userId);

            _contextMock.Verify(context => context.Sessions, Times.Once);
            Assert.AreEqual(session, result);
        }
    }
}
