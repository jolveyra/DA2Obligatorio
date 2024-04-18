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
                new Invitation("Juan", "juan123@gmail.com", 7), 
                new Invitation("Jose", "jose456@gmail.com", 7)
            };

            Mock<IInvitationRepository> invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            invitationRepositoryMock.Setup(repository => repository.GetAllInvitations()).Returns(invitations);
            InvitationLogic logic = new InvitationLogic(invitationRepositoryMock.Object);

            IEnumerable<Invitation> result = logic.GetAllInvitations();

            invitationRepositoryMock.VerifyAll();
            Assert.IsTrue(result.SequenceEqual(invitations));
        }
    }
}