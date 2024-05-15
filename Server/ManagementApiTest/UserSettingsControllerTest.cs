using Domain;
using LogicInterfaces;
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModels.UserModels;

namespace ManagementApiTest
{
    [TestClass]
    public class UserSettingsControllerTest
    {
        private Mock<IUserSettingsLogic> _userSettingsLogicMock;
        private UserSettingsController _userSettingsController;
        private Guid userId;

        [TestInitialize]
        public void Initialize()
        {
            _userSettingsLogicMock = new Mock<IUserSettingsLogic>();
            
            userId = Guid.NewGuid();
            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Items.Add("UserId", userId.ToString());

            ControllerContext controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            _userSettingsController = new UserSettingsController(_userSettingsLogicMock.Object) { ControllerContext = controllerContext };
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
            
            OkObjectResult result = _userSettingsController.GetUserSettings() as OkObjectResult;
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

            OkObjectResult result = _userSettingsController.UpdateUserSettings(userUpdateRequestModel) as OkObjectResult;
            UserResponseModel userResponseModel = result.Value as UserResponseModel;

            _userSettingsLogicMock.VerifyAll();
            Assert.IsTrue(expected.Equals(userResponseModel));
        }
    }
}
