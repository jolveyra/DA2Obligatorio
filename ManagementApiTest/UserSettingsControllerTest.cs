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
        private Mock<IUserSettingsLogic> _userSettingsLogicMock;
        private UserSettingsController _userSettingsController;

        [TestInitialize]
        public void Initialize()
        {
            _userSettingsLogicMock = new Mock<IUserSettingsLogic>();
            _userSettingsController = new UserSettingsController(_userSettingsLogicMock.Object);
        }

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

            _userSettingsLogicMock.Setup(x => x.GetUserById(It.IsAny<Guid>())).Returns(user);
            
            OkObjectResult result = _userSettingsController.GetUserSettings(userId) as OkObjectResult;
            UserResponseModel userResponseModel = result.Value as UserResponseModel;

            _userSettingsLogicMock.VerifyAll();
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

            _userSettingsLogicMock.Setup(x => x.UpdateUserSettings(It.IsAny<Guid>(), It.IsAny<User>())).Returns(user);
            
            UserResponseModel expected = new UserResponseModel(user);

            OkObjectResult result = _userSettingsController.UpdateUserSettings(userId, userUpdateRequestModel) as OkObjectResult;
            UserResponseModel userResponseModel = result.Value as UserResponseModel;

            _userSettingsLogicMock.VerifyAll();
            Assert.IsTrue(expected.Equals(userResponseModel));
        }
    }
}
