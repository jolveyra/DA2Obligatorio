using LogicInterfaces;
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using WebModels;
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
            IEnumerable<Building> buildings = new List<Building> { new Building() { Name = "Mirador" } };

            buildingLogicMock.Setup(x => x.GetAllBuildings()).Returns(buildings);

            OkObjectResult expected = new OkObjectResult(new List<BuildingResponseModel> { new BuildingResponseModel(buildings.First()) });
            List<BuildingResponseModel> expectedObject = expected.Value as List<BuildingResponseModel>;

            OkObjectResult result = buildingController.GetAllBuildings() as OkObjectResult;
            List<BuildingResponseModel> objectResult = result.Value as List<BuildingResponseModel>;

            buildingLogicMock.VerifyAll();
            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode) && expectedObject.First().Equals(objectResult.First()));
        }

        [TestMethod]
        public void GetBuildingByIdTestOk()
        {
            Building building =  new Building() { Name = "Mirador" } ;

            buildingLogicMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Returns(building);

            OkObjectResult expected = new OkObjectResult(new BuildingResponseModel(building));
            BuildingResponseModel expectedObject = expected.Value as BuildingResponseModel;

            OkObjectResult result = buildingController.GetBuildingById(It.IsAny<Guid>()) as OkObjectResult;
            BuildingResponseModel objectResult = result.Value as BuildingResponseModel;

            buildingLogicMock.VerifyAll();
            Assert.AreEqual(expected.StatusCode, result.StatusCode);
            Assert.AreEqual(expectedObject, objectResult);
        }

        [TestMethod]
        public void GetBuildingByIdTestNotFound()
        {
            buildingLogicMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Throws(new ArgumentException());

            NotFoundObjectResult expected = new NotFoundObjectResult("There is no building with that specific id");

            NotFoundObjectResult result = buildingController.GetBuildingById(It.IsAny<Guid>()) as NotFoundObjectResult;

            buildingLogicMock.VerifyAll();
            Assert.AreEqual(expected.StatusCode, result.StatusCode);
            Assert.AreEqual(expected.Value, result.Value);
        }

        [TestMethod]
        public void GetBuildingByIdTestInternalError()
        {
            buildingLogicMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Throws(new Exception());

            ObjectResult expected = new ObjectResult("An error occurred while retrieving the building") { StatusCode = 500 };

            ObjectResult result = buildingController.GetBuildingById(It.IsAny<Guid>()) as ObjectResult;

            buildingLogicMock.VerifyAll();
            Assert.AreEqual(expected.StatusCode, result.StatusCode);
            Assert.AreEqual(expected.Value, result.Value);
        }

        [TestMethod]
        public void CreateBuildingTestOk()
        {
            BuildingRequestModel buildingRequest = new BuildingRequestModel() { Name = "Mirador" };
            Building expected = new Building() { Id = Guid.NewGuid(), Name = "Mirador" };

            BuildingResponseModel expectedResult = new BuildingResponseModel(expected);
            buildingLogicMock.Setup(x => x.CreateBuilding(It.IsAny<Building>())).Returns(expected);

            CreatedAtActionResult expectedObjectResult = new CreatedAtActionResult("CreateBuilding", "CreateBuilding", new { id = 1 }, expectedResult);

            IActionResult result = buildingController.CreateBuilding(buildingRequest);

            CreatedAtActionResult resultObject = result as CreatedAtActionResult;
            BuildingResponseModel resultValue = resultObject.Value as BuildingResponseModel;

            buildingLogicMock.VerifyAll();

            Assert.IsTrue(resultObject.StatusCode.Equals(expectedObjectResult.StatusCode) && resultValue.Equals(expectedResult));
        }

        [TestMethod]
        public void CreateBuildingWithFlatsTestOk()
        {
            BuildingRequestModel buildingRequest = new BuildingRequestModel() { Name = "Mirador", Flats = 1 };
            Building expected = new Building() { Id = Guid.NewGuid(), Name = "Mirador", Flats = new List<Flat> { new Flat() } };

            BuildingResponseModel expectedResult = new BuildingResponseModel(expected);
            buildingLogicMock.Setup(x => x.CreateBuilding(It.IsAny<Building>())).Returns(expected);

            CreatedAtActionResult expectedObjectResult = new CreatedAtActionResult("CreateBuilding", "CreateBuilding", new { id = 1 }, expectedResult);

            IActionResult result = buildingController.CreateBuilding(buildingRequest);

            CreatedAtActionResult resultObject = result as CreatedAtActionResult;
            BuildingResponseModel resultValue = resultObject.Value as BuildingResponseModel;

            buildingLogicMock.VerifyAll();

            Assert.AreEqual(resultObject.StatusCode, expectedObjectResult.StatusCode);
            Assert.AreEqual(resultValue, expectedResult);

        }

        [TestMethod]
        public void CreateBuildingTestInternalError()
        {
            buildingLogicMock.Setup(x => x.CreateBuilding(It.IsAny<Building>())).Throws(new Exception());

            ObjectResult expected = new ObjectResult("An error occurred while creating the building") { StatusCode = 500 };

            ObjectResult result = buildingController.CreateBuilding(new BuildingRequestModel()) as ObjectResult;

            buildingLogicMock.VerifyAll();
            Assert.AreEqual(expected.StatusCode, result.StatusCode);
            Assert.AreEqual(expected.Value, result.Value);
        }

        [TestMethod]
        public void CreateBuildingTestArgumentError()
        {
            ArgumentException exception = new ArgumentException("Building already exists.");

            buildingLogicMock.Setup(x => x.CreateBuilding(It.IsAny<Building>())).Throws(exception);

            BadRequestObjectResult expectedObjectResult = new BadRequestObjectResult("Building already exists.");

            BadRequestObjectResult result = buildingController.CreateBuilding(new BuildingRequestModel()) as BadRequestObjectResult;

            buildingLogicMock.VerifyAll();

            Assert.IsTrue(result.StatusCode.Equals(expectedObjectResult.StatusCode) && result.Value.Equals(expectedObjectResult.Value));
        }

        [TestMethod]
        public void UpdateBuildingTestOk()
        {
            UpdateBuildingRequestModel updateBuildingRequest = new UpdateBuildingRequestModel() { SharedExpenses = 5000 };
            Building expected = new Building() { Id = Guid.NewGuid(), Name = "Mirador", SharedExpenses = 5000 };

            BuildingResponseModel expectedResult = new BuildingResponseModel(expected);
            buildingLogicMock.Setup(x => x.UpdateBuilding(It.IsAny<Guid>(), It.IsAny<float>())).Returns(expected);

            OkObjectResult expectedObjectResult = new OkObjectResult(expectedResult);

            IActionResult result = buildingController.UpdateBuildingById(It.IsAny<Guid>(), updateBuildingRequest);

            OkObjectResult resultObject = result as OkObjectResult;
            BuildingResponseModel resultValue = resultObject.Value as BuildingResponseModel;

            buildingLogicMock.VerifyAll();

            Assert.IsTrue(resultObject.StatusCode.Equals(expectedObjectResult.StatusCode) && resultValue.Equals(expectedResult));
        }


        [TestMethod]
        public void UpdateBuildingTestNotFound()
        {
            buildingLogicMock.Setup(x => x.UpdateBuilding(It.IsAny<Guid>(), It.IsAny<float>())).Throws(new ArgumentException());

            NotFoundObjectResult expectedObjectResult = new NotFoundObjectResult("There is no building with that specific id");

            NotFoundObjectResult result = buildingController.UpdateBuildingById(It.IsAny<Guid>(), new UpdateBuildingRequestModel()) as NotFoundObjectResult;

            buildingLogicMock.VerifyAll();

            Assert.IsTrue(result.StatusCode.Equals(expectedObjectResult.StatusCode) && result.Value.Equals(expectedObjectResult.Value));
        }

        [TestMethod]
        public void UpdateBuildingTestInternalError()
        {
            buildingLogicMock.Setup(x => x.UpdateBuilding(It.IsAny<Guid>(), It.IsAny<float>())).Throws(new Exception());

            ObjectResult expected = new ObjectResult("An error occurred while updating the building") { StatusCode = 500 };

            ObjectResult result = buildingController.UpdateBuildingById(It.IsAny<Guid>(), new UpdateBuildingRequestModel()) as ObjectResult;

            buildingLogicMock.VerifyAll();
            Assert.AreEqual(expected.StatusCode, result.StatusCode);
            Assert.AreEqual(expected.Value, result.Value);
        }

        [TestMethod]
        public void DeleteBuildingTestOk()
        {
            buildingLogicMock.Setup(x => x.DeleteBuilding(It.IsAny<Guid>()));

            OkResult result = buildingController.DeleteBuilding(It.IsAny<Guid>()) as OkResult;
            OkResult expectedResult = new OkResult();

            buildingLogicMock.VerifyAll();

            Assert.IsTrue(result.StatusCode.Equals(expectedResult.StatusCode));
        }

        [TestMethod]
        public void DeleteBuildingTestNotFound()
        {
            ArgumentException expectedException = new ArgumentException("Building not found.");
            buildingLogicMock.Setup(x => x.DeleteBuilding(It.IsAny<Guid>())).Throws(expectedException);

            NotFoundObjectResult expectedObjectResult = new NotFoundObjectResult("There is no building with that specific id.");

            NotFoundObjectResult result = buildingController.DeleteBuilding(It.IsAny<Guid>()) as NotFoundObjectResult;

            buildingLogicMock.VerifyAll();

            Assert.IsTrue(result.StatusCode.Equals(expectedObjectResult.StatusCode) && result.Value.Equals(expectedObjectResult.Value));
        }


        [TestMethod]
        public void DeleteBuildingTestInternalError()
        {
            buildingLogicMock.Setup(i => i.DeleteBuilding(It.IsAny<Guid>())).Throws(new Exception());

            ObjectResult expected = new ObjectResult("An error occurred while deleting the building") { StatusCode = 500 };

            ObjectResult result = buildingController.DeleteBuilding(It.IsAny<Guid>()) as ObjectResult;

            buildingLogicMock.VerifyAll();
            Assert.AreEqual(expected.StatusCode, result.StatusCode);
            Assert.AreEqual(expected.Value, result.Value);
        }


        [TestMethod]
        public void GetFlatByBuildingAndFlatIdTestOk()
        {
            Flat expected = new Flat()
            {
                Id = Guid.NewGuid(),
                Floor = 2,
                Number = 201,
                OwnerName = "Gonzalo",
                OwnerSurname = "Bergessio",
                OwnerEmail = "gonzabergessiobolso@gmail.com",
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
        public void UpdateFlatByBuildingAndFlatIdTestOk()
        {
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
                OwnerName = "Gonzalo",
                OwnerSurname = "Bergessio",
                OwnerEmail = "gonzabergessiobolso@gmail.com",
                Bathrooms = 3,
                HasBalcony = true
            };

            FlatResponseModel expectedResult = new FlatResponseModel(expected);
            buildingLogicMock.Setup(x => x.UpdateFlat(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Flat>())).Returns(expected);

            OkObjectResult expectedObjectResult = new OkObjectResult(expectedResult);

            IActionResult result = buildingController.UpdateFlatByBuildingAndFlatId(It.IsAny<Guid>(), It.IsAny<Guid>(), updateFlatRequest);

            OkObjectResult resultObject = result as OkObjectResult;
            FlatResponseModel resultValue = resultObject.Value as FlatResponseModel;

            buildingLogicMock.VerifyAll();

            Assert.AreEqual(resultObject.StatusCode, expectedObjectResult.StatusCode);
            Assert.AreEqual(resultValue, expectedResult);
        }
    }
}