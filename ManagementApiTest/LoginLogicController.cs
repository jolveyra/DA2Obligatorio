using Domain;
using LogicInterfaces;
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModels.LoginModels;

namespace ManagementApiTest
{
    [TestClass]
    public class LoginLogicController
    {
        [TestMethod]
        public void LoginTestOk()
        {
            LoginRequestModel request = new LoginRequestModel
            {
                Email = "test",
                Password = "test"
            };

            Guid token = Guid.NewGuid();

            LoginResponseModel expected = new LoginResponseModel(token);

            Mock<ILoginLogic> loginLogic = new Mock<ILoginLogic>(MockBehavior.Strict);
            loginLogic.Setup(x => x.Login(It.IsAny<User>())).Returns(token);
            LoginController controller = new LoginController(loginLogic.Object);

            OkObjectResult result = controller.LogIn(request) as OkObjectResult;
            LoginResponseModel resultValue = result.Value as LoginResponseModel;

            loginLogic.VerifyAll();
            Assert.AreEqual(expected.Token, resultValue.Token);
        }
    }
}
