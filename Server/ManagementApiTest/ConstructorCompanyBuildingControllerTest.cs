using Domain;
using ManagementApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.BuildingModels;
using Microsoft.AspNetCore.Mvc;

namespace ManagementApiTest
{
    [TestClass]
    public class ConstructorCompanyBuildingControllerTest
    {
        private Mock<IConstructorCompanyBuildingLogic> constructorCompanyBuildingLogicMock;
        private ConstructorCompanyBuildingController constructorCompanyBuildingController;

        [TestInitialize]
        public void TestInitialize()
        {
            constructorCompanyBuildingLogicMock = new Mock<IConstructorCompanyBuildingLogic>(MockBehavior.Strict);
            constructorCompanyBuildingController = new ConstructorCompanyBuildingController(constructorCompanyBuildingLogicMock.Object);
        }


        [TestMethod]
        public void GetAllConstructorCompanyBuildingsTestOk()
        {
            ConstructorCompany constructorCompany = new ConstructorCompany() { Id = Guid.NewGuid(), Name = "Constructor Company 1" };
            IEnumerable<Building> constructorCompanyBuildings = new List<Building>
            {
                new Building() {
                    Id = Guid.NewGuid(),
                    Name = "Building 1",
                    ConstructorCompany = constructorCompany,
                    Address = new Address { DoorNumber = 21, CornerStreet = "Street 1", Id = Guid.NewGuid(), Latitude = 23, Longitude = 24, Street = "Street 2" },
                    SharedExpenses = 13,
                    MaintenanceEmployees = new List<Guid>()
                },
                new Building() { Id = Guid.NewGuid(), Name = "Building 2", ConstructorCompany = constructorCompany,
                    Address = new Address { DoorNumber = 22, CornerStreet = "Street 2", Id = Guid.NewGuid(), Latitude = 25, Longitude = 26, Street = "Street 3" },
                    SharedExpenses = 14,
                    MaintenanceEmployees = new List<Guid>()
                }
            };

            constructorCompanyBuildingLogicMock.Setup(c => c.GetAllConstructorCompanyBuildings()).Returns(constructorCompanyBuildings);

            OkObjectResult expected = new OkObjectResult(new List<BuildingResponseModel>
            {
                new BuildingResponseModel(constructorCompanyBuildings.First()),
                new BuildingResponseModel(constructorCompanyBuildings.Last())
            });

            List<BuildingResponseModel> expectedObject = expected.Value as List<BuildingResponseModel>;

            OkObjectResult result = constructorCompanyBuildingController.GetAllConstructorCompanyBuildings() as OkObjectResult;
            List<BuildingResponseModel> objectResult = result.Value as List<BuildingResponseModel>;

            constructorCompanyBuildingLogicMock.VerifyAll();
            Assert.AreEqual(expected.StatusCode, result.StatusCode);
            CollectionAssert.AreEqual(expectedObject, objectResult);
        }




    }
}
