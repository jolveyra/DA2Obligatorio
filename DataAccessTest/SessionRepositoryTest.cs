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
        [TestMethod]
        public void GetUserIdByTokenTest()
        {
            Guid token = Guid.NewGuid();
            Session session = new Session { Id = token, UserId = Guid.NewGuid() };

            Mock<BuildingBossContext> _contextMock = new Mock<BuildingBossContext>();
            _contextMock.Setup(context => context.Sessions).ReturnsDbSet(new List<Session>() { session });
            SessionRepository sessionRepository = new SessionRepository(_contextMock.Object);

            Guid userId = sessionRepository.GetUserIdByToken(token);

            _contextMock.Verify(context => context.Sessions, Times.Once);
            Assert.AreEqual(session.UserId, userId);
        }

        [TestMethod]
        public void CreateSessionTest()
        {
            Session session = new Session { Id = Guid.NewGuid(), UserId = Guid.NewGuid() };

            Mock<BuildingBossContext> _contextMock = new Mock<BuildingBossContext>();
            _contextMock.Setup(context => context.Sessions.Add(session));
            _contextMock.Setup(context => context.SaveChanges()).Returns(1);
            SessionRepository sessionRepository = new SessionRepository(_contextMock.Object);

            Session newSession = sessionRepository.CreateSession(session);

            _contextMock.Verify(context => context.Sessions.Add(session), Times.Once);
            _contextMock.Verify(context => context.SaveChanges(), Times.Once);
            Assert.AreEqual(session, newSession);
        }
    }
}
