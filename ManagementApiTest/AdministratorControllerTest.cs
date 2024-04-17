using Domain;
using LogicInterfaces;
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModels.AdministratorModels;

namespace ManagementApiTest
{
    [TestClass]
    public class AdministratorControllerTest
    {
        private Mock<IAdministratorLogic> administratorLogicMock;
        private AdministratorController administratorController;

        [TestInitialize]
        public void TestInitialize()
        {
            administratorLogicMock = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            administratorController = new AdministratorController(administratorLogicMock.Object);
        }

        [TestMethod]
        public void GetAllAdministratorsTestOk()
        {
            IEnumerable<User> administrators = new List<User>
            {
                new User() { Id = Guid.NewGuid(), Role = Role.Administrator },
                new User() { Id = Guid.NewGuid(), Role = Role.Manager },
                new User() { Id = Guid.NewGuid(), Role = Role.Administrator }
            };

            administratorLogicMock.Setup(a => a.GetAllAdministrators()).Returns(administrators.Where(u => u.Role == Role.Administrator));

            OkObjectResult expected = new OkObjectResult(new List<AdministratorResponseModel>
            {
                new AdministratorResponseModel(administrators.First()),
                new AdministratorResponseModel(administrators.Last())
            });

            List<AdministratorResponseModel> expectedObject = expected.Value as List<AdministratorResponseModel>;

            OkObjectResult result = administratorController.GetAllAdministrators() as OkObjectResult;
            List<AdministratorResponseModel> objectResult = result.Value as List<AdministratorResponseModel>;

            administratorLogicMock.VerifyAll();
            Assert.IsTrue(expected.StatusCode.Equals(result.StatusCode) && expectedObject.First().Equals(objectResult.First()) && expectedObject.Last().Equals(objectResult.Last()));
        }

        [TestMethod]
        public void CreateAdministratorTestCreated()
        {
            CreateAdministratorRequestModel administratorCreateModel = new CreateAdministratorRequestModel()
            {
                Email = "juan123@gmail.com",
                Name = "Juan",
                Surname = "Perez",
                Password = "123456"
            };
            User expected = administratorCreateModel.ToEntity();
            expected.Id = Guid.NewGuid();

            administratorLogicMock.Setup(a => a.CreateAdministrator(It.IsAny<User>())).Returns(expected);

            AdministratorResponseModel expectedResult = new AdministratorResponseModel(expected);
            CreatedAtActionResult expectedObjectResult = new CreatedAtActionResult("CreateAdministrator", "CreateAdministrator", new { Id = expected.Id }, expectedResult);

            CreatedAtActionResult result = administratorController.CreateAdministrator(administratorCreateModel) as CreatedAtActionResult;
            AdministratorResponseModel objectResult = result.Value as AdministratorResponseModel;

            administratorLogicMock.VerifyAll();
            Assert.IsTrue(expectedObjectResult.StatusCode.Equals(result.StatusCode) && expectedResult.Equals(objectResult));
        }
    }
}