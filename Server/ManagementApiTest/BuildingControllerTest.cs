using LogicInterfaces;
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using WebModels.BuildingModels;
using Moq;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ManagementApiTest
{
    [TestClass]
    public class BuildingControllerTest
    {

        private Mock<IBuildingLogic> buildingLogicMock;
        private BuildingController buildingController;

        [TestInitialize]
        public void Initialize()
        {
            buildingLogicMock = new Mock<IBuildingLogic>(MockBehavior.Strict);
            buildingController = new BuildingController(buildingLogicMock.Object);
        }

        [TestMethod]
        public void GetAllBuildingsTestOk()
        {

            User user = new User() { Id = Guid.NewGuid(), Role = Role.Manager };

            IEnumerable<Building> buildings = new List<Building> { new Building() { Name = "Mirador", ManagerId = user.Id, Address = new Address() { Street = "", CornerStreet = "" } } };

            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Items.Add("UserId", user.Id.ToString());

            ControllerContext controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            buildingLogicMock.Setup(x => x.GetAllBuildingFlats(It.IsAny<Guid>())).Returns(new List<Flat>() { new Flat() { Id = Guid.NewGuid(), Bathrooms = 3, Building = buildings.First(), Floor = 2, HasBalcony = true, Number = 300, Owner = new Person() { Email = "",  Name="", Surname="" }, Rooms = 3 } });
            buildingLogicMock.Setup(x => x.GetAllBuildings(It.IsAny<Guid>())).Returns(buildings);

            OkObjectResult expected = new OkObjectResult(new List<BuildingWithoutFlatsResponseModel> { new BuildingWithoutFlatsResponseModel(buildings.First()) });
            List<BuildingWithoutFlatsResponseModel> expectedObject = expected.Value as List<BuildingWithoutFlatsResponseModel>;

            BuildingController anotherBuildingController = new BuildingController(buildingLogicMock.Object) { ControllerContext = controllerContext };

            OkObjectResult result = anotherBuildingController.GetAllBuildings() as OkObjectResult;
            List<BuildingWithoutFlatsResponseModel> objectResult = result.Value as List<BuildingWithoutFlatsResponseModel>;

            buildingLogicMock.VerifyAll();
            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode) && expectedObject.First().Equals(objectResult.First()));
        }

        [TestMethod]
        public void GetBuildingByIdTestOk()
        {
            User user = new User() { Id = Guid.NewGuid(), Role = Role.Manager };

            Building building =  new Building() { Id = Guid.NewGuid(), Name = "Mirador", ConstructorCompanyId = Guid.NewGuid(), ManagerId = user.Id , Address = new Address() { Street = "", CornerStreet = "" } } ;


            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Items.Add("UserId", user.Id.ToString());

            ControllerContext controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            buildingLogicMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Returns(building);
            buildingLogicMock.Setup(x => x.GetAllBuildingFlats(It.IsAny<Guid>())).Returns(new List<Flat>() { new Flat() { Id = Guid.NewGuid(), Bathrooms = 3, Building = building, Floor = 2, HasBalcony = true, Number = 300, Owner = new Person() { Email = "",  Name="", Surname="" }, Rooms = 3 } });

            OkObjectResult expected = new OkObjectResult(new BuildingResponseModel(building));
            BuildingResponseModel expectedObject = expected.Value as BuildingResponseModel;

            BuildingController anotherBuildingController = new BuildingController(buildingLogicMock.Object) { ControllerContext = controllerContext };

            OkObjectResult result = anotherBuildingController.GetBuildingById(It.IsAny<Guid>()) as OkObjectResult;
            BuildingResponseModel objectResult = result.Value as BuildingResponseModel;

            buildingLogicMock.VerifyAll();
            Assert.AreEqual(expected.StatusCode, result.StatusCode);
            Assert.AreEqual(expectedObject, objectResult);
        }

        [TestMethod]
        public void UpdateBuildingSharedExpensesTestOk()
        {
            UpdateBuildingRequestModel updateBuildingRequest = new UpdateBuildingRequestModel() { SharedExpenses = 5000 };
            Building expected = new Building() { Id = Guid.NewGuid(), Name = "Mirador", SharedExpenses = 5000, ConstructorCompanyId = Guid.NewGuid(), Address = new Address() { Street = "", CornerStreet = "" } };
            
            BuildingResponseModel expectedResult = new BuildingResponseModel(expected);
            buildingLogicMock.Setup(x => x.UpdateBuilding(It.IsAny<Guid>(), It.IsAny<Building>())).Returns(expected);
            buildingLogicMock.Setup(x => x.GetAllBuildingFlats(It.IsAny<Guid>())).Returns(new List<Flat>() { new Flat() { Id = Guid.NewGuid(), Bathrooms = 3, Building = expected, Floor = 2, HasBalcony = true, Number = 300, Owner = new Person() { Email = "",  Name="", Surname="" }, Rooms = 3 } });

            OkObjectResult expectedObjectResult = new OkObjectResult(expectedResult);

            IActionResult result = buildingController.UpdateBuildingById(It.IsAny<Guid>(), updateBuildingRequest);

            OkObjectResult resultObject = result as OkObjectResult;
            BuildingResponseModel resultValue = resultObject.Value as BuildingResponseModel;

            buildingLogicMock.VerifyAll();

            Assert.AreEqual(resultObject.StatusCode, expectedObjectResult.StatusCode);
            Assert.AreEqual(resultValue.SharedExpenses, expectedResult.SharedExpenses);
        }

        [TestMethod]
        public void UpdateBuildingMaintenanceEmployeesTestOk()
        {
            UpdateBuildingRequestModel updateBuildingRequest = new UpdateBuildingRequestModel() { SharedExpenses = 5000, 
                MaintenanceEmployees = new List<Guid>() {}
            };

            Building expected = new Building() { Id = Guid.NewGuid(), Name = "Mirador", SharedExpenses = 5000 , ConstructorCompanyId = Guid.NewGuid(), Address = new Address() { Street = "Hola", CornerStreet = "Hola" } };

            BuildingResponseModel expectedResult = new BuildingResponseModel(expected);
            buildingLogicMock.Setup(x => x.UpdateBuilding(It.IsAny<Guid>(), It.IsAny<Building>())).Returns(expected);
            buildingLogicMock.Setup(x => x.GetAllBuildingFlats(It.IsAny<Guid>())).Returns(new List<Flat>() {  });

            OkObjectResult expectedObjectResult = new OkObjectResult(expectedResult);

            IActionResult result = buildingController.UpdateBuildingById(It.IsAny<Guid>(), updateBuildingRequest);

            OkObjectResult resultObject = result as OkObjectResult;
            BuildingResponseModel resultValue = resultObject.Value as BuildingResponseModel;

            buildingLogicMock.VerifyAll();

            Assert.IsTrue(resultObject.StatusCode.Equals(expectedObjectResult.StatusCode) && resultValue.Equals(expectedResult));
        }

        [TestMethod]
        public void GetFlatByBuildingAndFlatIdTestOk()
        {
            Flat expected = new Flat()
            {
                Id = Guid.NewGuid(),
                Floor = 2,
                Number = 201,
                Owner = new Person()
                {
                    Name = "Gonzalo",
                    Surname = "Bergessio",
                    Email = "gonzabergessiobolso@gmail.com",
                },
                Bathrooms = 3,
                HasBalcony = true
            };

            FlatResponseModel expectedResult = new FlatResponseModel(expected);
            buildingLogicMock.Setup(x => x.GetFlatByBuildingAndFlatId(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(expected);

            OkObjectResult expectedObjectResult = new OkObjectResult(expectedResult);

            IActionResult result = buildingController.GetFlatByBuildingAndFlatId(It.IsAny<Guid>(), It.IsAny<Guid>());

            OkObjectResult resultObject = result as OkObjectResult;
            FlatResponseModel resultValue = resultObject.Value as FlatResponseModel;

            buildingLogicMock.VerifyAll();

            Assert.AreEqual(resultObject.StatusCode, expectedObjectResult.StatusCode);
            Assert.AreEqual(resultValue, expectedResult);
        }

        [TestMethod]
        public void UpdateFlatByFlatIdTestOk()
        {
            Guid buildingId = Guid.NewGuid();
            UpdateFlatRequestModel updateFlatRequest = new UpdateFlatRequestModel()
            {
                Floor = 2,
                Number = 201,
                OwnerName = "Gonzalo",
                OwnerSurname = "Bergessio",
                OwnerEmail = "gonzabergessiobolso@gmail.com",
                Bathrooms = 3,
                HasBalcony = true
            };

            Flat expected = new Flat()
            {
                Id = Guid.NewGuid(),
                Floor = 2,
                Number = 201,
                Owner = new Person()
                {
                    Name = "Gonzalo",
                    Surname = "Bergessio",
                    Email = "gonzabergessiobolso@gmail.com",
                },
                Bathrooms = 3,
                HasBalcony = true,
                Building = new Building() { Id = buildingId }
            };

            buildingLogicMock.Setup(x => x.UpdateFlat(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Flat>(), It.IsAny<bool>())).Returns(expected);

            FlatResponseModel expectedResult = new FlatResponseModel(expected);
            OkObjectResult expectedObjectResult = new OkObjectResult(expectedResult);

            OkObjectResult resultObject = buildingController.UpdateFlatByFlatId(buildingId, expected.Id, updateFlatRequest) as OkObjectResult;
            FlatResponseModel resultValue = resultObject.Value as FlatResponseModel;

            buildingLogicMock.VerifyAll();

            Assert.AreEqual(resultObject.StatusCode, expectedObjectResult.StatusCode);
            Assert.AreEqual(resultValue, expectedResult);
        }

    }
}