using CustomExceptions.DataAccessExceptions;
using DataAccess.Context;
using DataAccess.Repositories;
using Domain;
using Moq;
using Moq.EntityFrameworkCore;
using RepositoryInterfaces;

namespace DataAccessTest
{
    [TestClass]
    public class InvitationRepositoryTest
    {
        private Mock<BuildingBossContext> _contextMock;
        private InvitationRepository _invitationRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _contextMock = new Mock<BuildingBossContext>();
            _invitationRepository = new InvitationRepository(_contextMock.Object);
        }

        [TestMethod]
        public void GetAllInvitationsTest()
        {
            List<Invitation> invitations = new List<Invitation>()
            {
                new Invitation() { Id = Guid.NewGuid(), Name = "Juan", Email = "juan@gmail.com" },
                new Invitation() { Id = Guid.NewGuid(), Name = "Jose", Email = "jose@gmail.com" },
            };

            _contextMock.Setup(context => context.Invitations).ReturnsDbSet(invitations);
            IEnumerable<Invitation> result = _invitationRepository.GetAllInvitations();

            _contextMock.Verify(context => context.Invitations, Times.Once());
            Assert.IsTrue(result.SequenceEqual(invitations));
        }

        [TestMethod]
        public void GetInvitationByIdTest()
        {
            Guid id = Guid.NewGuid();
            Invitation invitation = new Invitation() { Id = id, Name = "Juan", Email = "juan@gmail.com" };

            _contextMock.Setup(context => context.Invitations).ReturnsDbSet(new List<Invitation>() { invitation });

            Invitation result = _invitationRepository.GetInvitationById(id);

            _contextMock.Verify(context => context.Invitations, Times.Once());
            Assert.AreEqual(invitation, result);
        }

        

        [TestMethod]
        public void GetNonExistingInvitationByIdTest()
        {
            Guid id = Guid.NewGuid();

            _contextMock.Setup(context => context.Invitations).ReturnsDbSet(new List<Invitation>());
            
            Exception exception = null;

            try
            {
                _invitationRepository.GetInvitationById(id);
            }
            catch (Exception e)
            {
                exception = e;
            }

            _contextMock.Verify(context => context.Invitations, Times.Once());
            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.AreEqual("Invitation not found", exception.Message);
        }

        [TestMethod]
        public void CreateInvitationTest()
        {
            Invitation invitation = new Invitation() { Id = Guid.NewGuid(), Name = "Juan", Email = "juan@gmail.com" };

            _contextMock.Setup(context => context.Invitations).ReturnsDbSet(new List<Invitation>());
            _contextMock.Setup(context => context.Invitations.Add(invitation));
            _contextMock.Setup(context => context.SaveChanges()).Returns(1);

            Invitation result = _invitationRepository.CreateInvitation(invitation);

            _contextMock.Verify(context => context.Invitations, Times.Once());
            _contextMock.Verify(context => context.Invitations.Add(invitation), Times.Once());
            _contextMock.Verify(context => context.SaveChanges(), Times.Once());
            Assert.AreEqual(invitation, result);
        }

        [TestMethod]
        public void DeleteInvitationByIdTest()
        {
            Guid id = Guid.NewGuid();
            Invitation invitation = new Invitation() { Id = id, Name = "Juan", Email = "juan@gmail.com" };

            _contextMock.Setup(context => context.Invitations).ReturnsDbSet(new List<Invitation>() { invitation });
            _contextMock.Setup(context => context.Invitations.Remove(invitation));
            _contextMock.Setup(context => context.SaveChanges()).Returns(1);

            _invitationRepository.DeleteInvitationById(id);

            _contextMock.Verify(context => context.Invitations, Times.Exactly(3));
            _contextMock.Verify(context => context.Invitations.Remove(invitation), Times.Once());
            _contextMock.Verify(context => context.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void UpdateInvitationStatusByIdTest()
        {
            Invitation invitation = new Invitation() { Id = Guid.NewGuid(), Name = "Juan", Email = "juan@gmail.com", IsAccepted = false };

            _contextMock.Setup(context => context.Invitations.Update(invitation));
            _contextMock.Setup(context => context.SaveChanges()).Returns(1);

            Invitation result = _invitationRepository.UpdateInvitation(invitation);

            _contextMock.Verify(context => context.Invitations.Update(invitation), Times.Once());
            _contextMock.Verify(context => context.SaveChanges(), Times.Once());
            Assert.AreEqual(invitation, result);
        }

        [TestMethod]
        public void DeleteNonExistingInvitation()
        {
            Guid invitationId = Guid.NewGuid();

            _contextMock.Setup(x => x.Invitations).ReturnsDbSet(new List<Invitation> { });

            try
            {
                _invitationRepository.DeleteInvitationById(invitationId);
            }catch(Exception e)
            {
                _contextMock.Verify(x => x.Invitations, Times.Once());
                Assert.IsInstanceOfType(e, typeof(DeleteException));
            }
        }
    }
}
