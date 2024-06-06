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
        public void GetAllConstructorCompanyAdministratorsTestOk()
        {
            IEnumerable<ConstructorCompanyAdministrator> admins = new List<ConstructorCompanyAdministrator>()
            {
                new ConstructorCompanyAdministrator()
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin1",
                    Surname = "Admin1",
                    Email = "admin1@mail.com",
                    Role = Role.ConstructorCompanyAdmin,
                    Password = "admin1",
                    ConstructorCompanyId = Guid.NewGuid(),
                    ConstructorCompany = new ConstructorCompany()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Company1"
                    }
                },
                new ConstructorCompanyAdministrator()
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin2",
                    Surname = "Admin2",
                    Email = "admin2@mail.com",
                    Role = Role.ConstructorCompanyAdmin,
                    Password = "admin2",
                    ConstructorCompanyId = Guid.NewGuid(),
                    ConstructorCompany = new ConstructorCompany()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Company2"
                    }
                }
            };

            List<ConstructorCompanyAdministratorResponseModel> expectedObject = new List<ConstructorCompanyAdministratorResponseModel>()
            {
                new ConstructorCompanyAdministratorResponseModel(admins.First()),
                new ConstructorCompanyAdministratorResponseModel(admins.Last())
            };

            constructorCompanyAdministratorLogicMock.Setup(c => c.GetAllConstructorCompanyAdministrators()).Returns(admins);

            OkObjectResult expected = new OkObjectResult(expectedObject);

            OkObjectResult result = constructorCompanyAdministratorController.GetAllConstructorCompanyAdministrators() as OkObjectResult;
            List<ConstructorCompanyAdministratorResponseModel> resultValue = result.Value as List<ConstructorCompanyAdministratorResponseModel>;

            constructorCompanyAdministratorLogicMock.VerifyAll();
            Assert.AreEqual(expected.StatusCode, result.StatusCode);
            Assert.IsTrue(expectedObject.First().Equals(resultValue.First()) && expectedObject.Last().Equals(resultValue.Last()));
        }

        [TestMethod]
        public void SetConstructorCompanyAdministratorTestOk()
        {
            UpdateConstructorCompanyAdministratorRequestModel UpdateConstructorCompanyAdministratorRequestModel = new UpdateConstructorCompanyAdministratorRequestModel()
            {
                ConstructorCompanyId = Guid.NewGuid()
            };

            ConstructorCompany constructorCompany = new ConstructorCompany()
            {
                Id = UpdateConstructorCompanyAdministratorRequestModel.ConstructorCompanyId
            };

            ConstructorCompanyAdministrator admin = new ConstructorCompanyAdministrator()
            {
                ConstructorCompanyId = constructorCompany.Id,
                Id = Guid.NewGuid()
            };

            ConstructorCompanyAdministratorResponseModel constructorCompanyAdministratorResponseModel = new ConstructorCompanyAdministratorResponseModel(admin);


            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Items.Add("UserId", admin.Id.ToString());

            ControllerContext controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            constructorCompanyAdministratorLogicMock.Setup(c => c.UpdateConstructorCompanyAdministrator(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(admin);

            OkObjectResult expected = new OkObjectResult(constructorCompanyAdministratorResponseModel);

            ConstructorCompanyAdministratorController anotherConstructorCompanyAdministratorController = new ConstructorCompanyAdministratorController(constructorCompanyAdministratorLogicMock.Object) { ControllerContext = controllerContext };

            OkObjectResult result = anotherConstructorCompanyAdministratorController.UpdateConstructorCompanyAdministrator(UpdateConstructorCompanyAdministratorRequestModel) as OkObjectResult;

            constructorCompanyAdministratorLogicMock.VerifyAll();
            Assert.AreEqual(expected.StatusCode, result.StatusCode);
            Assert.AreEqual(expected.Value, result.Value);
        }

        [TestMethod]
        public void GetConstructorCompanyAdministratorTestOk()
        {
            Guid constructorCompanyId = Guid.NewGuid();

            ConstructorCompanyAdministrator admin = new ConstructorCompanyAdministrator()
            {
                ConstructorCompanyId = Guid.NewGuid()
            };

            ConstructorCompanyAdministratorResponseModel response = new ConstructorCompanyAdministratorResponseModel(admin);

            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Items.Add("UserId", admin.Id.ToString());

            ControllerContext controllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            constructorCompanyAdministratorLogicMock.Setup(c => c.GetConstructorCompanyAdministrator(It.IsAny<Guid>())).Returns(admin);

            OkObjectResult expected = new OkObjectResult(response);

            ConstructorCompanyAdministratorController anotherConstructorCompanyAdministratorController = new ConstructorCompanyAdministratorController(constructorCompanyAdministratorLogicMock.Object) { ControllerContext = controllerContext };

            OkObjectResult result = anotherConstructorCompanyAdministratorController.GetConstructorCompanyAdministrator() as OkObjectResult;

            constructorCompanyAdministratorLogicMock.VerifyAll();
            Assert.AreEqual(expected.StatusCode, result.StatusCode);
            Assert.AreEqual(expected.Value, result.Value);

        }
    }
}
