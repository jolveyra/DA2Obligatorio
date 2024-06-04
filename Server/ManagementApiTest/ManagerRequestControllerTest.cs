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
    public class ManagerRequestControllerTest
    {
        private Mock<IManagerRequestLogic> requestLogicMock;
        private ManagerRequestController managerRequestController;

        [TestInitialize]
        public void TestInitialize()
        {
            requestLogicMock = new Mock<IManagerRequestLogic>(MockBehavior.Strict);
            managerRequestController = new ManagerRequestController(requestLogicMock.Object);
        }

        [TestMethod]
        public void GetAllManagerRequestsTestOk()
        {
            Building building = new Building() { Id = Guid.NewGuid(), Address = new Address() { CornerStreet = "Sth", DoorNumber = 65, Id = Guid.NewGuid(), Latitude = 0.01, Longitude = 0.10, Street = "Sth"}, ConstructorCompanyId = Guid.NewGuid(), MaintenanceEmployees = new List<Guid>(), ManagerId = Guid.NewGuid(), Name = "Sth", SharedExpenses = 10 };
            Flat flat = new Flat() { Id = Guid.NewGuid(), Bathrooms = 5, Building = building, Floor = 4, HasBalcony = true, Number = 403, Owner = new Person() { Email = "email", Id = Guid.NewGuid(), Name = "Sth", Surname = "sth" }, OwnerId = Guid.NewGuid(), Rooms = 4};
            User assignedEmployee = new User() { Email = "sth", Id = Guid.NewGuid(), Name = "sth" , Role = Role.MaintenanceEmployee, Password = "sth", Surname = "sth" };

            Guid managerId = Guid.NewGuid();
            IEnumerable<Request> requests = new List<Request>
            {
                new Request { Id = Guid.NewGuid(), Category = new Category() { Name = "Plumbing" }, Flat = flat, Building = building, AssignedEmployee = assignedEmployee  },
                new Request { Id = Guid.NewGuid(), Category = new Category() { Name = "Plumbing" }, Flat = flat, Building = building, AssignedEmployee = assignedEmployee }
            };

            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Items.Add("UserId", managerId.ToString());

            ControllerContext controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            ManagerRequestController anotherManagerRequestController = new ManagerRequestController(requestLogicMock.Object) { ControllerContext = controllerContext };

            requestLogicMock.Setup(r => r.GetAllManagerRequests(It.IsAny<Guid>())).Returns(requests);

            OkObjectResult expected = new OkObjectResult(new List<RequestResponseModel>
            {
                new RequestResponseModel(requests.First()),
                new RequestResponseModel(requests.Last())
            });
            List<RequestResponseModel> expectedObject = expected.Value as List<RequestResponseModel>;

            OkObjectResult result = anotherManagerRequestController.GetAllManagerRequests(It.IsAny<string>()) as OkObjectResult;
            List<RequestResponseModel> objectResult = result.Value as List<RequestResponseModel>;

            requestLogicMock.VerifyAll();
            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode) && expectedObject.First().Equals(objectResult.First()) && expectedObject.Last().Equals(objectResult.Last()));
        }

        [TestMethod]
        public void GetAllManagerRequestsByCategoryTestOk()
        {
            Building building = new Building() { Id = Guid.NewGuid(), Address = new Address() { CornerStreet = "Sth", DoorNumber = 65, Id = Guid.NewGuid(), Latitude = 0.01, Longitude = 0.10, Street = "Sth"}, ConstructorCompanyId = Guid.NewGuid(), MaintenanceEmployees = new List<Guid>(), ManagerId = Guid.NewGuid(), Name = "Sth", SharedExpenses = 10 };
            Flat flat = new Flat() { Id = Guid.NewGuid(), Bathrooms = 5, Building = building, Floor = 4, HasBalcony = true, Number = 403, Owner = new Person() { Email = "email", Id = Guid.NewGuid(), Name = "Sth", Surname = "sth" }, OwnerId = Guid.NewGuid(), Rooms = 4};
            User assignedEmployee = new User() { Email = "sth", Id = Guid.NewGuid(), Name = "sth" , Role = Role.MaintenanceEmployee, Password = "sth", Surname = "sth" };
            Guid managerId = Guid.NewGuid();
            Category category = new Category { Name = "Electricity" };
            IEnumerable<Request> requests = new List<Request>
            {
                new Request { Id = Guid.NewGuid(), Category = category, Building = building, Flat = flat, AssignedEmployee = assignedEmployee },
                new Request {Id = Guid.NewGuid(), Category = new Category() { Name = "Plumbing" }, Building = building, Flat = flat, AssignedEmployee = assignedEmployee }
            };

            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Items.Add("UserId", managerId.ToString());

            ControllerContext controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            ManagerRequestController anotherManagerRequestController = new ManagerRequestController(requestLogicMock.Object) { ControllerContext = controllerContext };

            requestLogicMock.Setup(r => r.GetAllManagerRequests(It.IsAny<Guid>())).Returns(requests);

            OkObjectResult expected = new OkObjectResult(new List<RequestResponseModel>
            {
                new RequestResponseModel(requests.First())
            });
            List<RequestResponseModel> expectedObject = expected.Value as List<RequestResponseModel>;

            OkObjectResult result = anotherManagerRequestController.GetAllManagerRequests(category.Name) as OkObjectResult;
            List<RequestResponseModel> objectResult = result.Value as List<RequestResponseModel>;

            requestLogicMock.VerifyAll();
            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode) && expectedObject.First().Equals(objectResult.First()));
        }

        [TestMethod]
        public void GetRequestByIdTestOk()
        {
            Building building = new Building() { Id = Guid.NewGuid(), Address = new Address() { CornerStreet = "Sth", DoorNumber = 65, Id = Guid.NewGuid(), Latitude = 0.01, Longitude = 0.10, Street = "Sth"}, ConstructorCompanyId = Guid.NewGuid(), MaintenanceEmployees = new List<Guid>(), ManagerId = Guid.NewGuid(), Name = "Sth", SharedExpenses = 10 };
            Flat flat = new Flat() { Id = Guid.NewGuid(), Bathrooms = 5, Building = building, Floor = 4, HasBalcony = true, Number = 403, Owner = new Person() { Email = "email", Id = Guid.NewGuid(), Name = "Sth", Surname = "sth" }, OwnerId = Guid.NewGuid(), Rooms = 4};
            User assignedEmployee = new User() { Email = "sth", Id = Guid.NewGuid(), Name = "sth" , Role = Role.MaintenanceEmployee, Password = "sth", Surname = "sth" };
            IEnumerable<Request> requests = new List<Request>
            {
                new Request { Id = Guid.NewGuid(), Category = new Category { Name = "Electricity" }, Flat = flat, Building = building, AssignedEmployee = assignedEmployee },
                new Request { Id = Guid.NewGuid(), Category = new Category() { Name = "Plumbing" }, Flat = new Flat() { Id = Guid.NewGuid() }, AssignedEmployee = new User() { Id = Guid.NewGuid() } }
            };

            requestLogicMock.Setup(r => r.GetRequestById(It.IsAny<Guid>())).Returns(requests.First());

            OkObjectResult expected = new OkObjectResult(new RequestResponseModel(requests.First()));
            RequestResponseModel expectedObject = expected.Value as RequestResponseModel;

            OkObjectResult result = managerRequestController.GetRequestById(requests.First().Id) as OkObjectResult;
            RequestResponseModel objectResult = result.Value as RequestResponseModel;

            requestLogicMock.VerifyAll();
            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode) && expectedObject.Equals(objectResult));
        }

        [TestMethod]
        public void CreateRequestTestCreated()
        {
            Building building = new Building() { Id = Guid.NewGuid(), Address = new Address() { CornerStreet = "Sth", DoorNumber = 65, Id = Guid.NewGuid(), Latitude = 0.01, Longitude = 0.10, Street = "Sth"}, ConstructorCompanyId = Guid.NewGuid(), MaintenanceEmployees = new List<Guid>(), ManagerId = Guid.NewGuid(), Name = "Sth", SharedExpenses = 10 };
            Flat flat = new Flat() { Id = Guid.NewGuid(), Bathrooms = 5, Building = building, Floor = 4, HasBalcony = true, Number = 403, Owner = new Person() { Email = "email", Id = Guid.NewGuid(), Name = "Sth", Surname = "sth" }, OwnerId = Guid.NewGuid(), Rooms = 4};
            User assignedEmployee = new User() { Email = "sth", Id = Guid.NewGuid(), Name = "sth" , Role = Role.MaintenanceEmployee, Password = "sth", Surname = "sth" };

            RequestCreateModel requestCreateModel = new RequestCreateModel
            {
                Description = "Broken pipe",
                FlatId = Guid.NewGuid(),
                CategoryName = "Plumbing"
            };
            Request expected = new Request
            {
                Description = requestCreateModel.Description,
                Flat = flat,
                Building = building,
                Category = new Category { Name = requestCreateModel.CategoryName },
                AssignedEmployee = assignedEmployee,
                ManagerId = Guid.NewGuid()
            };

            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Items.Add("UserId", expected.ManagerId.ToString());

            ControllerContext controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            ManagerRequestController anotherManagerRequestController = new ManagerRequestController(requestLogicMock.Object) { ControllerContext = controllerContext };
            requestLogicMock.Setup(r => r.CreateRequest(It.IsAny<Request>(), It.IsAny<Guid>())).Returns(expected);

            RequestResponseModel expectedResult = new RequestResponseModel(expected); 
            CreatedAtActionResult expectedObjectResult = new CreatedAtActionResult("CreateRequest", "CreateRequest", new { Id = expected.Id }, expectedResult);

            CreatedAtActionResult result = anotherManagerRequestController.CreateRequest(requestCreateModel) as CreatedAtActionResult;
            RequestResponseModel resultObject = result.Value as RequestResponseModel;

            requestLogicMock.VerifyAll();
            Assert.IsTrue(expectedObjectResult.StatusCode.Equals(result.StatusCode) && expectedResult.Equals(resultObject));
        }

        [TestMethod]
        public void UpdateRequestByIdTestOk()
        {
            Building building = new Building() { Id = Guid.NewGuid(), Address = new Address() { CornerStreet = "Sth", DoorNumber = 65, Id = Guid.NewGuid(), Latitude = 0.01, Longitude = 0.10, Street = "Sth"}, ConstructorCompanyId = Guid.NewGuid(), MaintenanceEmployees = new List<Guid>(), ManagerId = Guid.NewGuid(), Name = "Sth", SharedExpenses = 10 };
            Flat flat = new Flat() { Id = Guid.NewGuid(), Bathrooms = 5, Building = building, Floor = 4, HasBalcony = true, Number = 403, Owner = new Person() { Email = "email", Id = Guid.NewGuid(), Name = "Sth", Surname = "sth" }, OwnerId = Guid.NewGuid(), Rooms = 4};
            User assignedEmployee = new User() { Email = "sth", Id = Guid.NewGuid(), Name = "sth" , Role = Role.MaintenanceEmployee, Password = "sth", Surname = "sth" };
            RequestUpdateModel requestUpdateModel = new RequestUpdateModel
            {
                Description = "Broken pipe",
                CategoryName = "Plumbing",
                AssignedEmployeeId = Guid.NewGuid()
            };
            Request expected = new Request
            {
                Id = Guid.NewGuid(),
                Description = requestUpdateModel.Description,
                Flat = flat,
                Building = building,
                Category = new Category { Name = requestUpdateModel.CategoryName },
                AssignedEmployee = assignedEmployee
            };
            requestLogicMock.Setup(r => r.UpdateRequest(It.IsAny<Request>())).Returns(expected);

            RequestResponseModel expectedResult = new RequestResponseModel(expected);
            OkObjectResult expectedObjectResult = new OkObjectResult(expectedResult);

            OkObjectResult result = managerRequestController.UpdateRequestById(expected.Id, requestUpdateModel) as OkObjectResult;
            RequestResponseModel resultObject = result.Value as RequestResponseModel;

            requestLogicMock.VerifyAll();
            Assert.IsTrue(expectedObjectResult.StatusCode.Equals(result.StatusCode) && expectedResult.Equals(resultObject));
        }
    }
}
