using BusinessLogic;
using CustomExceptions.BusinessLogic;
using Domain;
using Moq;
using RepositoryInterfaces;

namespace BusinessLogicTest
{
    [TestClass]
    public class ManagerRequestLogicTest
    {
        private Mock<IRequestRepository> requestRepositoryMock;
        private RequestLogic _managerRequestLogic;

        [TestInitialize]
        public void TestInitialize()
        {
            requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            _managerRequestLogic = new RequestLogic(requestRepositoryMock.Object);
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

            IEnumerable<Request> result = _managerRequestLogic.GetAllRequests();

            requestRepositoryMock.VerifyAll();
            Assert.IsTrue(result.SequenceEqual(requests));
        }

        [TestMethod]
        public void GetRequestByIdTest()
        {
            Request request = new Request() { Id = Guid.NewGuid(), Description = "Request 1" };

            requestRepositoryMock.Setup(r => r.GetRequestById(It.IsAny<Guid>())).Returns(request);
            
            Request result = _managerRequestLogic.GetRequestById(request.Id);

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

            Request result = _managerRequestLogic.UpdateRequest(request);

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
                _managerRequestLogic.UpdateRequest(request);
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
                _managerRequestLogic.UpdateRequest(request);
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
                _managerRequestLogic.UpdateRequest(request);
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
                _managerRequestLogic.UpdateRequest(request);
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
                _managerRequestLogic.UpdateRequest(request);
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

            Request result = _managerRequestLogic.CreateRequest(request);

            requestRepositoryMock.VerifyAll();
            Assert.AreEqual(request, result);
        }
    }
}
