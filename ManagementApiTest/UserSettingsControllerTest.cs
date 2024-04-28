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
            userSettingsLogicMock.Setup(x => x.GetUserById(It.IsAny<Guid>())).Returns(user);
            UserSettingsController userSettingsController = new UserSettingsController(userSettingsLogicMock.Object);

            OkObjectResult result = userSettingsController.GetUserSettings(userId) as OkObjectResult;
            UserResponseModel userResponseModel = result.Value as UserResponseModel;

            userSettingsLogicMock.VerifyAll();
            Assert.AreEqual(user.Id, userResponseModel.Id);
        }

        [TestMethod]
        public void UpdateUserSettingsTest()
        {
            Guid userId = Guid.NewGuid();
            User user = new User()
            {
                Id = userId,
                Name = "Test1",
                Surname = "Test1",
                Email = "test@gmail.com",
                Password = "123456789",
            };

            UserUpdateRequestModel userUpdateRequestModel = new UserUpdateRequestModel()
            {
                Name = "Test1",
                Surname = "Test1",
                Password = "123456789",
            };

            Mock<IUserSettingsLogic> userSettingsLogicMock = new Mock<IUserSettingsLogic>();
            userSettingsLogicMock.Setup(x => x.UpdateUserSettings(It.IsAny<Guid>(), It.IsAny<User>())).Returns(user);
            UserSettingsController userSettingsController = new UserSettingsController(userSettingsLogicMock.Object);

            UserResponseModel expected = new UserResponseModel(user);

            OkObjectResult result = userSettingsController.UpdateUserSettings(userId, userUpdateRequestModel) as OkObjectResult;
            UserResponseModel userResponseModel = result.Value as UserResponseModel;

            userSettingsLogicMock.VerifyAll();
            Assert.IsTrue(expected.Equals(userResponseModel));
        }
    }
}
