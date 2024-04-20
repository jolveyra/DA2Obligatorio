using BusinessLogic;
using Domain;
using Moq;
using RepositoryInterfaces;

namespace BusinessLogicTest
{
    [TestClass]
    public class InvitationLogicTest
    {
        [TestMethod]
        public void GetAllInvitationsTest()
        {
            List<Invitation> invitations = new List<Invitation>()
            {
                new Invitation() { Id = Guid.NewGuid(), Name = "Juan", Email = "juan123@gmail.com" }, 
                new Invitation() { Id = Guid.NewGuid(), Name = "Jose", Email = "jose456@gmail.com" }
            };

            Mock<IInvitationRepository> invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            invitationRepositoryMock.Setup(repository => repository.GetAllInvitations()).Returns(invitations);
            InvitationLogic logic = new InvitationLogic(invitationRepositoryMock.Object);

            IEnumerable<Invitation> result = logic.GetAllInvitations();

            invitationRepositoryMock.VerifyAll();
            Assert.IsTrue(result.SequenceEqual(invitations));
        }

        [TestMethod]
        public void CreateInvitationTest()
        {
            Invitation invitation = new Invitation() { Name = "Juan", Email = "juan123@gmail.com" };

            Mock<IInvitationRepository> invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            invitationRepositoryMock.Setup(repository => repository.CreateInvitation(invitation)).Returns(invitation);
            InvitationLogic logic = new InvitationLogic(invitationRepositoryMock.Object);

            invitation.Id = Guid.NewGuid();

            Invitation expected = invitation;
            Invitation result = logic.CreateInvitation(invitation);

            invitationRepositoryMock.VerifyAll();
            Assert.IsTrue(invitation.Equals(invitation));
        }
    }
}