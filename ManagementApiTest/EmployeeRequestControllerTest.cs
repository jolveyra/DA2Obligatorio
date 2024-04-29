using Domain;
using LogicInterfaces;
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModels.RequestsModels;

namespace ManagementApiTest
{
    [TestClass]
    public class EmployeeRequestControllerTest
    {
        private Mock<IEmployeeRequestLogic> requestLogicMock;
        private EmployeeRequestController employeeRequestController;

        [TestInitialize]
        public void TestInitialize()
        {
            requestLogicMock = new Mock<IEmployeeRequestLogic>(MockBehavior.Strict);
            employeeRequestController = new EmployeeRequestController(requestLogicMock.Object);
        }

        [TestMethod]
        public void GetAllEmployeeRequestTestOk()
        {
            Guid employeeId = Guid.NewGuid();
            IEnumerable<Request> requests = new List<Request>()
            {
                new Request() { Id = Guid.NewGuid(), Description = "Description 1", Flat = new Flat(), AssignedEmployee = new User() { Id = employeeId, Role = Role.MaintenanceEmployee }, Category = new Category() { Id = Guid.NewGuid(), Name = "Category 1" } },
                new Request() { Id = Guid.NewGuid(), Description = "Description 2", Flat = new Flat(), AssignedEmployee = new User() { Id = Guid.NewGuid(), Role = Role.MaintenanceEmployee }, Category = new Category() { Id = Guid.NewGuid(), Name = "Category 2" } }
            };

            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Items.Add("UserId", employeeId.ToString());

            ControllerContext controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            EmployeeRequestController anotherEmployeeRequestController = new EmployeeRequestController(requestLogicMock.Object) { ControllerContext = controllerContext };

            requestLogicMock.Setup(r => r.GetAllRequestsByEmployeeId(It.IsAny<Guid>())).Returns(new List<Request>() { requests.First() });
            
            OkObjectResult expected = new OkObjectResult(new List<RequestResponseModel>
            {
                new RequestResponseModel(requests.First())
            });
            List<RequestResponseModel> expectedObject = expected.Value as List<RequestResponseModel>;
            
            OkObjectResult result = anotherEmployeeRequestController.GetAllEmployeeRequests() as OkObjectResult;
            List<RequestResponseModel> objectResult = result.Value as List<RequestResponseModel>;

            requestLogicMock.VerifyAll();
            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode) && expectedObject.SequenceEqual(objectResult));
        }

        [TestMethod]
        public void UpdateRequestStatusByIdTestOk()
        {
            RequestUpdateStatusModel requestUpdateStatusModel = new RequestUpdateStatusModel() { Status = RequestStatus.InProgress.ToString() };

            Request expected = new Request
            {
                Id = Guid.NewGuid(),
                Status = RequestStatus.InProgress,
                Description = "Description",
                Flat = new Flat() { Id = Guid.NewGuid() },
                AssignedEmployee = new User() { Id = Guid.NewGuid() },
            };

            requestLogicMock.Setup(r => r.UpdateRequestStatusById(It.IsAny<Guid>(), It.IsAny<RequestStatus>())).Returns(expected);
            
            RequestResponseModel expectedResult = new RequestResponseModel(expected);
            OkObjectResult expectedObjectResult = new OkObjectResult(expectedResult);

            OkObjectResult result = employeeRequestController.UpdateRequestStatusById(expected.Id, requestUpdateStatusModel) as OkObjectResult;
            RequestResponseModel resultObject = result.Value as RequestResponseModel;

            requestLogicMock.VerifyAll();
            Assert.IsTrue(expectedObjectResult.StatusCode.Equals(result.StatusCode) && expectedResult.Equals(resultObject));
        }
    }
}
