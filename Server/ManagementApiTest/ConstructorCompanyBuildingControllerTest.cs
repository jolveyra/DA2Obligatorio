using Domain;
using ManagementApi.Controllers;
using LogicInterfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.BuildingModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebModels.ConstructorCompanyBuildingModels;

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
                    ConstructorCompany = constructorCompany,
                    Address = new Address { DoorNumber = 21, CornerStreet = "Street 1", Id = Guid.NewGuid(), Latitude = 23, Longitude = 24, Street = "Street 2" },
                    SharedExpenses = 13,
                    MaintenanceEmployees = new List<Guid>()
                },
                new Building() { Id = Guid.NewGuid(), Name = "Building 2", ConstructorCompany = constructorCompany,
                    Address = new Address { DoorNumber = 22, CornerStreet = "Street 2", Id = Guid.NewGuid(), Latitude = 25, Longitude = 26, Street = "Street 3" },
                    SharedExpenses = 14,
                    MaintenanceEmployees = new List<Guid>()
                }
            };


            ConstructorCompanyAdministrator constructorCompanyAdministrator = new ConstructorCompanyAdministrator()
            {
                Id = Guid.NewGuid(),
                Name = "Administrator 1",
                ConstructorCompany = constructorCompany
            };

            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Items.Add("UserId", constructorCompanyAdministrator.Id.ToString());

            ControllerContext controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            ConstructorCompanyBuildingController anotherConstructorCompanyBuildingController = new ConstructorCompanyBuildingController(constructorCompanyBuildingLogicMock.Object) { ControllerContext = controllerContext };

            constructorCompanyBuildingLogicMock.Setup(c => c.GetAllConstructorCompanyBuildings(It.IsAny<Guid>())).Returns(constructorCompanyBuildings);

            OkObjectResult expected = new OkObjectResult(new List<BuildingResponseModel>
            {
                new BuildingResponseModel(constructorCompanyBuildings.First()),
                new BuildingResponseModel(constructorCompanyBuildings.Last())
            });

            List<BuildingResponseModel> expectedObject = expected.Value as List<BuildingResponseModel>;

            OkObjectResult result = anotherConstructorCompanyBuildingController.GetAllConstructorCompanyBuildings() as OkObjectResult;
            List<BuildingResponseModel> objectResult = result.Value as List<BuildingResponseModel>;

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
                Flats = 12,
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
                ConstructorCompany = new ConstructorCompany() { Id = Guid.NewGuid(), Name = "A Constructor Company" }
            };

            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Items.Add("UserId", constructorCompanyAdministrator.Id.ToString());

            ControllerContext controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            Building building = buildingRequestModel.ToEntity();
            building.Id = Guid.NewGuid();
            building.ConstructorCompany = constructorCompanyAdministrator.ConstructorCompany;

            constructorCompanyBuildingLogicMock.Setup(c => c.CreateConstructorCompanyBuilding(It.IsAny<Building>(), It.IsAny<int>(), It.IsAny<Guid>())).Returns(building);

            OkObjectResult expectedObject = new OkObjectResult(new BuildingResponseModel(building));

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
                ConstructorCompany = constructorCompany,
                Address = new Address { DoorNumber = 21, CornerStreet = "Street 1", Id = Guid.NewGuid(), Latitude = 23, Longitude = 24, Street = "Street 2" },
                SharedExpenses = 13,
                MaintenanceEmployees = new List<Guid>()
            };

            ConstructorCompanyAdministrator constructorCompanyAdministrator = new ConstructorCompanyAdministrator()
            {
                Id = Guid.NewGuid(),
                Name = "Administrator 1",
                ConstructorCompany = constructorCompany
            };

            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Items.Add("UserId", constructorCompanyAdministrator.Id.ToString());

            ControllerContext controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            constructorCompanyBuildingLogicMock.Setup(c => c.GetConstructorCompanyBuildingById(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(building);

            OkObjectResult expectedObject = new OkObjectResult(new BuildingResponseModel(building));

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

            User newManager = new User() { Id = Guid.NewGuid(), Name = "Manager 2", Role = Role.Manager };

            Building building = new Building()
            {
                Id = id,
                Name = "Building 1",
                ConstructorCompany = constructorCompany,
                Address = new Address { DoorNumber = 21, CornerStreet = "Street 1", Id = Guid.NewGuid(), Latitude = 23, Longitude = 24, Street = "Street 2" },
                SharedExpenses = 13,
                MaintenanceEmployees = new List<Guid>(),
                Manager = newManager
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
                ConstructorCompany = constructorCompany
            };

            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Items.Add("UserId", constructorCompanyAdministrator.Id.ToString());

            ControllerContext controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            constructorCompanyBuildingLogicMock.Setup(c => c.UpdateConstructorCompanyBuilding(It.IsAny<Building>(), It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(building);

            OkObjectResult expectedObject = new OkObjectResult(new BuildingResponseModel(building));

            ConstructorCompanyBuildingController anotherConstructorCompanyBuildingController = new ConstructorCompanyBuildingController(constructorCompanyBuildingLogicMock.Object) { ControllerContext = controllerContext };

            OkObjectResult result = anotherConstructorCompanyBuildingController.UpdateConstructorCompanyBuilding(id, updateConstructorCompanyBuildingRequestModel) as OkObjectResult;

            constructorCompanyBuildingLogicMock.VerifyAll();
            Assert.AreEqual(expectedObject.StatusCode, result.StatusCode);
            Assert.AreEqual(expectedObject.Value, result.Value);
        }
    }
}
