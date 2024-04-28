using Domain;
using LogicInterfaces;
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModels.UserResponseModel;

namespace ManagementApiTest
{
    [TestClass]
    public class UserSettingsControllerTest
    {
        [TestMethod]
        public void GetUserByIdTest()
        {
            Guid userId = Guid.NewGuid();
            User user = new User()
            {
                Id = userId,
                Name = "Test",
                Email = ""
            };

            Mock<IUserSettingsLogic> userSettingsLogicMock = new Mock<IUserSettingsLogic>();
            userSettingsLogicMock.Setup(x => x.GetUserById(userId)).Returns(user);
            UserSettingsController userSettingsController = new UserSettingsController(userSettingsLogicMock.Object);

            OkObjectResult result = userSettingsController.GetUserSettings(userId) as OkObjectResult;
            UserResponseModel userResponseModel = result.Value as UserResponseModel;

            userSettingsLogicMock.VerifyAll();
            Assert.AreEqual(user.Id, userResponseModel.Id);
        }
    }
}
