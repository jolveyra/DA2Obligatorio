using BusinessLogic;
using CustomExceptions.BusinessLogic;
using Domain;
using Moq;
using RepositoryInterfaces;

namespace BusinessLogicTest
{
    [TestClass]
    public class ReportLogicTest
    {
        [TestMethod]
        public void GetNonExistingReportTest()
        {
            string filter = "NonExistingFilter";

            Mock<IRequestRepository> requestRepositoryMock = new Mock<IRequestRepository>();
            ReportLogic reportLogic = new ReportLogic(requestRepositoryMock.Object);

            Exception exception = null;

            try
            {
                reportLogic.GetReport(filter);
            }
            catch (Exception e)
            {
                exception = e;
            }

            requestRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(ReportException));
            Assert.AreEqual("Invalid filter", exception.Message);
        }
    }
}
