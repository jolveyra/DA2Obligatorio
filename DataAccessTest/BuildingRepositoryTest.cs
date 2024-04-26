using IDataAccess;
using DataAccess;
using DataAccess.Context;
using Domain;
using Moq;
using Moq.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessTest
{
    [TestClass]
    public class BuildingRepositoryTest
    {

        private IBuildingRepository buildingRepository;
        private Mock<BuildingBossContext> mockContext;

        [TestInitialize]
        public void Initialize()
        {
            mockContext = new Mock<BuildingBossContext>();

            buildingRepository = new BuildingRepository(mockContext.Object);
        }
        

        [TestMethod]
        public void GetAllBuildingsTestOk()
        {
            List<Building> buildings = new List<Building>()
            {
                new Building() { Id = Guid.NewGuid(), Name = "Building 1", Direction = "Address 1", ConstructorCompany = "City 1", SharedExpenses = 150, Flats = new List<Flat>() },
                new Building() { Id = Guid.NewGuid(), Name = "Building 2", Direction = "Address 2", ConstructorCompany = "City 2", SharedExpenses = 152, Flats = new List<Flat>() }

            };

            mockContext.Setup(x => x.Buildings).ReturnsDbSet(buildings);

            IEnumerable<Building> result = buildingRepository.GetAllBuildings();

            CollectionAssert.AreEqual(buildings, result.ToList());

        }
    }
}