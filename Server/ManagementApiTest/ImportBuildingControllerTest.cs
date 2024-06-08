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

            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Items.Add("UserId", ccAdminId.ToString());

            ControllerContext controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
            
            ImportBuildingController anotherBuildingController= new ImportBuildingController(_importBuildingLogicMock.Object) { ControllerContext = controllerContext };
            _importBuildingLogicMock.Setup(x => x.ImportBuildings(importBuildingRequestModel.DllName, importBuildingRequestModel.FileName, ccAdminId));

            OkObjectResult expected = new OkObjectResult(null);
            ImportBuildingResponseModel expectedObjectResult = new ImportBuildingResponseModel("Imported successfully");


            OkObjectResult result = anotherBuildingController.ImportBuildings(importBuildingRequestModel) as OkObjectResult;
            ImportBuildingResponseModel objectResult = result.Value as ImportBuildingResponseModel;

            _importBuildingLogicMock.VerifyAll();

            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode));
            Assert.IsTrue(objectResult.Equals(expectedObjectResult));
        }
    }
}
