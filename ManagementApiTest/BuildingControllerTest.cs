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
        public void CreateBuildingTestInternalError()
        {
            BuildingRequestModel buildingRequest = new BuildingRequestModel() { Name = "Mirador" };

            Exception exception = new Exception("Internal server error.");

            buildingLogicMock.Setup(x => x.CreateBuilding(It.IsAny<Building>())).Throws(exception);

            StatusCodeResult expectedStatusCodeResult = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            IActionResult result = buildingController.CreateBuilding(buildingRequest);

            StatusCodeResult resultStatusCode = result as StatusCodeResult;

            buildingLogicMock.VerifyAll();
            Assert.IsTrue(resultStatusCode.StatusCode.Equals(expectedStatusCodeResult.StatusCode));
        }

        [TestMethod]
        public void CreateBuildingTestArgumentError()
        {
            BuildingRequestModel buildingRequest = new BuildingRequestModel() { Name = "Mirador" };

            ArgumentException exception = new ArgumentException("Building already exists.");

            buildingLogicMock.Setup(x => x.CreateBuilding(It.IsAny<Building>())).Throws(exception);

            StatusCodeResult expectedStatusCodeResult = new BadRequestResult();

            StatusCodeResult resultStatusCode = buildingController.CreateBuilding(buildingRequest) as StatusCodeResult;

            buildingLogicMock.VerifyAll();

            Assert.IsTrue(resultStatusCode.StatusCode.Equals(expectedStatusCodeResult.StatusCode));
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
        public void UpdateBuildingTestOkNotFound()
        {
            ArgumentException expectedException = new ArgumentException("Building not found.");
            buildingLogicMock.Setup(x => x.UpdateBuilding(It.IsAny<Guid>(), It.IsAny<float>())).Throws(expectedException);

            NotFoundObjectResult expectedObjectResult = new NotFoundObjectResult("There is no building with that specific id.");

            NotFoundObjectResult result = buildingController.UpdateBuildingById(It.IsAny<Guid>(), new UpdateBuildingRequestModel()) as NotFoundObjectResult;

            buildingLogicMock.VerifyAll();

            Assert.IsTrue(result.StatusCode.Equals(expectedObjectResult.StatusCode) && result.Value.Equals(expectedObjectResult.Value));
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
    }
}