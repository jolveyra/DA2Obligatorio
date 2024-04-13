using LogicInterfaces;
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using WebModels;
using Moq;
using Domain;

namespace ManagementApiTest
{
    [TestClass]
    public class BuildingControllerTest
    {
        [TestMethod]
        public void GetAllBuildingsTestOk()
        {
            IEnumerable<Building> buildings = new List<Building> { new Building() { Name = "Mirador" } };

            Mock<IBuildingLogic> buildingLogicMock = new Mock<IBuildingLogic>(MockBehavior.Strict);
            buildingLogicMock.Setup(x => x.GetAllBuildings()).Returns(buildings);

            BuildingController buildingController = new BuildingController(buildingLogicMock.Object);

            OkObjectResult expected = new OkObjectResult(new List<BuildingResponseModel> { new BuildingResponseModel(buildings.First()) });
            List<BuildingResponseModel> expectedObject = expected.Value as List<BuildingResponseModel>;

            OkObjectResult result = buildingController.GetAllBuildings() as OkObjectResult;
            List<BuildingResponseModel> objectResult = result.Value as List<BuildingResponseModel>;

            buildingLogicMock.VerifyAll();
            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode));
            Assert.AreEqual(expectedObject.First(), objectResult.First());
        }

        [TestMethod]
        public void CreateBuildingTestOk()
        {
            BuildingRequestModel buildingRequest = new BuildingRequestModel() { Name = "Mirador" };
            Building expected = new Building() { Id = Guid.NewGuid(), Name = "Mirador" };

            BuildingResponseModel expectedResult = new BuildingResponseModel(expected);
            Mock<IBuildingLogic> buildingLogicMock = new Mock<IBuildingLogic>(MockBehavior.Strict);
            buildingLogicMock.Setup(x => x.CreateBuilding(It.IsAny<Building>())).Returns(expected);

            BuildingController buildingController = new BuildingController(buildingLogicMock.Object);

            CreatedAtActionResult expectedObjectResult = new CreatedAtActionResult("CreateBuilding", "CreateBuilding", new { id = 1 }, expectedResult);

            IActionResult result = buildingController.CreateBuilding(buildingRequest);

            CreatedAtActionResult resultObject = result as CreatedAtActionResult;
            BuildingResponseModel resultValue = resultObject.Value as BuildingResponseModel;

            buildingLogicMock.VerifyAll();

            Assert.AreEqual(resultObject.StatusCode, expectedObjectResult.StatusCode);
            Assert.AreEqual(resultValue, expectedResult);
        }

        [TestMethod]
        public void UpdateBuildingTestOk()
        {
            UpdateBuildingRequestModel updateBuildingRequest = new UpdateBuildingRequestModel() { SharedExpenses = 5000 };
            Building expected = new Building() { Id = Guid.NewGuid(), Name = "Mirador", SharedExpenses = 5000 };

            BuildingResponseModel expectedResult = new BuildingResponseModel(expected);
            Mock<IBuildingLogic> buildingLogicMock = new Mock<IBuildingLogic>(MockBehavior.Strict);
            buildingLogicMock.Setup(x => x.UpdateBuilding(It.IsAny<Guid>(), It.IsAny<float>())).Returns(expected);

            BuildingController buildingController = new BuildingController(buildingLogicMock.Object);

            OkObjectResult expectedObjectResult = new OkObjectResult(expectedResult);

            IActionResult result = buildingController.UpdateBuildingById(It.IsAny<Guid>(), updateBuildingRequest);

            OkObjectResult resultObject = result as OkObjectResult;
            BuildingResponseModel resultValue = resultObject.Value as BuildingResponseModel;

            buildingLogicMock.VerifyAll();

            Assert.IsTrue(resultObject.StatusCode.Equals(expectedObjectResult.StatusCode) && resultValue.Equals(expectedResult));
        }
    }
}