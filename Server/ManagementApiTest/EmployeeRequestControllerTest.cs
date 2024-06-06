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
            Building building = new Building() { Id = Guid.NewGuid(), Address = new Address() { CornerStreet = "Sth", DoorNumber = 65, Id = Guid.NewGuid(), Latitude = 0.01, Longitude = 0.10, Street = "Sth"}, ConstructorCompanyId = Guid.NewGuid(), MaintenanceEmployees = new List<Guid>(), ManagerId = Guid.NewGuid(), Name = "Sth", SharedExpenses = 10 };
            Flat flat = new Flat() { Id = Guid.NewGuid(), Bathrooms = 5, Building = building, Floor = 4, HasBalcony = true, Number = 403, Owner = new Person() { Email = "email", Id = Guid.NewGuid(), Name = "Sth", Surname = "sth" }, OwnerId = Guid.NewGuid(), Rooms = 4};
            IEnumerable<Request> requests = new List<Request>()
            {
                new Request() { Id = Guid.NewGuid(), Description = "Description 1", Flat = flat, Building = building, AssignedEmployee = new User() { Id = employeeId }, Category = new Category() { Id = Guid.NewGuid(), Name = "Category 1" } },
                new Request() { Id = Guid.NewGuid(), Description = "Description 2", Flat = flat, Building = building, AssignedEmployee = new User() { Id = Guid.NewGuid() }, Category = new Category() { Id = Guid.NewGuid(), Name = "Category 2" } }
            };

            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Items.Add("UserId", employeeId.ToString());

            ControllerContext controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            EmployeeRequestController anotherEmployeeRequestController = new EmployeeRequestController(requestLogicMock.Object) { ControllerContext = controllerContext };

            requestLogicMock.Setup(r => r.GetAllRequestsByEmployeeId(It.IsAny<Guid>())).Returns(new List<Request>() { requests.First() });
            
            OkObjectResult expected = new OkObjectResult(new List<RequestResponseWithoutEmployeeModel>
            {
                new RequestResponseWithoutEmployeeModel(requests.First())
            });
            List<RequestResponseWithoutEmployeeModel> expectedObject = expected.Value as List<RequestResponseWithoutEmployeeModel>;
            
            OkObjectResult result = anotherEmployeeRequestController.GetAllEmployeeRequests() as OkObjectResult;
            List<RequestResponseWithoutEmployeeModel> objectResult = result.Value as List<RequestResponseWithoutEmployeeModel>;

            requestLogicMock.VerifyAll();
            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode) && expectedObject.First().Equals(objectResult.First()));
        }

        [TestMethod]
        public void UpdateRequestStatusByIdTestOk()
        {
            Building building = new Building() { Id = Guid.NewGuid(), Address = new Address() { CornerStreet = "Sth", DoorNumber = 65, Id = Guid.NewGuid(), Latitude = 0.01, Longitude = 0.10, Street = "Sth"}, ConstructorCompanyId = Guid.NewGuid(), MaintenanceEmployees = new List<Guid>(), ManagerId = Guid.NewGuid(), Name = "Sth", SharedExpenses = 10 };
            Flat flat = new Flat() { Id = Guid.NewGuid(), Bathrooms = 5, Building = building, Floor = 4, HasBalcony = true, Number = 403, Owner = new Person() { Email = "email", Id = Guid.NewGuid(), Name = "Sth", Surname = "sth" }, OwnerId = Guid.NewGuid(), Rooms = 4};
            RequestUpdateStatusModel requestUpdateStatusModel = new RequestUpdateStatusModel() { Status = RequestStatus.InProgress.ToString() };

            Request expected = new Request
            {
                Id = Guid.NewGuid(),
                Status = RequestStatus.InProgress,
                Description = "Description",
                Flat = flat,
                Building = building,
                AssignedEmployee = new User() { Id = Guid.NewGuid() },
                Category = new Category() { Id = Guid.NewGuid(), Name = "Hola" }
            };

            requestLogicMock.Setup(r => r.UpdateRequestStatusById(It.IsAny<Guid>(), It.IsAny<RequestStatus>())).Returns(expected);
            
            RequestResponseWithoutEmployeeModel expectedResult = new RequestResponseWithoutEmployeeModel(expected);
            OkObjectResult expectedObjectResult = new OkObjectResult(expectedResult);

            OkObjectResult result = employeeRequestController.UpdateRequestStatusById(expected.Id, requestUpdateStatusModel) as OkObjectResult;
            RequestResponseWithoutEmployeeModel resultObject = result.Value as RequestResponseWithoutEmployeeModel;

            requestLogicMock.VerifyAll();
            Assert.IsTrue(expectedObjectResult.StatusCode.Equals(result.StatusCode) && expectedResult.Equals(resultObject));
        }
    }
}
