using Domain;
using LogicInterfaces;
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModels.UserModels;

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

            OkObjectResult expected = new OkObjectResult(new List<UserResponseModel>()
            {
                new UserResponseModel(managers.First())
            });
            List<UserResponseModel> expectedResult = expected.Value as List<UserResponseModel>;

            OkObjectResult result = managerController.GetAllManagers() as OkObjectResult;
            List<UserResponseModel> resultValue = result.Value as List<UserResponseModel>;

            managerLogicMock.VerifyAll();
            Assert.IsTrue(result.StatusCode.Equals(expected.StatusCode) && expectedResult.SequenceEqual(resultValue));
        }
    }
}
