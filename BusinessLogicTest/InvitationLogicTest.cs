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
        private Mock<IUserRepository> userRepositoryMock;
        private InvitationLogic invitationLogic;

        [TestInitialize]
        public void Initialize()
        {
            invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            invitationLogic = new InvitationLogic(invitationRepositoryMock.Object, userRepositoryMock.Object);
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
            userRepositoryMock.Setup(repository => repository.GetUserByEmail(It.IsAny<string>())).Throws(new ArgumentException("There is no user with that email."));

            invitation.Id = Guid.NewGuid();
            Invitation expected = invitation;
            Invitation result = invitationLogic.CreateInvitation(invitation);

            invitationRepositoryMock.VerifyAll();
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "There is already a user with the same email")]
        public void CreateInvitationWithExistingEmailTest()
        {
            User user = new User() { Name = "Juan", Email = "juan@gmail.com" };
            Invitation invitation = new Invitation() { Name = "Juan", Email = "juan@gmail.com"};

            invitationRepositoryMock.Setup(repository => repository.CreateInvitation(It.IsAny<Invitation>())).Throws(new ArgumentException());
            userRepositoryMock.Setup(repository => repository.GetUserByEmail(It.IsAny<string>())).Returns(user);

            invitationLogic.CreateInvitation(invitation);
            invitationRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void GetInvitationByIdTest()
        {
            Guid id = Guid.NewGuid();
            Invitation expected = new Invitation() { Id = id, Name = "Juan", Email = "juan@gmail.com" };

            invitationRepositoryMock.Setup(repository => repository.GetInvitationById(id)).Returns(expected);
            
            Invitation result = invitationLogic.GetInvitationById(id);

            invitationRepositoryMock.VerifyAll();
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "There is no invitation with that id")]
        public void GetNonExistingInvitationByIdTest()
        {
            Guid id = Guid.NewGuid();

            invitationRepositoryMock.Setup(repository => repository.GetInvitationById(id)).Throws(new ArgumentException());
            
            invitationLogic.GetInvitationById(id);

            invitationRepositoryMock.VerifyAll();
        }
    }
}