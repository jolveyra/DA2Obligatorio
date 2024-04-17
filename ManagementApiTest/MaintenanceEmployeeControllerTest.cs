using Domain;
using LogicInterfaces;
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModels.MaintenanceEmployeeModels;

namespace ManagementApiTest
{
    [TestClass]
    public class MaintenanceEmployeeControllerTest
    {

        [TestMethod]
        public void GetAllMaintenanceEmployeeTestOk()
        {
            IEnumerable<User> maintenanceEmployees = new List<User>
            {
                new User() { Id = Guid.NewGuid(), Role = Role.Manager },
                new User() { Id = Guid.NewGuid(), Role = Role.Administrator },
                new User() { Id = Guid.NewGuid(), Role = Role.Manager }
            };

            Mock<IMaintenanceEmployeeLogic> maintenanceEmployeeLogicMock = new Mock<IMaintenanceEmployeeLogic>(MockBehavior.Strict);
            MaintenanceEmployeeController maintenanceEmployeeController = new MaintenanceEmployeeController(maintenanceEmployeeLogicMock.Object);

            maintenanceEmployeeLogicMock.Setup(m => m.GetAllMaintenanceEmployees()).Returns(maintenanceEmployees.Where(u => u.Role == Role.Manager));

            OkObjectResult expected = new OkObjectResult(new List<MaintenanceEmployeeResponseModel>
            {
                new MaintenanceEmployeeResponseModel(maintenanceEmployees.First()),
                new MaintenanceEmployeeResponseModel(maintenanceEmployees.Last())
            });

            List<MaintenanceEmployeeResponseModel> expectedObject = expected.Value as List<MaintenanceEmployeeResponseModel>;

            OkObjectResult result = maintenanceEmployeeController.GetAllMaintenanceEmployees() as OkObjectResult;
            List<MaintenanceEmployeeResponseModel> objectResult = result.Value as List<MaintenanceEmployeeResponseModel>;

            maintenanceEmployeeLogicMock.VerifyAll();
            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode) && expectedObject.First().Equals(objectResult.First()) && expectedObject.Last().Equals(objectResult.Last()));
        }

    }
}