using Domain;
using LogicInterfaces;
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModels;

namespace ManagementApiTest
{
    [TestClass]
    public class InvitationControllerTest
    {
        private Mock<IInvitationLogic> invitationLogicMock;
        private InvitationController invitationController;

        [TestInitialize]
        public void TestInitialize()
        {
            invitationLogicMock = new Mock<IInvitationLogic>(MockBehavior.Strict);
            invitationController = new InvitationController(invitationLogicMock.Object);
        }

        [TestMethod]
        public void GetAllInvitationsTestOk()
        {
            IEnumerable<Invitation> invitations = new List<Invitation>
            {
                new Invitation() { Name = "Juan", Email = "juan@gmail.com" },
                new Invitation() { Name = "Jose", Email = "jose@gmail.com" }
            };

            invitationLogicMock.Setup(x => x.GetAllInvitations()).Returns(invitations);

            OkObjectResult expected = new OkObjectResult(new List<InvitationResponseModel>
            {
                new InvitationResponseModel(invitations.First()),
                new InvitationResponseModel(invitations.Last())
            });
            List<InvitationResponseModel> expectedObject = expected.Value as List<InvitationResponseModel>;

            OkObjectResult result = invitationController.GetAllInvitations() as OkObjectResult;
            List<InvitationResponseModel> objectResult = result.Value as List<InvitationResponseModel>;

            invitationLogicMock.VerifyAll();
            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode) && expectedObject.First().Equals(objectResult.First()) && expectedObject.Last().Equals(objectResult.Last()));
        }

        [TestMethod]
        public void GetInvitationByIdTestOk()
        {
            IEnumerable<Invitation> invitations = new List<Invitation>
            {
                new Invitation() { Name = "Juan", Email = "juan@gmail.com" },
                new Invitation() { Name = "Jose", Email = "jose@gmail.com" }
            };

            invitationLogicMock.Setup(i => i.GetInvitationById(It.IsAny<Guid>())).Returns(invitations.First());

            OkObjectResult expected = new OkObjectResult(new InvitationResponseModel(invitations.First()));
            InvitationResponseModel expectedObject = expected.Value as InvitationResponseModel;

            OkObjectResult result = invitationController.GetInvitationById(It.IsAny<Guid>()) as OkObjectResult;
            InvitationResponseModel objectResult = result.Value as InvitationResponseModel;

            invitationLogicMock.VerifyAll();
            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode) && expectedObject.Equals(objectResult));
        }

        [TestMethod]
        public void GetInvitationByIdNotFound()
        {
            invitationLogicMock.Setup(i => i.GetInvitationById(It.IsAny<Guid>())).Throws(new ArgumentException());

            NotFoundObjectResult expected = new NotFoundObjectResult("There is no invitation with that specific id");

            NotFoundObjectResult result = invitationController.GetInvitationById(It.IsAny<Guid>()) as NotFoundObjectResult;

            invitationLogicMock.VerifyAll();
            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode) && expected.Value.Equals(result.Value));
        }

        [TestMethod]
        public void CreateInvitationTestOk()
        {
            InvitationRequestModel invitationRequest = new InvitationRequestModel() { Name = "Juan", Email = "juan@gmail.com" };
            Invitation expected = new Invitation() { Name = "Juan", Email = "juan@gmail.com" };

            invitationLogicMock.Setup(i => i.CreateInvitation(It.IsAny<Invitation>())).Returns(expected);

            InvitationResponseModel expectedResult = new InvitationResponseModel(expected);
            CreatedAtActionResult expectedObjectResult = new CreatedAtActionResult("CreateInvitation", "CreateInvitation", new { Id = 1 }, expectedResult);

            CreatedAtActionResult result = invitationController.CreateInvitation(invitationRequest) as CreatedAtActionResult;
            InvitationResponseModel resultObject = result.Value as InvitationResponseModel;

            invitationLogicMock.VerifyAll();
            Assert.IsTrue(expectedObjectResult.StatusCode.Equals(result.StatusCode) && expectedResult.Equals(resultObject));
        }
    }
}
