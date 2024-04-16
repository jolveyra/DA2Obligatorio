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
                new Invitation("Juan", "juan@gmail.com", 7),
                new Invitation("Jose", "jose@gmail.com", 7)
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
                new Invitation("Juan", "juan@gmail.com", 7),
                new Invitation("Jose", "jose@gmail.com", 7)
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
        public void GetInvitationByIdTestNotFound()
        {
            invitationLogicMock.Setup(i => i.GetInvitationById(It.IsAny<Guid>())).Throws(new ArgumentException());

            NotFoundObjectResult expected = new NotFoundObjectResult("There is no invitation with that specific id");

            NotFoundObjectResult result = invitationController.GetInvitationById(It.IsAny<Guid>()) as NotFoundObjectResult;

            invitationLogicMock.VerifyAll();
            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode) && expected.Value.Equals(result.Value));
        }

        [TestMethod]
        public void CreateInvitationTestCreated()
        {
            CreateInvitationRequestModel createInvitationRequest = new CreateInvitationRequestModel() { Name = "Juan", Email = "juan@gmail.com", DaysToExpiration = 7 };
            Invitation expected = new Invitation("Juan", "juan@gmail.com", 7);

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
            Invitation expected = new Invitation("Juan", "juan@gmail.com", 7);
            expected.IsAccepted = true;

            invitationLogicMock.Setup(i => i.UpdateInvitation(It.IsAny<Guid>(), It.IsAny<bool>())).Returns(expected);

            InvitationResponseModel expectedResult = new InvitationResponseModel(expected);
            OkObjectResult expectedObjectResult = new OkObjectResult(expectedResult);

            OkObjectResult result = invitationController.UpdateInvitationById(It.IsAny<Guid>(), createInvitationRequest) as OkObjectResult;
            InvitationResponseModel resultObject = result.Value as InvitationResponseModel;

            invitationLogicMock.VerifyAll();
            Assert.IsTrue(expectedObjectResult.StatusCode.Equals(result.StatusCode) && expectedResult.Accepted.Equals(resultObject.Accepted));
        }

        [TestMethod]
        public void UpdateInvitationByIdTestNotFound()
        {
            invitationLogicMock.Setup(i => i.UpdateInvitation(It.IsAny<Guid>(), It.IsAny<bool>())).Throws(new ArgumentException());

            NotFoundObjectResult expected = new NotFoundObjectResult("There is no invitation with that specific id");

            NotFoundObjectResult result = invitationController.UpdateInvitationById(It.IsAny<Guid>(), new UpdateInvitationRequestModel()) as NotFoundObjectResult;

            invitationLogicMock.VerifyAll();
            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode) && expected.Value.Equals(result.Value));
        }

        [TestMethod]
        public void UpdateInvitationByIdTestInternalError()
        {
            invitationLogicMock.Setup(i => i.UpdateInvitation(It.IsAny<Guid>(), It.IsAny<bool>())).Throws(new Exception());

            ObjectResult expected = new ObjectResult("An error occurred while updating the invitation") { StatusCode = 500 };

            ObjectResult result = invitationController.UpdateInvitationById(It.IsAny<Guid>(), new UpdateInvitationRequestModel()) as ObjectResult;

            invitationLogicMock.VerifyAll();
            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode) && expected.Value.Equals(result.Value));
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

        [TestMethod]
        public void DeleteInvitationByIdTestNoContent()
        {
            invitationLogicMock.Setup(i => i.DeleteInvitation(It.IsAny<Guid>())).Throws(new ArgumentException());

            NoContentResult expected = new NoContentResult();

            NoContentResult result = invitationController.DeleteInvitationById(It.IsAny<Guid>()) as NoContentResult;

            invitationLogicMock.VerifyAll();
            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode));
        }
    }
}
