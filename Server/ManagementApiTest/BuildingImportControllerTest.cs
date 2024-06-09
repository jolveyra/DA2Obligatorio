using Azure;
using Domain;
using LogicInterfaces;
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModels.BuildingModels;
using WebModels.BuildingImportModels;

namespace ManagementApiTest
{
    [TestClass]
    public class BuildingImportControllerTest
    {
        private Mock<IBuildingImportLogic> _importBuildingLogicMock;
        private BuildingImportController buildingImportController;

        [TestInitialize]
        public void Initialize()
        {
            _importBuildingLogicMock = new Mock<IBuildingImportLogic>(MockBehavior.Strict);
            buildingImportController = new BuildingImportController(_importBuildingLogicMock.Object);
        }

        [TestMethod]
        public void ImportBuildingTestOk()
        {
            Guid ccAdminId = Guid.NewGuid();
            BuildingImportRequestModel buildingImportRequestModel = new BuildingImportRequestModel()
            {
                DllName = "ImportadorJsonsV1",
                FileName = "BuildingsToImportCompany"
            };

            Building building = new Building()
            {
                Name = "Building",
                SharedExpenses = 4200,
                Address = new Address()
                {
                    Street = "Street",
                    CornerStreet = "CornerStreet",
                    DoorNumber = 9090,
                    Latitude = 0,
                    Longitude = 0
                },
                ManagerId = ccAdminId
            };

            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Items.Add("UserId", ccAdminId.ToString());

            ControllerContext controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
            
            BuildingImportController anotherController= new BuildingImportController(_importBuildingLogicMock.Object) { ControllerContext = controllerContext };
            _importBuildingLogicMock.Setup(x => x.ImportBuildings(buildingImportRequestModel.DllName, buildingImportRequestModel.FileName, ccAdminId)).Returns(new List<Building>() { building });

            List<BuildingWithoutFlatsResponseModel> expectedObjectResult = new List<BuildingWithoutFlatsResponseModel>()
            {
                new BuildingWithoutFlatsResponseModel(building)
            };
            CreatedAtActionResult expected = new CreatedAtActionResult("ImportBuildings", "ImportBuildingsController", new { Id = "", route = "buildings" }, expectedObjectResult);

            CreatedAtActionResult result = anotherController.ImportBuildings(buildingImportRequestModel) as CreatedAtActionResult;
            List<BuildingWithoutFlatsResponseModel> objectResult = result.Value as List<BuildingWithoutFlatsResponseModel>;

            _importBuildingLogicMock.VerifyAll();

            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode));
            Assert.IsTrue(objectResult.First().Equals(expectedObjectResult.First()));
        }
    }
}
