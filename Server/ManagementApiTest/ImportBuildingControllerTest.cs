using LogicInterfaces;
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
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
            ImportBuildingRequestModel importBuildingRequestModel = new ImportBuildingRequestModel()
            {
                DllName = "ImportadorJsonsV1",
                FileName = "BuildingsToImportCompany"
            };

            _importBuildingLogicMock.Setup(x => x.ImportBuildings(importBuildingRequestModel.DllName, importBuildingRequestModel.FileName));

            OkObjectResult expected = new OkObjectResult(null);

            OkObjectResult result = _importBuildingController.ImportBuildings(importBuildingRequestModel) as OkObjectResult;

            _importBuildingLogicMock.VerifyAll();

            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode));
        }
    }
}
