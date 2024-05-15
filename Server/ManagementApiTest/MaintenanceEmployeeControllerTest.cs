using Domain;
using LogicInterfaces;
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModels.UserModels;

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

            OkObjectResult expected = new OkObjectResult(new List<UserResponseModel>
            {
                new UserResponseModel(maintenanceEmployees.First()),
                new UserResponseModel(maintenanceEmployees.Last())
            });

            List<UserResponseModel> expectedObject = expected.Value as List<UserResponseModel>;

            OkObjectResult result = maintenanceEmployeeController.GetAllMaintenanceEmployees() as OkObjectResult;
            List<UserResponseModel> objectResult = result.Value as List<UserResponseModel>;

            maintenanceEmployeeLogicMock.VerifyAll();
            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode) && expectedObject.First().Equals(objectResult.First()) && expectedObject.Last().Equals(objectResult.Last()));
        }

        [TestMethod]
        public void CreateMaintenanceEmployeeTestCreated()
        {
            CreateUserRequestModel maintenanceEmployeeRequestModel = new CreateUserRequestModel()
            {
                Email = "juan123@gmail.com",
                Name = "Juan",
                Surname = "Perez",
                Password = "123456"
            };
            User expected = maintenanceEmployeeRequestModel.ToEntity();
            expected.Id = Guid.NewGuid();

            maintenanceEmployeeLogicMock.Setup(m => m.CreateMaintenanceEmployee(It.IsAny<User>())).Returns(expected);

            UserResponseModel expectedResult = new UserResponseModel(expected);
            CreatedAtActionResult expectedObjectResult = new CreatedAtActionResult("CreateMaintenanceEmployee", "CreateMaintenanceEmployee", new { expected.Id }, expectedResult);

            CreatedAtActionResult result = maintenanceEmployeeController.CreateMaintenanceEmployee(maintenanceEmployeeRequestModel) as CreatedAtActionResult;
            UserResponseModel objectResult = result.Value as UserResponseModel;

            maintenanceEmployeeLogicMock.VerifyAll();
            Assert.IsTrue(expectedObjectResult.StatusCode.Equals(result.StatusCode) && expectedResult.Equals(objectResult));
        }
    }
}