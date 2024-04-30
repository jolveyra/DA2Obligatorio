using Domain;
using LogicInterfaces;
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModels.InvitationModels;

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
                new Invitation() { Id = Guid.NewGuid(), Name = "Juan", Email = "juan@gmail.com" },
                new Invitation() { Id = Guid.NewGuid(), Name = "Jose", Email = "jose@gmail.com" },
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
                new Invitation() { Id = Guid.NewGuid(), Name = "Juan", Email = "juan@gmail.com" },
                new Invitation() { Id = Guid.NewGuid(), Name = "Jose", Email = "jose@gmail.com" },
            };

            invitationLogicMock.Setup(i => i.GetInvitationById(It.IsAny<Guid>())).Returns(invitations.First());

            OkObjectResult expected = new OkObjectResult(new InvitationResponseModel(invitations.First()));
            InvitationResponseModel expectedObject = expected.Value as InvitationResponseModel;

            OkObjectResult result = invitationController.GetInvitationById(invitations.First().Id) as OkObjectResult;
            InvitationResponseModel objectResult = result.Value as InvitationResponseModel;

            invitationLogicMock.VerifyAll();
            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode) && expectedObject.Equals(objectResult));
        }

        [TestMethod]
        public void CreateInvitationTestCreated()
        {
            CreateInvitationRequestModel createInvitationRequest = new CreateInvitationRequestModel() { Name = "Juan", Email = "juan@gmail.com", DaysToExpiration = 7 };
            Invitation expected = new Invitation() { Id = Guid.NewGuid(), Name = "Juan", Email = "juan@gmail.com" };

            invitationLogicMock.Setup(i => i.CreateInvitation(It.IsAny<Invitation>())).Returns(expected);

            InvitationResponseModel expectedResult = new InvitationResponseModel(expected);
            CreatedAtActionResult expectedObjectResult = new CreatedAtActionResult("CreateInvitation", "CreateInvitation", new { Id = expected.Id }, expectedResult);

            CreatedAtActionResult result = invitationController.CreateInvitation(createInvitationRequest) as CreatedAtActionResult;
            InvitationResponseModel resultObject = result.Value as InvitationResponseModel;

            invitationLogicMock.VerifyAll();
            Assert.IsTrue(expectedObjectResult.StatusCode.Equals(result.StatusCode) && expectedResult.Equals(resultObject));
        }

        [TestMethod]
        public void UpdateInvitationByIdTestOk()
        {
            UpdateInvitationRequestModel createInvitationRequest = new UpdateInvitationRequestModel() { IsAccepted = true };
            Invitation expected = new Invitation() { Id = Guid.NewGuid(), Name = "Juan", Email = "juan@gmail.com" };

            invitationLogicMock.Setup(i => i.UpdateInvitationStatus(It.IsAny<Guid>(), It.IsAny<bool>())).Returns(expected);

            expected.IsAccepted = true;
            UpdateInvitationResponseModel expectedResult = new UpdateInvitationResponseModel(expected);
            OkObjectResult expectedObjectResult = new OkObjectResult(expectedResult);

            OkObjectResult result = invitationController.UpdateInvitationStatusById(expected.Id, createInvitationRequest) as OkObjectResult;
            UpdateInvitationResponseModel resultObject = result.Value as UpdateInvitationResponseModel;

            invitationLogicMock.VerifyAll();
            Assert.IsTrue(expectedObjectResult.StatusCode.Equals(result.StatusCode) && expectedResult.IsAccepted.Equals(resultObject.IsAccepted));
        }

        [TestMethod]
        public void DeleteInvitationByIdTestOk()
        {
            invitationLogicMock.Setup(i => i.DeleteInvitation(It.IsAny<Guid>()));

            OkResult expected = new OkResult();

            OkResult result = invitationController.DeleteInvitationById(It.IsAny<Guid>()) as OkResult;

            invitationLogicMock.VerifyAll();
            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode));
        }
    }
}
