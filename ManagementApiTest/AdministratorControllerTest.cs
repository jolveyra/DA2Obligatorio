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
        [TestMethod]
        public void GetAllAdministratorsTestOk()
        {
            IEnumerable<User> administrators = new List<User>
            {
                new User() { Id = Guid.NewGuid(), Role = Role.Administrator },
                new User() { Id = Guid.NewGuid(), Role = Role.Manager },
                new User() { Id = Guid.NewGuid(), Role = Role.Administrator }
            };

            Mock<IAdministratorLogic> administratorLogicMock = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            AdministratorController administratorController = new AdministratorController(administratorLogicMock.Object);


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

    }
}