using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using LogicInterfaces;
using ManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModels.ConstructorCompanyModels;

namespace ManagementApiTest
{
    [TestClass]
    public class ConstructorCompanyControllerTest
    {

        private Mock<IConstructorCompanyLogic> constructorCompanyLogicMock;
        private ConstructorCompanyController constructorCompanyController;

        [TestInitialize]
        public void TestInitialize()
        {
            constructorCompanyLogicMock = new Mock<IConstructorCompanyLogic>(MockBehavior.Strict);
            constructorCompanyController = new ConstructorCompanyController(constructorCompanyLogicMock.Object);
        }

        [TestMethod]
        public void GetAllConstructorCompaniesTestOk()
        {
            IEnumerable<ConstructorCompany> constructorCompanies = new List<ConstructorCompany>
            {
                new ConstructorCompany() { Id = Guid.NewGuid(), Name = "ConstructorCompany 1" },
                new ConstructorCompany() { Id = Guid.NewGuid(), Name = "ConstructorCompany 2" }
            };

            constructorCompanyLogicMock.Setup(c => c.GetAllConstructorCompanies()).Returns(constructorCompanies);

            OkObjectResult expected = new OkObjectResult(new List<ConstructorCompanyResponseModel>
            {
                new ConstructorCompanyResponseModel(constructorCompanies.First()),
                new ConstructorCompanyResponseModel(constructorCompanies.Last())
            });

            List<ConstructorCompanyResponseModel> expectedObject = expected.Value as List<ConstructorCompanyResponseModel>;

            OkObjectResult result = constructorCompanyController.GetAllConstructorCompanies() as OkObjectResult;
            List<ConstructorCompanyResponseModel> objectResult = result.Value as List<ConstructorCompanyResponseModel>;

            constructorCompanyLogicMock.VerifyAll();
            Assert.AreEqual(expected.StatusCode, result.StatusCode);
            CollectionAssert.AreEqual(expectedObject, objectResult);
        }

        [TestMethod]
        public void CreateConstructorCompanyTestOk()
        {
            CreateConstructorCompanyRequestModel constructorCompanyRequestModel = new CreateConstructorCompanyRequestModel()
            {
                Name = "ConstructorCompany 1"
            };
            ConstructorCompany constructorCompany = constructorCompanyRequestModel.ToEntity();

            constructorCompanyLogicMock.Setup(c => c.CreateConstructorCompany(It.IsAny<ConstructorCompany>())).Returns(constructorCompany);

            OkObjectResult expectedObject = new OkObjectResult(new ConstructorCompanyResponseModel(constructorCompany));
            OkObjectResult result = constructorCompanyController.CreateConstructorCompany(constructorCompanyRequestModel) as OkObjectResult;

            ConstructorCompanyResponseModel expectedConstructorCompanyResponseModel = expectedObject.Value as ConstructorCompanyResponseModel;
            ConstructorCompanyResponseModel resultConstructorCompanyResponseModel = result.Value as ConstructorCompanyResponseModel;

            constructorCompanyLogicMock.VerifyAll();
            Assert.AreEqual(expectedObject.StatusCode, result.StatusCode);
            Assert.AreEqual(expectedConstructorCompanyResponseModel, resultConstructorCompanyResponseModel);
        }

        [TestMethod]
        public void GetConstrucorCompanyByIdTestOk()
        {
            Guid id = Guid.NewGuid();
            ConstructorCompany constructorCompany = new ConstructorCompany()
            {
                Id = id,
                Name = "ConstructorCompany 1"
            };

            constructorCompanyLogicMock.Setup(c => c.GetConstructorCompanyById(It.IsAny<Guid>())).Returns(constructorCompany);

            OkObjectResult expected = new OkObjectResult(new ConstructorCompanyResponseModel(constructorCompany));
            OkObjectResult result = constructorCompanyController.GetConstructorCompanyById(id) as OkObjectResult;

            ConstructorCompanyResponseModel expectedObject = expected.Value as ConstructorCompanyResponseModel;
            ConstructorCompanyResponseModel objectResult = result.Value as ConstructorCompanyResponseModel;

            constructorCompanyLogicMock.VerifyAll();
            Assert.AreEqual(expected.StatusCode, result.StatusCode);
            Assert.AreEqual(expectedObject, objectResult);
        }
    }

}
