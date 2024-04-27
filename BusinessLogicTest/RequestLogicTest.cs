using BusinessLogic;
using CustomExceptions.BusinessLogic;
using Domain;
using Moq;
using RepositoryInterfaces;

namespace BusinessLogicTest
{
    [TestClass]
    public class RequestLogicTest
    {
        private Mock<IRequestRepository> requestRepositoryMock;
        private RequestLogic _requestLogic;

        [TestInitialize]
        public void TestInitialize()
        {
            requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            _requestLogic = new RequestLogic(requestRepositoryMock.Object);
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

            IEnumerable<Request> result = _requestLogic.GetAllRequests();

            requestRepositoryMock.VerifyAll();
            Assert.IsTrue(result.SequenceEqual(requests));
        }

        [TestMethod]
        public void GetRequestByIdTest()
        {
            Request request = new Request() { Id = Guid.NewGuid(), Description = "Request 1" };

            requestRepositoryMock.Setup(r => r.GetRequestById(It.IsAny<Guid>())).Returns(request);
            
            Request result = _requestLogic.GetRequestById(request.Id);

            requestRepositoryMock.VerifyAll();
            Assert.AreEqual(request, result);
        }

        [TestMethod]
        public void UpdateRequestTest()
        {
            Request request = new Request() { Id = Guid.NewGuid(), Description = "Request 1", BuildingId = Guid.NewGuid(), FlatId = Guid.NewGuid(), AssignedEmployeeId = Guid.NewGuid(), Category = new Category() };
            Request expected = new Request() { Id = request.Id, Description = "Request 2", BuildingId = Guid.NewGuid(), FlatId = Guid.NewGuid(), AssignedEmployeeId = Guid.NewGuid(), Category = new Category() };

            requestRepositoryMock.Setup(r => r.GetRequestById(It.IsAny<Guid>())).Returns(request);
            requestRepositoryMock.Setup(r => r.UpdateRequest(It.IsAny<Request>())).Returns(expected);

            Request result = _requestLogic.UpdateRequest(request);

            requestRepositoryMock.VerifyAll();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void UpdateRequestTest_InvalidDescription()
        {
            Request request = new Request() { Id = Guid.NewGuid(), BuildingId = Guid.NewGuid(), FlatId = Guid.NewGuid(), AssignedEmployeeId = Guid.NewGuid() };

            requestRepositoryMock.Setup(r => r.GetRequestById(It.IsAny<Guid>())).Returns(request);
            Exception exception = null;

            try
            {
                _requestLogic.UpdateRequest(request);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(RequestException));
            Assert.IsTrue(exception.Message.Equals("Description cannot be empty or null"));
        }

        [TestMethod]
        public void UpdateRequestTest_InvalidBuildingId()
        {
            Request request = new Request() { Id = Guid.NewGuid(), Description = "Request 1", FlatId = Guid.NewGuid(), AssignedEmployeeId = Guid.NewGuid() };

            requestRepositoryMock.Setup(r => r.GetRequestById(It.IsAny<Guid>())).Returns(request);
            Exception exception = null;

            try
            {
                _requestLogic.UpdateRequest(request);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(RequestException));
            Assert.IsTrue(exception.Message.Equals("BuildingId cannot be empty or null"));
        }

        [TestMethod]
        public void UpdateRequestTest_InvalidFlatId()
        {
            Request request = new Request() { Id = Guid.NewGuid(), Description = "Request 1", BuildingId = Guid.NewGuid(), AssignedEmployeeId = Guid.NewGuid() };

            requestRepositoryMock.Setup(r => r.GetRequestById(It.IsAny<Guid>())).Returns(request);
            Exception exception = null;

            try
            {
                _requestLogic.UpdateRequest(request);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(RequestException));
            Assert.IsTrue(exception.Message.Equals("FlatId cannot be empty or null"));
        }

        [TestMethod]
        public void UpdateRequestTest_InvalidAssignedEmployeeId()
        {
            Request request = new Request() { Id = Guid.NewGuid(), Description = "Request 1", BuildingId = Guid.NewGuid(), FlatId = Guid.NewGuid() };
            requestRepositoryMock.Setup(r => r.GetRequestById(It.IsAny<Guid>())).Returns(request);
            Exception exception = null;

            try
            {
                _requestLogic.UpdateRequest(request);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(RequestException));
            Assert.IsTrue(exception.Message.Equals("AssignedEmployeeId cannot be empty or null"));
        }

        [TestMethod]
        public void UpdateRequestTest_InvalidCategory()
        {
            Request request = new Request() { Id = Guid.NewGuid(), Description = "Request 1", BuildingId = Guid.NewGuid(), FlatId = Guid.NewGuid(), AssignedEmployeeId = Guid.NewGuid()};

            requestRepositoryMock.Setup(r => r.GetRequestById(It.IsAny<Guid>())).Returns(request);
            Exception exception = null;

            try
            {
                _requestLogic.UpdateRequest(request);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(RequestException));
            Assert.IsTrue(exception.Message.Equals("Category cannot be null"));
        }

        [TestMethod]
        public void CreateRequestTest()
        {
            Request request = new Request() { Id = Guid.NewGuid(), Description = "Request 1", BuildingId = Guid.NewGuid(), FlatId = Guid.NewGuid(), AssignedEmployeeId = Guid.NewGuid(), Category = new Category() };

            requestRepositoryMock.Setup(r => r.CreateRequest(It.IsAny<Request>())).Returns(request);

            Request result = _requestLogic.CreateRequest(request);

            requestRepositoryMock.VerifyAll();
            Assert.AreEqual(request, result);
        }

        [TestMethod]
        public void GetAllRequestsByEmployeeIdTest()
        {
            Guid employeeId = Guid.NewGuid();
            IEnumerable<Request> requests = new List<Request>()
            {
                new Request() { Id = Guid.NewGuid(), Description = "Request 1", AssignedEmployeeId = employeeId },
                new Request() { Id = Guid.NewGuid(), Description = "Request 2", AssignedEmployeeId = Guid.NewGuid() },
                new Request() { Id = Guid.NewGuid(), Description = "Request 3", AssignedEmployeeId = employeeId }
            };
            IEnumerable<Request> expected = new List<Request>()
            {
                requests.First(),
                requests.Last()
            };

            requestRepositoryMock.Setup(r => r.GetAllRequests()).Returns(requests);

            IEnumerable<Request> result = _requestLogic.GetAllRequestsByEmployeeId(employeeId);

            requestRepositoryMock.VerifyAll();
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [TestMethod]
        public void UpdateRequestStatusByIdTest()
        {
            Guid requestId = Guid.NewGuid();
            RequestStatus requestStatus = RequestStatus.InProgress;
            Request request = new Request() { Id = requestId, Description = "Request 1", BuildingId = Guid.NewGuid(), FlatId = Guid.NewGuid(), AssignedEmployeeId = Guid.NewGuid(), Category = new Category(), Status = requestStatus = RequestStatus.Pending };
            Request expected = new Request() { Id = requestId, Description = "Request 1", BuildingId = Guid.NewGuid(), FlatId = Guid.NewGuid(), AssignedEmployeeId = Guid.NewGuid(), Category = new Category(), Status = requestStatus };
            
            requestRepositoryMock.Setup(r => r.GetRequestById(It.IsAny<Guid>())).Returns(request);
            requestRepositoryMock.Setup(r => r.UpdateRequest(It.IsAny<Request>())).Returns(expected);

            Request result = _requestLogic.UpdateRequestStatusById(requestId, requestStatus);

            requestRepositoryMock.VerifyAll();
            Assert.AreEqual(expected, result);
        }
    }
}
