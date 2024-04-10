using Domain;
using LogicInterfaces;
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using WebModels;
using Moq;

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
            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode) && expectedObject.First ().Name.Equals(objectResult.First().Name));
        }
    }
}