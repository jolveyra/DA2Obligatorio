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
            IEnumerable<Building> _buildings = new List<Building>
            {
                new Building() { Name = "Mirador" }
            };

            Mock<IBuildingLogic> buildingLogicMock = new Mock<IBuildingLogic>();
            buildingLogicMock.Setup(x => x.GetAllBuildings()).Returns(_buildings);

            BuildingController _buildingController = new BuildingController(buildingLogicMock.Object);

            OkObjectResult expected = new OkObjectResult(new List<BuildingResponseModel>
            {
                new BuildingResponseModel(_buildings.First())
            });

            List<BuildingResponseModel> expectedObject = expected.Value as List<BuildingResponseModel>;

            OkObjectResult result = _buildingController.GetAllBuildings() as OkObjectResult;
            Console.WriteLine(result.Value);

            List<BuildingResponseModel> ObjectResult = result.Value as List<BuildingResponseModel>;

            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode));

        }
    }
}