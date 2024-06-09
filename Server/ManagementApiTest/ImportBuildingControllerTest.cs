using Domain;
using LogicInterfaces;
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModels.BuildingModels;
using WebModels.ImportBuildingModels;

namespace ManagementApiTest
{
    [TestClass]
    public class ImportBuildingControllerTest
    {
        private Mock<IImportBuildingLogic> _importBuildingLogicMock;
        private ImportBuildingController _importBuildingController;

        [TestInitialize]
        public void Initialize()
        {
            _importBuildingLogicMock = new Mock<IImportBuildingLogic>(MockBehavior.Strict);
            _importBuildingController = new ImportBuildingController(_importBuildingLogicMock.Object);
        }

        [TestMethod]
        public void ImportBuildingTestOk()
        {
            Guid ccAdminId = Guid.NewGuid();
            ImportBuildingRequestModel importBuildingRequestModel = new ImportBuildingRequestModel()
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
            
            ImportBuildingController anotherBuildingController= new ImportBuildingController(_importBuildingLogicMock.Object) { ControllerContext = controllerContext };
            _importBuildingLogicMock.Setup(x => x.ImportBuildings(importBuildingRequestModel.DllName, importBuildingRequestModel.FileName, ccAdminId)).Returns(new List<Building>() { building });

            List<BuildingWithoutFlatsResponseModel> expectedObjectResult = new List<BuildingWithoutFlatsResponseModel>()
            {
                new BuildingWithoutFlatsResponseModel(building)
            };
            OkObjectResult expected = new OkObjectResult(expectedObjectResult);


            OkObjectResult result = anotherBuildingController.ImportBuildings(importBuildingRequestModel) as OkObjectResult;
            List<BuildingWithoutFlatsResponseModel> objectResult = result.Value as List<BuildingWithoutFlatsResponseModel>;

            _importBuildingLogicMock.VerifyAll();

            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode));
            Assert.IsTrue(objectResult.First().Equals(expectedObjectResult.First()));
        }
    }
}
