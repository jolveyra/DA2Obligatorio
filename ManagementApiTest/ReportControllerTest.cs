using LogicInterfaces;
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ManagementApiTest
{
    [TestClass]
    public class ReportControllerTest
    {
        [TestMethod]
        public void GetReportTestOk()
        {
            List<(string, int, int, int, double)> report = new List<(string, int, int, int, double)>
            {
                ("Building1", 1, 1, 1, 1),
                ("Building2", 0, 0, 1, 5)
            };

            Mock<IReportLogic> reportLogicMock = new Mock<IReportLogic>();
            reportLogicMock.Setup(rl => rl.GetReport(It.IsAny<string>())).Returns(report);
            ReportController reportController = new ReportController(reportLogicMock.Object);

            OkObjectResult result = reportController.GetReport("building") as OkObjectResult;
            List<(string, int, int, int, double)> resultValue = result.Value as List<(string, int, int, int, double)>;

            reportLogicMock.VerifyAll();
            Assert.IsTrue(resultValue.SequenceEqual(report));
        }
    }
}
