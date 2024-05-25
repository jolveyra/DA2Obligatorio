using BusinessLogic;
using CustomExceptions;
using Domain;
using Moq;
using RepositoryInterfaces;
using System;

namespace BusinessLogicTest
{
    [TestClass]
    public class InvitationLogicTest
    {
        private Mock<IInvitationRepository> invitationRepositoryMock;
        private Mock<IUserRepository> userRepositoryMock;
        private Mock<ISessionRepository> sessionRepositoryMock;
        private InvitationLogic invitationLogic;

        [TestInitialize]
        public void Initialize()
        {
            invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            sessionRepositoryMock = new Mock<ISessionRepository>(MockBehavior.Strict);
            invitationLogic = new InvitationLogic(invitationRepositoryMock.Object, userRepositoryMock.Object, sessionRepositoryMock.Object);
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
            Invitation invitation = new Invitation() { Name = "Juan", Email = "juan123@gmail.com", ExpirationDate = DateTime.Now.AddDays(6)};

            invitationRepositoryMock.Setup(repository => repository.CreateInvitation(invitation)).Returns(invitation);
            userRepositoryMock.Setup(repository => repository.GetAllUsers()).Returns(new List<User>());

            invitation.Id = Guid.NewGuid();
            Invitation result = invitationLogic.CreateInvitation(invitation, "manager");

            invitation.Role = InvitationRole.Manager;
            Invitation expected = invitation;

            invitationRepositoryMock.VerifyAll();
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void CreateInvitationWithExistingEmailTest()
        {
            User user = new User() { Name = "Juan", Email = "juan123@gmail.com" };
            Invitation invitation = new Invitation() { Name = "Juan", Email = "juan123@gmail.com", ExpirationDate = DateTime.Now.AddDays(6) };

            userRepositoryMock.Setup(repository => repository.GetAllUsers()).Returns(new List<User>() { user });
            Exception exception = null;

            try
            {
                invitationLogic.CreateInvitation(invitation, "manager");
            }
            catch (Exception e)
            {
                exception = e;
            }

            userRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(InvitationException));
            Assert.IsTrue(exception.Message.Equals("There is already a user with the same email"));
        }

        [TestMethod]
        public void CreateInvitationWithInvalidEmailTest()
        {
            Invitation invitation = new Invitation() { Name = "Juan", Email = "", ExpirationDate = DateTime.Now.AddDays(6) };
            Exception exception = null;

            try
            {
                invitationLogic.CreateInvitation(invitation, "manager");
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(InvitationException));
            Assert.IsTrue(exception.Message.Equals("An Email must contain '@', '.' and be longer than 4 characters long"));
        }

        [TestMethod]
        public void CreateInvitationWithNoNameTest()
        {
            Invitation invitation = new Invitation() { Name = "", Email = "juan@gmail.com", ExpirationDate = DateTime.Now.AddDays(6) };
            Exception exception = null;

            try
            {
                invitationLogic.CreateInvitation(invitation, "manager");
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(InvitationException));
            Assert.IsTrue(exception.Message.Equals("The Name field cannot be empty"));
        }

        [TestMethod]
        public void CreateInvitationWithInvalidExpirationDateTest()
        {
            Invitation invitation = new Invitation() { Name = "Juan", Email = "juan@gmail.com", ExpirationDate = DateTime.Now.AddDays(-6) };
            Exception exception = null;

            try
            {
                invitationLogic.CreateInvitation(invitation, "manager");
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(InvitationException));
            Assert.IsTrue(exception.Message.Equals("The date of expiration must be from tomorrow onwards"));
        }

        [TestMethod]
        public void GetInvitationByIdTest()
        {
            Guid id = Guid.NewGuid();
            Invitation expected = new Invitation() { Name = "Juan", Email = "juan123@gmail.com", ExpirationDate = DateTime.Now.AddDays(6) };

            invitationRepositoryMock.Setup(repository => repository.GetInvitationById(It.IsAny<Guid>())).Returns(expected);
            
            Invitation result = invitationLogic.GetInvitationById(id);

            invitationRepositoryMock.VerifyAll();
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void UpdateInvitationStatusTest()
        {
            Guid id = Guid.NewGuid();
            Invitation invitation = new Invitation() { Id = id, Name = "Juan", Email = "juan@gmail.com", IsAccepted = false };
            Invitation expected = new Invitation() { Id = id, Name = "Juan", Email = "juan@gmail.com", IsAccepted = true };

            userRepositoryMock.Setup(repository => repository.GetAllUsers()).Returns(new List<User>());
            userRepositoryMock.Setup(repository => repository.CreateUser(It.IsAny<User>())).Returns(new User());
            sessionRepositoryMock.Setup(repository => repository.CreateSession(It.IsAny<Session>())).Returns(new Session());
            invitationRepositoryMock.Setup(repository => repository.GetInvitationById(It.IsAny<Guid>())).Returns(invitation);
            invitationRepositoryMock.Setup(repository => repository.UpdateInvitation(It.IsAny<Invitation>())).Returns(expected);

            Invitation result = invitationLogic.UpdateInvitationStatus(id, true);

            invitationRepositoryMock.VerifyAll();
            userRepositoryMock.VerifyAll();
            sessionRepositoryMock.VerifyAll();
            Assert.IsTrue(expected.Equals(result));
        }

        [TestMethod]
        public void UpdateExistingEmailInvitationStatusTest()
        {
            Guid id = Guid.NewGuid();
            Invitation invitation = new Invitation() { Id = id, Name = "Juan", Email = "juan@gmail.com", IsAccepted = false };

            userRepositoryMock.Setup(repository => repository.GetAllUsers()).Returns(new List<User>() { new User() { Email = "juan@gmail.com" } });
            invitationRepositoryMock.Setup(repository => repository.GetInvitationById(It.IsAny<Guid>())).Returns(invitation);
            Exception exception = null;

            try
            {
                Invitation result = invitationLogic.UpdateInvitationStatus(id, true);
            }
            catch (Exception e)
            {
                exception = e;
            }

            invitationRepositoryMock.VerifyAll();
            userRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(UserException));
            Assert.IsTrue(exception.Message.Equals("A user with the same email already exists"));
        }

        [TestMethod]
        public void UpdateAnsweredInvitationStatusTest()
        {
            Guid id = Guid.NewGuid();
            Invitation invitation = new Invitation() { Id = id, Name = "Juan", Email = "juan@gmail.com", IsAnswered = true, IsAccepted = false };

            invitationRepositoryMock.Setup(repository => repository.GetInvitationById(It.IsAny<Guid>())).Returns(invitation);
            Exception exception = null;

            try
            {
                Invitation result = invitationLogic.UpdateInvitationStatus(id, true);
            }
            catch (Exception e)
            {
                exception = e;
            }

            invitationRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(InvitationException));
            Assert.IsTrue(exception.Message.Equals("The invitation has already been answered"));
        }

        [TestMethod]
        public void DeleteNotExistingInvitationTest()
        {
            Guid idToDelete = Guid.NewGuid();
            Invitation invitation = new Invitation()
            {
                Id = idToDelete,
                Name = "Juan",
                Email = "juan@gmail.com",
                ExpirationDate = DateTime.Now.AddDays(3),
                IsAccepted = false,
                IsAnswered = true
            };

            invitationRepositoryMock.Setup(repository => repository.GetAllInvitations()).Returns(new List<Invitation>() { });

            try
            {
                invitationLogic.DeleteInvitation(idToDelete);

            }
            catch (Exception e)
            {
                invitationRepositoryMock.VerifyAll();
                Assert.IsInstanceOfType(e, typeof(DeleteException));
            }

        }

        [TestMethod]
        public void DeleteAnsweredInvitationTest()
        {
            Guid idToDelete = Guid.NewGuid();
            Invitation invitation = new Invitation()
            {
                Id = idToDelete, Name = "Juan", Email = "juan@gmail.com", ExpirationDate = DateTime.Now.AddDays(3),
                IsAccepted = false, IsAnswered = true
            };

            invitationRepositoryMock.Setup(repository => repository.GetAllInvitations()).Returns(new List<Invitation>() { invitation });
            invitationRepositoryMock.Setup(repository => repository.DeleteInvitationById(It.IsAny<Guid>()));

            invitationLogic.DeleteInvitation(idToDelete);

            invitationRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void DeleteExpiredInvitationTest()
        {
            Guid idToDelete = Guid.NewGuid();
            Invitation invitation = new Invitation()
            {
                Id = idToDelete, Name = "Juan", Email = "juan@gmail.com", ExpirationDate = DateTime.Now.AddDays(-3),
                IsAccepted = false, IsAnswered = false
            };

            invitationRepositoryMock.Setup(repository => repository.GetAllInvitations()).Returns(new List<Invitation>() { invitation });
            invitationRepositoryMock.Setup(repository => repository.DeleteInvitationById(It.IsAny<Guid>()));

            invitationLogic.DeleteInvitation(idToDelete);

            invitationRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void DeleteNonAnsweredNonExpiredInvitationTest()
        {
            Guid idToDelete = Guid.NewGuid();
            Invitation invitation = new Invitation()
            {
                Id = idToDelete, Name = "Juan", Email = "juan@gmail.com", ExpirationDate = DateTime.Now.AddDays(3),
                IsAccepted = false, IsAnswered = false
            };

            invitationRepositoryMock.Setup(repository => repository.GetAllInvitations()).Returns(new List<Invitation>() { invitation });
            Exception exception = null;

            try
            {
                invitationLogic.DeleteInvitation(idToDelete);
            }
            catch (Exception e)
            {
                exception = e;
            }

            invitationRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(InvitationException));
            Assert.IsTrue(exception.Message.Equals("The invitation cannot be deleted"));
        }

        [TestMethod]
        public void UpdateInvitationWithNoBodyTest()
        {
            Guid id= Guid.NewGuid();
            Exception exception = null;

            try
            {
                invitationLogic.UpdateInvitationStatus(id, null);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(InvitationException));
            Assert.IsTrue(exception.Message.Equals("The field isAccepted is missing in the body of the request"));
        }

        [TestMethod]
        public void CreateInvitationWithEmptyRoleTest()
        {
            Invitation invitation = new Invitation() { Name = "Juan", Email = "", ExpirationDate = DateTime.Now.AddDays(6) };
            Exception exception = null;

            try
            {
                invitationLogic.CreateInvitation(invitation, "");
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(InvitationException));
            Assert.IsTrue(exception.Message.Equals("The Role field cannot be empty"));
        }

        [TestMethod]
        public void CreateInvitationWithNonExistingRoleTest()
        {
            Invitation invitation = new Invitation() { Name = "Juan", Email = "", ExpirationDate = DateTime.Now.AddDays(6) };
            Exception exception = null;

            try
            {
                invitationLogic.CreateInvitation(invitation, "This role does not exist");
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(InvitationException));
            Assert.IsTrue(exception.Message.Equals("The Role field must be either 'ConstructorCompanyAdmin' or 'Manager'"));
        }
    }
}