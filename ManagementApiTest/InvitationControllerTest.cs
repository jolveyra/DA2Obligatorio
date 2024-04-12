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
                new Invitation() { Name = "Juan", Email = "juan@gmail.com" }
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
            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode) 
                          && expectedObject.First().Name.Equals(objectResult.First().Name)
                          && expectedObject.First().Email.Equals(objectResult.First().Email)
                          && expectedObject.First().Name.Equals(objectResult.Last().Name)
                          && expectedObject.First().Email.Equals(objectResult.Last().Email)
            );
            // TODO: Override Equals in InvitationResponseModel
        }
    }
}
