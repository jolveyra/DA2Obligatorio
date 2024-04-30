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
        private Mock<IUserRepository> userRepositoryMock;
        private ReportLogic reportLogic;

        [TestInitialize]
        public void TestInitialize()
        {
            requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            reportLogic = new ReportLogic(requestRepositoryMock.Object, userRepositoryMock.Object);
        }

        [TestMethod]
        public void GetNonExistingReportTest()
        {
            Guid managerId = Guid.NewGuid();
            string filter = "NonExistingFilter";

            userRepositoryMock.Setup(u => u.GetUserById(It.IsAny<Guid>())).Returns(new User() { Id = managerId, Role = Role.Manager });
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
            userRepositoryMock.VerifyAll();
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
                    Flat = new Flat
                    {
                        Building = new Building
                        {
                            Name = "Building1",
                            Manager = manager
                        }
                    }
                },
                new Request
                {
                    Flat = new Flat
                    {
                        Building = new Building
                        {
                            Name = "Building2",
                            Manager = manager
                        }
                    }
                },
                new Request
                {
                    Flat = new Flat
                    {
                        Building = new Building
                        {
                            Name = "Building1",
                            Manager = manager
                        }
                    }
                },
                new Request
                {
                    Flat = new Flat
                    {
                        Building = new Building
                        {
                            Name = "Building2",
                            Manager = manager
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
                            Name = "Building1",
                            Manager = manager
                        }
                    },
                    Status = RequestStatus.InProgress,
                    StartingDate = DateTime.Now.AddHours(-2)
                }
            };

            userRepositoryMock.Setup(u => u.GetUserById(It.IsAny<Guid>())).Returns(manager);
            requestRepositoryMock.Setup(r => r.GetAllRequests()).Returns(requests);

            List<(string, int, int, int, double)> expectedReport = new List<(string, int, int, int, double)>
            {
                ("Building1", 2, 1, 0, 0),
                ("Building2", 1, 0, 1, 2)
            };

            IEnumerable<(string, int, int, int, double)> actualReport = reportLogic.GetReport(manager.Id, filter);

            requestRepositoryMock.VerifyAll();
            userRepositoryMock.VerifyAll();
            Assert.IsTrue(expectedReport.SequenceEqual(actualReport));
        }

        [TestMethod]
        public void GetEmployeeReportTest()
        {
            User manager = new User() { Id = Guid.NewGuid(), Role = Role.Manager };
            string filter = "Employee";
            Guid employee1Id = Guid.NewGuid();
            Guid employee2Id = Guid.NewGuid();
            User employee1 = new User() { Id = employee1Id, Name = "Employee1", Surname = "Employee1" };
            User employee2 = new User() { Id = employee2Id, Name = "Employee2", Surname = "Employee2" };

            List<Request> requests = new List<Request>
            {
                new Request
                {
                    AssignedEmployeeId = employee1Id,
                    Flat = new Flat
                    {
                        Building = new Building
                        {
                            Name = "Building1",
                            Manager = manager
                        }
                    }
                },
                new Request
                {
                    AssignedEmployeeId = employee2Id,
                    Flat = new Flat
                    {
                        Building = new Building
                        {
                            Name = "Building2",
                            Manager = manager
                        }
                    }
                },
                new Request
                {
                    AssignedEmployeeId = employee1Id,
                    Flat = new Flat
                    {
                        Building = new Building
                        {
                            Name = "Building2",
                            Manager = manager
                        }
                    }
                },
                new Request
                {
                    AssignedEmployeeId = employee2Id,
                    Flat = new Flat
                    {
                        Building = new Building
                        {
                            Name = "Building1",
                            Manager = manager
                        }
                    },
                    Status = RequestStatus.Completed,
                    StartingDate = DateTime.Now.AddHours(-2),
                    CompletionDate = DateTime.Now
                },
                new Request
                {
                    AssignedEmployeeId = employee1Id,
                    Flat = new Flat
                    {
                        Building = new Building
                        {
                            Name = "Building2",
                            Manager = manager
                        }
                    },
                    Status = RequestStatus.InProgress,
                    StartingDate = DateTime.Now.AddHours(-2)
                }
            };

            userRepositoryMock.Setup(u => u.GetUserById(manager.Id)).Returns(manager);
            userRepositoryMock.Setup(u => u.GetUserById(employee1Id)).Returns(employee1);
            userRepositoryMock.Setup(u => u.GetUserById(employee2Id)).Returns(employee2);
            requestRepositoryMock.Setup(r => r.GetAllRequests()).Returns(requests);

            IEnumerable<(string, int, int, int, double)> expectedReport = new List<(string, int, int, int, double)>
            {
                ("Employee1 Employee1", 2, 1, 0, 0),
                ("Employee2 Employee2", 1, 0, 1, 2)
            };

            IEnumerable<(string, int, int, int, double)> actualReport = reportLogic.GetReport(manager.Id, filter);

            requestRepositoryMock.VerifyAll();
            userRepositoryMock.VerifyAll();
            Assert.IsTrue(expectedReport.SequenceEqual(actualReport));
        }
    }
}
