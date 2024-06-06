using Domain;
using ManagementApi.Controllers;
using LogicInterfaces;
using Moq;
using WebModels.BuildingModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ManagementApiTest
{
    [TestClass]
    public class ConstructorCompanyBuildingControllerTest
    {
        private Mock<IConstructorCompanyBuildingLogic> constructorCompanyBuildingLogicMock;
        private ConstructorCompanyBuildingController constructorCompanyBuildingController;

        [TestInitialize]
        public void TestInitialize()
        {
            constructorCompanyBuildingLogicMock = new Mock<IConstructorCompanyBuildingLogic>(MockBehavior.Strict);
            constructorCompanyBuildingController = new ConstructorCompanyBuildingController(constructorCompanyBuildingLogicMock.Object);
        }


        [TestMethod]
        public void GetAllConstructorCompanyBuildingsTestOk()
        {
            ConstructorCompany constructorCompany = new ConstructorCompany() { Id = Guid.NewGuid(), Name = "Constructor Company 1" };
            IEnumerable<Building> constructorCompanyBuildings = new List<Building>
            {
                new Building() {
                    Id = Guid.NewGuid(),
                    Name = "Building 1",
                    ConstructorCompanyId = constructorCompany.Id,
                    Address = new Address { DoorNumber = 21, CornerStreet = "Street 1", Id = Guid.NewGuid(), Latitude = 23, Longitude = 24, Street = "Street 2" },
                    SharedExpenses = 13,
                    MaintenanceEmployees = new List<Guid>(),
                    ManagerId = Guid.NewGuid()
                },
                new Building() { Id = Guid.NewGuid(), Name = "Building 2", ConstructorCompanyId = constructorCompany.Id,
                    Address = new Address { DoorNumber = 22, CornerStreet = "Street 2", Id = Guid.NewGuid(), Latitude = 25, Longitude = 26, Street = "Street 3" },
                    SharedExpenses = 14,
                    MaintenanceEmployees = new List<Guid>(),
                    ManagerId = Guid.NewGuid()
                }
            };


            ConstructorCompanyAdministrator constructorCompanyAdministrator = new ConstructorCompanyAdministrator()
            {
                Id = Guid.NewGuid(),
                Name = "Administrator 1",
                ConstructorCompanyId = constructorCompany.Id
            };

            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Items.Add("UserId", constructorCompanyAdministrator.Id.ToString());

            ControllerContext controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            ConstructorCompanyBuildingController anotherConstructorCompanyBuildingController = new ConstructorCompanyBuildingController(constructorCompanyBuildingLogicMock.Object) { ControllerContext = controllerContext };

            constructorCompanyBuildingLogicMock.Setup(c => c.GetAllConstructorCompanyBuildings(It.IsAny<Guid>())).Returns(constructorCompanyBuildings);

            OkObjectResult expected = new OkObjectResult(new List<BuildingWithoutFlatsResponseModel>
            {
                new BuildingWithoutFlatsResponseModel(constructorCompanyBuildings.First()),
                new BuildingWithoutFlatsResponseModel(constructorCompanyBuildings.Last())
            });

            List<BuildingWithoutFlatsResponseModel> expectedObject = expected.Value as List<BuildingWithoutFlatsResponseModel>;

            OkObjectResult result = anotherConstructorCompanyBuildingController.GetAllConstructorCompanyBuildings() as OkObjectResult;
            List<BuildingWithoutFlatsResponseModel> objectResult = result.Value as List<BuildingWithoutFlatsResponseModel>;

            constructorCompanyBuildingLogicMock.VerifyAll();
            Assert.AreEqual(expected.StatusCode, result.StatusCode);
            CollectionAssert.AreEqual(expectedObject, objectResult);
        }

        [TestMethod]
        public void CreateConstructorCompanyBuildingTestOk()
        {

            BuildingRequestModel buildingRequestModel = new BuildingRequestModel()
            {
                Name = "Building 1",
                AmountOfFlats = 12,
                DoorNumber = 21,
                CornerStreet = "Street 1",
                Street = "Street 2",
                Latitude = 23,
                Longitude = 24,
                SharedExpenses = 13
            };
            ConstructorCompanyAdministrator constructorCompanyAdministrator = new ConstructorCompanyAdministrator()
            {
                Id = Guid.NewGuid(),
                Name = "Administrator 1",
                ConstructorCompanyId = Guid.NewGuid()
            };

            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Items.Add("UserId", constructorCompanyAdministrator.Id.ToString());

            ControllerContext controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            Building building = buildingRequestModel.ToEntity();
            building.Id = Guid.NewGuid();
            building.ConstructorCompanyId = constructorCompanyAdministrator.ConstructorCompanyId;
            building.ManagerId = Guid.NewGuid();

            constructorCompanyBuildingLogicMock.Setup(c => c.CreateConstructorCompanyBuilding(It.IsAny<Building>(), It.IsAny<int>(), It.IsAny<Guid>())).Returns(building);

            OkObjectResult expectedObject = new OkObjectResult(new BuildingWithoutFlatsResponseModel(building));

            ConstructorCompanyBuildingController anotherConstructorCompanyBuildingController = new ConstructorCompanyBuildingController(constructorCompanyBuildingLogicMock.Object) { ControllerContext = controllerContext };

            OkObjectResult result = anotherConstructorCompanyBuildingController.CreateConstructorCompanyBuilding(buildingRequestModel) as OkObjectResult;

            constructorCompanyBuildingLogicMock.VerifyAll();
            Assert.AreEqual(expectedObject.StatusCode, result.StatusCode);
            Assert.AreEqual(expectedObject.Value, result.Value);
        }

        [TestMethod]
        public void GetConstructorCompanyBuildingByIdTestOk()
        {
            Guid id = Guid.NewGuid();
            ConstructorCompany constructorCompany = new ConstructorCompany() { Id = Guid.NewGuid(), Name = "Constructor Company 1" };
            Building building = new Building()
            {
                Id = id,
                Name = "Building 1",
                ConstructorCompanyId = constructorCompany.Id,
                Address = new Address { DoorNumber = 21, CornerStreet = "Street 1", Id = Guid.NewGuid(), Latitude = 23, Longitude = 24, Street = "Street 2" },
                SharedExpenses = 13,
                MaintenanceEmployees = new List<Guid>(),
                ManagerId = Guid.NewGuid()
            };

            ConstructorCompanyAdministrator constructorCompanyAdministrator = new ConstructorCompanyAdministrator()
            {
                Id = Guid.NewGuid(),
                Name = "Administrator 1",
                ConstructorCompanyId = constructorCompany.Id
            };

            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Items.Add("UserId", constructorCompanyAdministrator.Id.ToString());

            ControllerContext controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            constructorCompanyBuildingLogicMock.Setup(c => c.GetConstructorCompanyBuildingById(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(building);

            OkObjectResult expectedObject = new OkObjectResult(new BuildingWithoutFlatsResponseModel(building));

            ConstructorCompanyBuildingController anotherConstructorCompanyBuildingController = new ConstructorCompanyBuildingController(constructorCompanyBuildingLogicMock.Object) { ControllerContext = controllerContext };

            OkObjectResult result = anotherConstructorCompanyBuildingController.GetConstructorCompanyBuildingById(id) as OkObjectResult;

            constructorCompanyBuildingLogicMock.VerifyAll();
            Assert.AreEqual(expectedObject.StatusCode, result.StatusCode);
            Assert.AreEqual(expectedObject.Value, result.Value);
        }

        [TestMethod]
        public void UpdateConstructorCompanyBuildingTestOk()
        {
            Guid id = Guid.NewGuid();
            ConstructorCompany constructorCompany = new ConstructorCompany() { Id = Guid.NewGuid(), Name = "Constructor Company 1" };

            User newManager = new User() { Id = Guid.NewGuid(), Name = "ManagerId 2", Role = Role.Manager };

            Building building = new Building()
            {
                Id = id,
                Name = "Building 1",
                ConstructorCompanyId = constructorCompany.Id,
                Address = new Address { DoorNumber = 21, CornerStreet = "Street 1", Id = Guid.NewGuid(), Latitude = 23, Longitude = 24, Street = "Street 2" },
                SharedExpenses = 13,
                MaintenanceEmployees = new List<Guid>(),
                ManagerId = newManager.Id
            };


            UpdateConstructorCompanyBuildingRequestModel updateConstructorCompanyBuildingRequestModel = new UpdateConstructorCompanyBuildingRequestModel()
            {
                Name = "Building 1",
                ManagerId = newManager.Id
            };

            ConstructorCompanyAdministrator constructorCompanyAdministrator = new ConstructorCompanyAdministrator()
            {
                Id = Guid.NewGuid(),
                Name = "Administrator 1",
                ConstructorCompanyId = constructorCompany.Id
            };

            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Items.Add("UserId", constructorCompanyAdministrator.Id.ToString());

            ControllerContext controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            constructorCompanyBuildingLogicMock.Setup(c => c.UpdateConstructorCompanyBuilding(It.IsAny<Building>(), It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(building);

            OkObjectResult expectedObject = new OkObjectResult(new BuildingWithoutFlatsResponseModel(building));

            ConstructorCompanyBuildingController anotherConstructorCompanyBuildingController = new ConstructorCompanyBuildingController(constructorCompanyBuildingLogicMock.Object) { ControllerContext = controllerContext };

            OkObjectResult result = anotherConstructorCompanyBuildingController.UpdateConstructorCompanyBuilding(id, updateConstructorCompanyBuildingRequestModel) as OkObjectResult;

            constructorCompanyBuildingLogicMock.VerifyAll();
            Assert.AreEqual(expectedObject.StatusCode, result.StatusCode);
            Assert.AreEqual(expectedObject.Value, result.Value);
        }

        [TestMethod]
        public void DeleteConstructorCompanyBuildingTestOk()
        {
            Guid id = Guid.NewGuid();
            ConstructorCompany constructorCompany = new ConstructorCompany() { Id = Guid.NewGuid(), Name = "Constructor Company 1" };
            Building building = new Building()
            {
                Id = id,
                Name = "Building 1",
                ConstructorCompanyId = constructorCompany.Id,
                Address = new Address { DoorNumber = 21, CornerStreet = "Street 1", Id = Guid.NewGuid(), Latitude = 23, Longitude = 24, Street = "Street 2" },
                SharedExpenses = 13,
                MaintenanceEmployees = new List<Guid>()
            };

            ConstructorCompanyAdministrator constructorCompanyAdministrator = new ConstructorCompanyAdministrator()
            {
                Id = Guid.NewGuid(),
                Name = "Administrator 1",
                ConstructorCompanyId = constructorCompany.Id
            };

            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Items.Add("UserId", constructorCompanyAdministrator.Id.ToString());

            ControllerContext controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            constructorCompanyBuildingLogicMock.Setup(c => c.DeleteConstructorCompanyBuilding(It.IsAny<Guid>(), It.IsAny<Guid>()));

            OkResult expectedObject = new OkResult();

            ConstructorCompanyBuildingController anotherConstructorCompanyBuildingController = new ConstructorCompanyBuildingController(constructorCompanyBuildingLogicMock.Object) { ControllerContext = controllerContext };

            OkResult result = anotherConstructorCompanyBuildingController.DeleteConstructorCompanyBuilding(id) as OkResult;

            constructorCompanyBuildingLogicMock.VerifyAll();
            Assert.AreEqual(expectedObject.StatusCode, result.StatusCode);
        }
    }
}
