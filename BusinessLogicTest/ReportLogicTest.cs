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
        private Mock<IRequestRepository> requestRepositoryMock;
        private ReportLogic reportLogic;

        [TestInitialize]
        public void TestInitialize()
        {
            requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            reportLogic = new ReportLogic(requestRepositoryMock.Object);
        }

        [TestMethod]
        public void GetNonExistingReportTest()
        {
            string filter = "NonExistingFilter";

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

        [TestMethod]
        public void GetEmployeeReportTest()
        {
            string filter = "Employee";

            List<Request> requests = new List<Request>
            {
                new Request
                {
                    AssignedEmployee = new User()
                    {
                        Role = Role.MaintenanceEmployee,
                        Name = "Employee1",
                        Surname = "Employee1"
                    }
                },
                new Request
                {
                    AssignedEmployee = new User()
                    {
                        Role = Role.MaintenanceEmployee,
                        Name = "Employee2",
                        Surname = "Employee2"
                    }
                },
                new Request
                {
                    AssignedEmployee = new User()
                    {
                        Role = Role.MaintenanceEmployee,
                        Name = "Employee1",
                        Surname = "Employee1"
                    }
                },
                new Request
                {
                    AssignedEmployee = new User()
                    {
                        Role = Role.MaintenanceEmployee,
                        Name = "Employee2",
                        Surname = "Employee2"
                    },
                    Status = RequestStatus.Completed,
                    StartingDate = DateTime.Now.AddHours(-2),
                    CompletionDate = DateTime.Now
                },
                new Request
                {
                    AssignedEmployee = new User()
                    {
                        Role = Role.MaintenanceEmployee,
                        Name = "Employee1",
                        Surname = "Employee1"
                    },
                    Status = RequestStatus.InProgress,
                    StartingDate = DateTime.Now.AddHours(-2)
                }
            };

            requestRepositoryMock.Setup(r => r.GetAllRequests()).Returns(requests);

            List<(string, int, int, int, double)> expectedReport = new List<(string, int, int, int, double)>
            {
                ("Employee1 Employee1", 2, 1, 0, 0),
                ("Employee2 Employee2", 1, 0, 1, 2)
            };

            List<(string, int, int, int, double)> actualReport = reportLogic.GetReport(filter);

            requestRepositoryMock.VerifyAll();
            CollectionAssert.AreEqual(expectedReport, actualReport);
        }
    }
}
