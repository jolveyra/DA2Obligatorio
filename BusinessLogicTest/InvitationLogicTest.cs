using BusinessLogic;
using Domain;
using Moq;
using RepositoryInterfaces;

namespace BusinessLogicTest
{
    [TestClass]
    public class InvitationLogicTest
    {
        private Mock<IInvitationRepository> invitationRepositoryMock;
        private InvitationLogic invitationLogic;

        [TestInitialize]
        public void Initialize()
        {
            invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            invitationLogic = new InvitationLogic(invitationRepositoryMock.Object);
        }

        [TestMethod]
        public void GetAllInvitationsTest()
        {
            List<Invitation> invitations = new List<Invitation>()
            {
                new Invitation() { Id = Guid.NewGuid(), Name = "Juan", Email = "juan123@gmail.com" }, 
                new Invitation() { Id = Guid.NewGuid(), Name = "Jose", Email = "jose456@gmail.com" }
            };

            invitationRepositoryMock.Setup(repository => repository.GetAllInvitations()).Returns(invitations);

            IEnumerable<Invitation> result = invitationLogic.GetAllInvitations();

            invitationRepositoryMock.VerifyAll();
            Assert.IsTrue(result.SequenceEqual(invitations));
        }

        [TestMethod]
        public void CreateInvitationTest()
        {
            Invitation invitation = new Invitation() { Name = "Juan", Email = "juan123@gmail.com" };

            invitationRepositoryMock.Setup(repository => repository.CreateInvitation(invitation)).Returns(invitation);

            invitation.Id = Guid.NewGuid();
            Invitation expected = invitation;
            Invitation result = invitationLogic.CreateInvitation(invitation);

            invitationRepositoryMock.VerifyAll();
            Assert.IsTrue(invitation.Equals(invitation));
        }
    }
}