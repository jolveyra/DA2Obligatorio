
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ManagementApiTest
{
    [TestClass]
    public class TeapotControllerTest
    {
        [TestMethod]
        public void GetTeapotTest418()
        {
            TeapotController teapotController = new TeapotController();
            ObjectResult result = teapotController.GetTeapot() as ObjectResult;
            string resultValue = result.Value as string;

            Assert.IsTrue(result.StatusCode == 418 && resultValue.Equals("I got tired of buildings and managers, I'm just gonna be a teapot for a while"));
        }
    }
}
