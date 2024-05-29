using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicInterfaces;
using ManagementApi.Controllers;
using Moq;
using WebModels.ConstructorCompanyAdministratorModels;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagementApiTest
{
    [TestClass]
    public class ConstructorCompanyAdministratorControllerTest
    {
        private Mock<IConstructorCompanyAdministratorLogic> constructorCompanyAdministratorLogicMock;
        private ConstructorCompanyAdministratorController constructorCompanyAdministratorController;

        [TestInitialize]
        public void TestInitialize()
        {
            constructorCompanyAdministratorLogicMock = new Mock<IConstructorCompanyAdministratorLogic>(MockBehavior.Strict);
            constructorCompanyAdministratorController = new ConstructorCompanyAdministratorController(constructorCompanyAdministratorLogicMock.Object);
        }

        [TestMethod]
        public void SetConstructorCompanyAdministratorTestOk()
        {
            SetConstructorCompanyAdministratorRequestModel setConstructorCompanyAdministratorRequestModel = new SetConstructorCompanyAdministratorRequestModel()
            {
                ConstructorCompanyId = Guid.NewGuid()
            };

            ConstructorCompany constructorCompany = new ConstructorCompany()
            {
                Id = setConstructorCompanyAdministratorRequestModel.ConstructorCompanyId
            };

            ConstructorCompanyAdministrator admin = new ConstructorCompanyAdministrator()
            {
                ConstructorCompany = constructorCompany,
                Id = Guid.NewGuid()
            };

            ConstructorCompanyAdministratorResponseModel constructorCompanyAdministratorResponseModel = new ConstructorCompanyAdministratorResponseModel(admin);


            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Items.Add("UserId", admin.Id.ToString());

            ControllerContext controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            constructorCompanyAdministratorLogicMock.Setup(c => c.SetConstructorCompanyAdministrator(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(admin);

            OkObjectResult expected = new OkObjectResult(constructorCompanyAdministratorResponseModel);

            ConstructorCompanyAdministratorController anotherConstructorCompanyAdministratorController = new ConstructorCompanyAdministratorController(constructorCompanyAdministratorLogicMock.Object) { ControllerContext = controllerContext };

            OkObjectResult result = anotherConstructorCompanyAdministratorController.SetConstructorCompanyAdministrator(setConstructorCompanyAdministratorRequestModel) as OkObjectResult;

            constructorCompanyAdministratorLogicMock.VerifyAll();
            Assert.AreEqual(expected.StatusCode, result.StatusCode);
            Assert.AreEqual(expected.Value, result.Value);
        }

        [TestMethod]
        public void GetAdminConstructorCompanyTestOk()
        {
            Guid constructorCompanyId = Guid.NewGuid();

            ConstructorCompanyAdministrator admin = new ConstructorCompanyAdministrator()
            {
                ConstructorCompany = new ConstructorCompany()
                {
                    Name = "ConstructorCompany",
                    Id = constructorCompanyId
                },
                Id = Guid.NewGuid()
            };

            ConstructorCompanyResponseModel response = new ConstructorCompanyResponseModel(admin.ConstructorCompany);

            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Items.Add("UserId", admin.Id.ToString());

            ControllerContext controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            constructorCompanyAdministratorLogicMock.Setup(c => c.GetAdminConstructorCompany(It.IsAny<Guid>())).Returns(admin.ConstructorCompany);

            OkObjectResult expected = new OkObjectResult(response);

            ConstructorCompanyAdministratorController anotherConstructorCompanyAdministratorController = new ConstructorCompanyAdministratorController(constructorCompanyAdministratorLogicMock.Object) { ControllerContext = controllerContext };

            OkObjectResult result = anotherConstructorCompanyAdministratorController.GetAdminConstructorCompany() as OkObjectResult;

            constructorCompanyAdministratorLogicMock.VerifyAll();
            Assert.AreEqual(expected.StatusCode, result.StatusCode);
            Assert.AreEqual(expected.Value, result.Value);

        }
    }
}
