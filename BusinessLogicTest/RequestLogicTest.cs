using BusinessLogic;
using Domain;
using Moq;
using RepositoryInterfaces;

namespace BusinessLogicTest
{
    [TestClass]
    public class RequestLogicTest
    {
        private Mock<IRequestRepository> requestRepositoryMock;
        private RequestLogic requestLogic;

        [TestInitialize]
        public void TestInitialize()
        {
            requestRepositoryMock = new Mock<IRequestRepository>();
            requestLogic = new RequestLogic(requestRepositoryMock.Object);
        }

        [TestMethod]
        public void GetAllRequestsTest()
        {
            IEnumerable<Request> requests = new List<Request>()
            {
                new Request() { Id = Guid.NewGuid(), Description = "Request 1" },
                new Request() { Id = Guid.NewGuid(), Description = "Request 2" },
                new Request() { Id = Guid.NewGuid(), Description = "Request 3" }
            };

            requestRepositoryMock.Setup(r => r.GetAllRequests()).Returns(requests);

            IEnumerable<Request> result = requestLogic.GetAllRequests();

            requestRepositoryMock.VerifyAll();
            Assert.IsTrue(result.SequenceEqual(requests));
        }

        [TestMethod]
        public void GetRequestByIdTest()
        {
            Request request = new Request() { Id = Guid.NewGuid(), Description = "Request 1" };

            requestRepositoryMock.Setup(r => r.GetRequestById(It.IsAny<Guid>())).Returns(request);
            
            Request result = requestLogic.GetRequestById(request.Id);

            requestRepositoryMock.VerifyAll();
            Assert.AreEqual(request, result);
        }

        [TestMethod]
        public void UpdateRequestTest()
        {
            Request request = new Request() { Id = Guid.NewGuid(), Description = "Request 1", BuildingId = Guid.NewGuid(), FlatId = Guid.NewGuid(), AssignedEmployeeId = Guid.NewGuid() };
            Request expected = new Request() { Id = request.Id, Description = "Request 2", BuildingId = Guid.NewGuid(), FlatId = Guid.NewGuid(), AssignedEmployeeId = Guid.NewGuid() };

            requestRepositoryMock.Setup(r => r.GetRequestById(It.IsAny<Guid>())).Returns(request);
            requestRepositoryMock.Setup(r => r.UpdateRequest(It.IsAny<Request>())).Returns(expected);

            Request result = requestLogic.UpdateRequest(request);

            requestRepositoryMock.VerifyAll();
            Assert.AreEqual(expected, result);
        }
    }
}
