using BusinessLogic;
using CustomExceptions;
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
            Guid managerId = Guid.NewGuid();
            string filter = "NonExistingFilter";

            Exception exception = null;

            try
            {
                reportLogic.GetReport(managerId, filter);
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
            User manager = new User() { Id = Guid.NewGuid(), Role = Role.Manager };
            string filter = "Building";

            List<Request> requests = new List<Request>
            {
                new Request
                {
                    Building = new Building
                    {
                        Name = "Building1",
                        ManagerId = manager.Id
                    },
                    ManagerId = manager.Id
                },
                new Request
                {
                    
                    Building = new Building
                    {
                        Name = "Building2",
                        ManagerId = manager.Id  
                    },
                    ManagerId = manager.Id
                },
                new Request
                {
                    Building = new Building
                    {
                        Name = "Building1",
                        ManagerId = manager.Id
                    },
                    ManagerId = manager.Id
                },
                new Request
                {
                    Building = new Building
                    {
                        Name = "Building2",
                        ManagerId = manager.Id
                    },
                    ManagerId = manager.Id,
                    Status = RequestStatus.Completed,
                    StartingDate = DateTime.Now.AddHours(-2),
                    CompletionDate = DateTime.Now
                },
                new Request
                {
                    Building = new Building
                    {
                        Name = "Building1",
                        ManagerId = manager.Id
                    },
                    ManagerId = manager.Id,
                    Status = RequestStatus.InProgress,
                    StartingDate = DateTime.Now.AddHours(-2)
                }
            };

            requestRepositoryMock.Setup(r => r.GetAllRequests()).Returns(requests);

            List<Report> expectedReport = new List<Report>
            {
                new Report ("Building1") { Pending = 2, InProgress = 1 },
                new Report ("Building2") { Pending = 1, InProgress = 0, Completed = 1, AvgTimeToComplete = 2 },
            };

            IEnumerable<Report> actualReport = reportLogic.GetReport(manager.Id, filter);

            requestRepositoryMock.VerifyAll();
            Assert.IsTrue(expectedReport.SequenceEqual(actualReport));
        }

        [TestMethod]
        public void GetEmployeeReportTest()
        {
            User manager = new User() { Id = Guid.NewGuid(), Role = Role.Manager };
            string filter = "Employee";
            User employee1 = new User() { Id = Guid.NewGuid(), Role = Role.MaintenanceEmployee, Name = "Employee1", Surname = "Employee1", Email = "employee1@mail.com" };
            User employee2 = new User() { Id = Guid.NewGuid(), Role = Role.MaintenanceEmployee, Name = "Employee2", Surname = "Employee2", Email = "employee2@mail.com"  };

            List<Request> requests = new List<Request>
            {
                new Request
                {
                    AssignedEmployee = employee1,
                    Building = new Building
                    {
                        Name = "Building1",
                        ManagerId = manager.Id
                    },
                    ManagerId = manager.Id
                },
                new Request
                {
                    AssignedEmployee = employee2,
                    Building = new Building
                    {
                        Name = "Building2",
                        ManagerId = manager.Id
                    },
                    ManagerId = manager.Id
                },
                new Request
                {
                    AssignedEmployee = employee1,
                    Building = new Building
                    {
                        Name = "Building2",
                        ManagerId = manager.Id
                    },
                    ManagerId = manager.Id
                },
                new Request
                {
                    AssignedEmployee = employee2,
                    Building = new Building
                    {
                        Name = "Building1",
                        ManagerId = manager.Id
                    },
                    ManagerId = manager.Id,
                    Status = RequestStatus.Completed,
                    StartingDate = DateTime.Now.AddHours(-2),
                    CompletionDate = DateTime.Now
                },
                new Request
                {
                    AssignedEmployee = employee1,
                    Flat = new Flat
                    {
                        Building = new Building
                        {
                            Name = "Building2",
                            ManagerId = manager.Id
                        }
                    },
                    ManagerId = manager.Id,
                    Status = RequestStatus.InProgress,
                    StartingDate = DateTime.Now.AddHours(-2)
                }
            };

            requestRepositoryMock.Setup(r => r.GetAllRequests()).Returns(requests);

            IEnumerable<Report> expectedReport = new List<Report>
            {
                new Report ("Employee1 Employee1 - employee1@mail.com") { Pending = 2, InProgress = 1 },
                new Report ("Employee2 Employee2 - employee2@mail.com") { Pending = 1, InProgress = 0, Completed = 1, AvgTimeToComplete = 2 }
            };

            IEnumerable<Report> actualReport = reportLogic.GetReport(manager.Id, filter);

            requestRepositoryMock.VerifyAll();
            Assert.IsTrue(expectedReport.SequenceEqual(actualReport));
        }
    }
}
