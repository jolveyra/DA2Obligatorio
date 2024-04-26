using DataAccess.Context;
using DataAccess.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace DataAccessTest
{
    [TestClass]
    public class InvitationRepositoryTest
    {
        [TestMethod]
        public void GetAllInvitationsTest()
        {
            List<Invitation> invitations = new List<Invitation>()
            {
                new Invitation() { Id = Guid.NewGuid(), Name = "Juan", Email = "juan@gmail.com" },
                new Invitation() { Id = Guid.NewGuid(), Name = "Jose", Email = "jose@gmail.com" },
            };

            Mock<BuildingBossContext> contextMock = new Mock<BuildingBossContext>();
            contextMock.Setup(context => context.Invitations).ReturnsDbSet(invitations);
            InvitationRepository invitationRepository = new InvitationRepository(contextMock.Object);

            IEnumerable<Invitation> result = invitationRepository.GetAllInvitations();

            contextMock.Verify(context => context.Invitations, Times.Once());
            Assert.IsTrue(result.SequenceEqual(invitations));
        }

        [TestMethod]
        public void GetInvitationByIdTest()
        {
            Guid id = Guid.NewGuid();
            Invitation invitation = new Invitation() { Id = id, Name = "Juan", Email = "juan@gmail.com" };

            Mock<BuildingBossContext> contextMock = new Mock<BuildingBossContext>();
            contextMock.Setup(context => context.Invitations).ReturnsDbSet(new List<Invitation>() { invitation });
            InvitationRepository invitationRepository = new InvitationRepository(contextMock.Object);

            Invitation result = invitationRepository.GetInvitationById(id);

            contextMock.Verify(context => context.Invitations, Times.Once());
            Assert.AreEqual(invitation, result);
        }

        

        [TestMethod]
        public void GetNonExistingInvitationByIdTest()
        {
            Guid id = Guid.NewGuid();

            Mock<BuildingBossContext> contextMock = new Mock<BuildingBossContext>();
            contextMock.Setup(context => context.Invitations).ReturnsDbSet(new List<Invitation>());
            InvitationRepository invitationRepository = new InvitationRepository(contextMock.Object);

            Exception exception = null;

            try
            {
                invitationRepository.GetInvitationById(id);
            }
            catch (Exception e)
            {
                exception = e;
            }

            contextMock.Verify(context => context.Invitations, Times.Once());
            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.AreEqual("Invitation not found", exception.Message);
        }
    }
}
