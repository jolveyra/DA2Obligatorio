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
            UserLogged userLogged = new UserLogged() { Token = Guid.NewGuid(), Name = "Name", Role = Role.Administrator };

            LoginResponseModel expected = new LoginResponseModel(userLogged);

            Mock<ILoginLogic> loginLogic = new Mock<ILoginLogic>(MockBehavior.Strict);
            loginLogic.Setup(x => x.Login(It.IsAny<User>())).Returns(userLogged);
            LoginController controller = new LoginController(loginLogic.Object);

            OkObjectResult result = controller.Login(request) as OkObjectResult;
            LoginResponseModel resultValue = result.Value as LoginResponseModel;

            loginLogic.VerifyAll();
            Assert.AreEqual(expected.Token, resultValue.Token);
        }
    }
}
