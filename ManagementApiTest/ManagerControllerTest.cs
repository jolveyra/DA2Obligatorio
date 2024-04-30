using Domain;
using LogicInterfaces;
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModels.ManagerModels;

namespace ManagementApiTest
{
    [TestClass]
    public class ManagerControllerTest
    {
        [TestMethod]
        public void GetAllManagersTestOk()
        {
            IEnumerable<User> managers = new List<User>()
            {
                new User()
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    Email = "Email",
                    Surname = "Surname",
                    Role = Role.Manager
                }
            };

            Mock<IManagerLogic> managerLogicMock = new Mock<IManagerLogic>(MockBehavior.Strict);
            managerLogicMock.Setup(m => m.GetAllManagers()).Returns(managers);
            ManagerController managerController = new ManagerController(managerLogicMock.Object);

            OkObjectResult expected = new OkObjectResult(new List<ManagerResponseModel>()
            {
                new ManagerResponseModel(managers.First())
            });
            List<ManagerResponseModel> expectedResult = expected.Value as List<ManagerResponseModel>;

            OkObjectResult result = managerController.GetAllManagers() as OkObjectResult;
            List<ManagerResponseModel> resultValue = result.Value as List<ManagerResponseModel>;

            managerLogicMock.VerifyAll();
            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode) && expectedResult.SequenceEqual(resultValue));
        }
    }
}
