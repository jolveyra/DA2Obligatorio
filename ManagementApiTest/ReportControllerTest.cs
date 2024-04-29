using LogicInterfaces;
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModels.ReportModels.Out;

namespace ManagementApiTest
{
    [TestClass]
    public class ReportControllerTest
    {
        [TestMethod]
        public void GetReportTestOk()
        {
            Guid managerId = Guid.NewGuid();
            IEnumerable<(string, int, int, int, double)> report = new List<(string, int, int, int, double)>
            {
                ("Building1", 1, 1, 1, 1),
                ("Building2", 0, 0, 1, 5)
            };
            IEnumerable<ReportResponseModel> expected = new List<ReportResponseModel>
            {
                new ReportResponseModel(report.First()),
                new ReportResponseModel(report.Last())
            };

            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Items.Add("UserId", managerId.ToString());

            ControllerContext controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            Mock<IReportLogic> reportLogicMock = new Mock<IReportLogic>();
            ReportController reportController = new ReportController(reportLogicMock.Object) { ControllerContext = controllerContext };
            reportLogicMock.Setup(rl => rl.GetReport(It.IsAny<Guid>(), It.IsAny<string>())).Returns(report);

            OkObjectResult result = reportController.GetReport("building") as OkObjectResult;
            IEnumerable<ReportResponseModel> resultValue = result.Value as IEnumerable<ReportResponseModel>;

            reportLogicMock.VerifyAll();
            Assert.IsTrue(resultValue.SequenceEqual(expected));
        }
    }
}
