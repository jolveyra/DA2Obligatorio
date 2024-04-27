using Domain;
using LogicInterfaces;
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModels.RequestsModels;

namespace ManagementApiTest
{
    [TestClass]
    public class EmployeeRequestControllerTest
    {
        [TestMethod]
        public void GetAllEmployeeRequestTest()
        {
            Guid employeeId = Guid.NewGuid();
            IEnumerable<Request> requests = new List<Request>()
            {
                new Request() { Id = Guid.NewGuid(), Description = "Description 1", BuildingId = Guid.NewGuid(), FlatId = Guid.NewGuid(), Category = new Category() { Id = Guid.NewGuid(), Name = "Category 1" }, AssignedEmployeeId = employeeId },
                new Request() { Id = Guid.NewGuid(), Description = "Description 2", BuildingId = Guid.NewGuid(), FlatId = Guid.NewGuid(), Category = new Category() { Id = Guid.NewGuid(), Name = "Category 2" }, AssignedEmployeeId = Guid.NewGuid() }
            };

            Mock<IEmployeeRequestLogic> requestLogicMock = new Mock<IEmployeeRequestLogic>(MockBehavior.Strict);
            requestLogicMock.Setup(r => r.GetAllRequestsByEmployeeId(It.IsAny<Guid>())).Returns(new List<Request>() { requests.First() });
            EmployeeRequestController requestController = new EmployeeRequestController(requestLogicMock.Object);
            
            OkObjectResult expected = new OkObjectResult(new List<RequestResponseModel>
            {
                new RequestResponseModel(requests.First())
            });
            List<RequestResponseModel> expectedObject = expected.Value as List<RequestResponseModel>;
            
            OkObjectResult result = requestController.GetAllEmployeeRequests(employeeId) as OkObjectResult;
            List<RequestResponseModel> objectResult = result.Value as List<RequestResponseModel>;

            requestLogicMock.VerifyAll();
            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode) && expectedObject.SequenceEqual(objectResult));
        }
    }
}
