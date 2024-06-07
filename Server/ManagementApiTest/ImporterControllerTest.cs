using LogicInterfaces;
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using WebModels.BuildingModels;
using Moq;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using WebModels.ImporterModels;

namespace ManagementApiTest
{
    [TestClass]
    public class ImporterControllerTest
    {
        private Mock<IImporterLogic> _importerLogicMock;
        private ImporterController _importerController;

        [TestInitialize]
        public void Setup()
        {
            _importerLogicMock = new Mock<IImporterLogic>(MockBehavior.Strict);
            _importerController = new ImporterController(_importerLogicMock.Object);
        }

        [TestMethod]
        public void GetAllImportersTestOk()
        {
            IEnumerable<Importer> importers = new List<Importer>
            {
                new Importer() { Id = Guid.NewGuid(), Name = "ImportadorJsonsV1", Path = @"../../../ImportadorJsonsV1"}
            };

            _importerLogicMock.Setup(x => x.GetAllImporters()).Returns(importers);

            OkObjectResult expected = new OkObjectResult(new List<ImporterResponseModel>
            {
                new ImporterResponseModel(importers.First())
            });

            List<ImporterResponseModel> expectedObject = expected.Value as List<ImporterResponseModel>;

            OkObjectResult result = _importerController.GetAllImporters() as OkObjectResult;

            List<ImporterResponseModel> objectResult = result.Value as List<ImporterResponseModel>;

            _importerLogicMock.VerifyAll();

            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode) && expectedObject.First().Equals(objectResult.First()));

        }
    }
}
