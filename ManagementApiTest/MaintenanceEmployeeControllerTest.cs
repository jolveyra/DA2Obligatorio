using Domain;
using LogicInterfaces;
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModels.AdministratorModels;
using WebModels.MaintenanceEmployeeModels;

namespace ManagementApiTest
{
    [TestClass]
    public class MaintenanceEmployeeControllerTest
    {
        private Mock<IMaintenanceEmployeeLogic> maintenanceEmployeeLogicMock;
        private MaintenanceEmployeeController maintenanceEmployeeController;

        [TestInitialize]
        public void TestInitialize()
        {
            maintenanceEmployeeLogicMock = new Mock<IMaintenanceEmployeeLogic>(MockBehavior.Strict);
            maintenanceEmployeeController = new MaintenanceEmployeeController(maintenanceEmployeeLogicMock.Object);
        }

        [TestMethod]
        public void GetAllMaintenanceEmployeeTestOk()
        {
            IEnumerable<User> maintenanceEmployees = new List<User>
            {
                new User() { Id = Guid.NewGuid(), Role = Role.Manager },
                new User() { Id = Guid.NewGuid(), Role = Role.Administrator },
                new User() { Id = Guid.NewGuid(), Role = Role.Manager }
            };

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

        [TestMethod]
        public void CreateMaintenanceEmployeeTestCreated()
        {
            CreateMaintenanceEmployeeRequestModel maintenanceEmployeeRequestModel = new CreateMaintenanceEmployeeRequestModel()
            {
                Email = "juan123@gmail.com",
                Name = "Juan",
                Surname = "Perez",
                Password = "123456"
            };
            User expected = maintenanceEmployeeRequestModel.ToEntity();
            expected.Id = Guid.NewGuid();

            maintenanceEmployeeLogicMock.Setup(m => m.CreateMaintenanceEmployee(It.IsAny<User>())).Returns(expected);

            MaintenanceEmployeeResponseModel expectedResult = new MaintenanceEmployeeResponseModel(expected);
            CreatedAtActionResult expectedObjectResult = new CreatedAtActionResult("CreateMaintenanceEmployee", "CreateMaintenanceEmployee", new { expected.Id }, expectedResult);

            CreatedAtActionResult result = maintenanceEmployeeController.CreateMaintenanceEmployee(maintenanceEmployeeRequestModel) as CreatedAtActionResult;
            MaintenanceEmployeeResponseModel objectResult = result.Value as MaintenanceEmployeeResponseModel;

            maintenanceEmployeeLogicMock.VerifyAll();
            Assert.IsTrue(expectedObjectResult.StatusCode.Equals(result.StatusCode) && expectedResult.Equals(objectResult));
        }
    }
}