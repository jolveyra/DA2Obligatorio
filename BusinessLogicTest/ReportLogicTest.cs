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

        [TestMethod]
        public void GetBuildingReportTest()
        {
            string filter = "Building";

            Mock<IRequestRepository> requestRepositoryMock = new Mock<IRequestRepository>();
            ReportLogic reportLogic = new ReportLogic(requestRepositoryMock.Object);

            List<Request> requests = new List<Request>
            {
                new Request
                {
                    Flat = new Flat
                    {
                        Building = new Building
                        {
                            Name = "Building1"
                        }
                    }
                },
                new Request
                {
                    Flat = new Flat
                    {
                        Building = new Building
                        {
                            Name = "Building2"
                        }
                    }
                },
                new Request
                {
                    Flat = new Flat
                    {
                        Building = new Building
                        {
                            Name = "Building1"
                        }
                    }
                },
                new Request
                {
                    Flat = new Flat
                    {
                        Building = new Building
                        {
                            Name = "Building2"
                        }
                    },
                    Status = RequestStatus.Completed,
                    StartingDate = DateTime.Now.AddHours(-2),
                    CompletionDate = DateTime.Now
                },
                new Request
                {
                    Flat = new Flat
                    {
                        Building = new Building
                        {
                            Name = "Building1"
                        }
                    },
                    Status = RequestStatus.InProgress,
                    StartingDate = DateTime.Now.AddHours(-2)
                }
            };

            requestRepositoryMock.Setup(r => r.GetAllRequests()).Returns(requests);

            List<(string, int, int, int, double)> expectedReport = new List<(string, int, int, int, double)>
            {
                ("Building1", 2, 1, 0, 0),
                ("Building2", 1, 0, 1, 2)
            };

            List<(string, int, int, int, double)> actualReport = reportLogic.GetReport(filter);

            requestRepositoryMock.VerifyAll();
            CollectionAssert.AreEqual(expectedReport, actualReport);
        }
    }
}
