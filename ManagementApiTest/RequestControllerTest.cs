using Domain;
using LogicInterfaces;
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModels.RequestsModels;

namespace ManagementApiTest
{
    [TestClass]
    public class RequestControllerTest
    {
        [TestMethod]
        public void GetAllManagerRequestsTestOk()
        {
            IEnumerable<Request> requests = new List<Request>
            {
                new Request { Id = Guid.NewGuid() },
                new Request { Id = Guid.NewGuid() }
            };

            Mock<IRequestLogic> requestLogicMock = new Mock<IRequestLogic>(MockBehavior.Strict);
            requestLogicMock.Setup(r => r.GetAllRequests()).Returns(requests);

            RequestController requestController = new RequestController(requestLogicMock.Object);

            OkObjectResult expected = new OkObjectResult(new List<RequestResponseModel>
            {
                new RequestResponseModel(requests.First()),
                new RequestResponseModel(requests.Last())
            });
            List<RequestResponseModel> expectedObject = expected.Value as List<RequestResponseModel>;

            OkObjectResult result = requestController.GetAllManagerRequests() as OkObjectResult;
            List<RequestResponseModel> objectResult = result.Value as List<RequestResponseModel>;

            requestLogicMock.VerifyAll();
            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode) && expectedObject.First().Equals(objectResult.First()) && expectedObject.Last().Equals(objectResult.Last()));
        }
    }
}
