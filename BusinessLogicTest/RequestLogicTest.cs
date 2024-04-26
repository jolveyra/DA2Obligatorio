using BusinessLogic;
using Domain;
using Moq;
using RepositoryInterfaces;

namespace BusinessLogicTest
{
    [TestClass]
    public class RequestLogicTest
    {
        [TestMethod]
        public void GetAllRequestsTest()
        {
            IEnumerable<Request> requests = new List<Request>()
            {
                new Request() { Id = Guid.NewGuid(), Description = "Request 1" },
                new Request() { Id = Guid.NewGuid(), Description = "Request 2" },
                new Request() { Id = Guid.NewGuid(), Description = "Request 3" }
            };

            Mock<IRequestRepository> requestRepositoryMock = new Mock<IRequestRepository>();
            requestRepositoryMock.Setup(r => r.GetAllRequests()).Returns(requests);
            RequestLogic requestLogic = new RequestLogic(requestRepositoryMock.Object);

            IEnumerable<Request> result = requestLogic.GetAllRequests();

            requestRepositoryMock.VerifyAll();
            Assert.IsTrue(result.SequenceEqual(requests));
        }
    }
}
