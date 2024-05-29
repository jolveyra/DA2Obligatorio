using BusinessLogic;
using CustomExceptions;
using Domain;
using Moq;
using RepositoryInterfaces;

namespace BusinessLogicTest
{
    [TestClass]
    public class RequestLogicTest
    {
        private Mock<IRequestRepository> requestRepositoryMock;
        private Mock<IBuildingRepository> buildingRepositoryMock;
        private Mock<IUserRepository> userRepositoryMock;
        private RequestLogic _requestLogic;

        [TestInitialize]
        public void TestInitialize()
        {
            requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            buildingRepositoryMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
            userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            _requestLogic = new RequestLogic(requestRepositoryMock.Object, buildingRepositoryMock.Object, userRepositoryMock.Object);
        }

        [TestMethod]
        public void GetAllManagerRequestsTest()
        {
            Guid managerId = Guid.NewGuid();
            IEnumerable<Request> requests = new List<Request>()
            {
                new Request() { Id = Guid.NewGuid(), Description = "Request 1", ManagerId = managerId, Flat = new Flat() { Building = new Building() { Manager = new User() { Role = Role.Manager, Id = managerId } } } },
                new Request() { Id = Guid.NewGuid(), Description = "Request 2", ManagerId = Guid.NewGuid(), Flat = new Flat() { Building = new Building() { Manager = new User() { Role = Role.Manager, Id = Guid.NewGuid() } } } },
                new Request() { Id = Guid.NewGuid(), Description = "Request 3", ManagerId = managerId, Flat = new Flat() { Building = new Building() { Manager = new User() { Role = Role.Manager, Id = managerId } } } }
            };

            userRepositoryMock.Setup(u => u.GetUserById(It.IsAny<Guid>())).Returns(new User() { Id = managerId, Role = Role.Manager });
            requestRepositoryMock.Setup(r => r.GetAllRequests()).Returns(requests);

            IEnumerable<Request> result = _requestLogic.GetAllManagerRequests(managerId);

            requestRepositoryMock.VerifyAll();
            userRepositoryMock.VerifyAll();
            Assert.IsTrue(result.SequenceEqual(new List<Request>() { requests.ToList()[0], requests.ToList()[2] }));
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
            Request request = new Request() { Id = Guid.NewGuid(), Description = "Request 1", Flat = new Flat(), BuildingId = Guid.NewGuid(), AssignedEmployeeId = Guid.NewGuid(), Category = new Category() };
            Request expected = new Request() { Id = request.Id, Description = "Request 2", Flat = new Flat(), BuildingId = Guid.NewGuid(), AssignedEmployeeId = Guid.NewGuid(), Category = new Category() };

            requestRepositoryMock.Setup(r => r.GetRequestById(It.IsAny<Guid>())).Returns(request);
            requestRepositoryMock.Setup(r => r.UpdateRequest(It.IsAny<Request>())).Returns(expected);

            Request result = _requestLogic.UpdateRequest(request);

            requestRepositoryMock.VerifyAll();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void UpdateRequestTest_InvalidDescription()
        {
            Request request = new Request() { Id = Guid.NewGuid(), Flat = new Flat(), AssignedEmployeeId = Guid.NewGuid() };

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

            requestRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(RequestException));
            Assert.IsTrue(exception.Message.Equals("Description cannot be empty or null"));
        }

        [TestMethod]
        public void UpdateRequestTest_InvalidFlat()
        {
            Request request = new Request() { Id = Guid.NewGuid(), Description = "Request 1", AssignedEmployeeId = Guid.NewGuid() };

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

            requestRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(RequestException));
            Assert.IsTrue(exception.Message.Equals("Flat cannot be empty or null"));
        }

        [TestMethod]
        public void UpdateRequestTest_InvalidBuilding()
        {
            Request request = new Request() { Id = Guid.NewGuid(), Description = "Request 1", AssignedEmployeeId = Guid.NewGuid(), Flat = new Flat() };

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

            requestRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(RequestException));
            Assert.IsTrue(exception.Message.Equals("BuildingId cannot be empty or null"));
        }

        [TestMethod]
        public void UpdateRequestTest_InvalidAssignedEmployee()
        {
            Request request = new Request() { Id = Guid.NewGuid(), Description = "Request 1", Flat = new Flat(), BuildingId = Guid.NewGuid() };
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

            requestRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(RequestException));
            Assert.IsTrue(exception.Message.Equals("AssignedEmployee cannot be empty or null"));
        }

        [TestMethod]
        public void UpdateRequestTest_InvalidCategory()
        {
            Request request = new Request() { Id = Guid.NewGuid(), Description = "Request 1", Flat = new Flat(), BuildingId = Guid.NewGuid(), AssignedEmployeeId = Guid.NewGuid() };

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

            requestRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(RequestException));
            Assert.IsTrue(exception.Message.Equals("Category cannot be null"));
        }

        [TestMethod]
        public void CreateRequestTest()
        {
            Request request = new Request() { Id = Guid.NewGuid(), Description = "Request 1", Flat = new Flat(), BuildingId = Guid.NewGuid(), AssignedEmployeeId = Guid.NewGuid(), Category = new Category() };

            requestRepositoryMock.Setup(r => r.CreateRequest(It.IsAny<Request>())).Returns(request);

            Request result = _requestLogic.CreateRequest(request, It.IsAny<Guid>());

            requestRepositoryMock.VerifyAll();
            Assert.AreEqual(request, result);
        }

        [TestMethod]
        public void GetAllRequestsByEmployeeIdTest()
        {
            Guid employeeId = Guid.NewGuid();
            IEnumerable<Request> requests = new List<Request>()
            {
                new Request() { Id = Guid.NewGuid(), Description = "Request 1", Flat = new Flat(), AssignedEmployeeId = employeeId },
                new Request() { Id = Guid.NewGuid(), Description = "Request 2", Flat = new Flat(), AssignedEmployeeId = Guid.NewGuid() },
                new Request() { Id = Guid.NewGuid(), Description = "Request 3", Flat = new Flat(), AssignedEmployeeId = employeeId }
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
        public void UpdateRequestStatusInProgressByIdTest()
        {
            Guid requestId = Guid.NewGuid();
            RequestStatus requestStatus = RequestStatus.InProgress;
            Request request = new Request() { Id = requestId, Description = "Request 1", Flat = new Flat(), BuildingId = Guid.NewGuid(), AssignedEmployeeId = Guid.NewGuid(), Category = new Category(), Status = requestStatus = RequestStatus.Pending };
            Request expected = new Request() { Id = requestId, Description = "Request 1", Flat = new Flat(), BuildingId = Guid.NewGuid(), AssignedEmployeeId = Guid.NewGuid(), Category = new Category(), Status = requestStatus, StartingDate = DateTime.Now };
            
            requestRepositoryMock.Setup(r => r.GetRequestById(It.IsAny<Guid>())).Returns(request);
            requestRepositoryMock.Setup(r => r.UpdateRequest(It.IsAny<Request>())).Returns(expected);

            Request result = _requestLogic.UpdateRequestStatusById(requestId, requestStatus);

            requestRepositoryMock.VerifyAll();
            Assert.IsTrue(expected.Equals(result) && (result.StartingDate - DateTime.Now).Hours == 0);
        }

        [TestMethod]
        public void UpdateRequestStatusCompletedByIdTest()
        {
            Guid requestId = Guid.NewGuid();
            RequestStatus requestStatus = RequestStatus.Completed;
            Request request = new Request() { Id = requestId, Description = "Request 1", Flat = new Flat(), BuildingId = Guid.NewGuid(), AssignedEmployeeId = Guid.NewGuid(), Category = new Category(), Status = requestStatus, StartingDate = DateTime.Now };
            Request expected = new Request() { Id = requestId, Description = "Request 1", Flat = new Flat(), BuildingId = Guid.NewGuid(), AssignedEmployeeId = Guid.NewGuid(), Category = new Category(), Status = requestStatus, StartingDate = DateTime.Now, CompletionDate = DateTime.Now };
            
            requestRepositoryMock.Setup(r => r.GetRequestById(It.IsAny<Guid>())).Returns(request);
            requestRepositoryMock.Setup(r => r.UpdateRequest(It.IsAny<Request>())).Returns(expected);

            Request result = _requestLogic.UpdateRequestStatusById(requestId, requestStatus);

            requestRepositoryMock.VerifyAll();
            Assert.IsTrue(expected.Equals(result) && (result.CompletionDate - DateTime.Now).Hours == 0);
        }

        [TestMethod]
        public void UpdateCompletedRequestTest()
        {
            Request request = new Request() { Id = Guid.NewGuid(), Description = "Request 1", Status = RequestStatus.Completed, Flat = new Flat(), AssignedEmployeeId = Guid.NewGuid() };

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

            requestRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(RequestException));
            Assert.IsTrue(exception.Message.Equals("Cannot update completed request"));
        }
    }
}
