using RepositoryInterfaces;
using DataAccess.Context;
using Domain;
using Moq;
using Moq.EntityFrameworkCore;
using DataAccess.Repositories;


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
                new Building() { Id = Guid.NewGuid(), Name = "Building 1", Street = "Street", DoorNumber = 12, CornerStreet = "Another Street", ConstructorCompany = "City 1", SharedExpenses = 150, Flats = new List<Flat>() },
                new Building() { Id = Guid.NewGuid(), Name = "Building 2", Street = "Street2", DoorNumber = 12, CornerStreet = "Another Street", ConstructorCompany = "City 2", SharedExpenses = 152, Flats = new List<Flat>() }

            };

            mockContext.Setup(x => x.Buildings).ReturnsDbSet(buildings);

            IEnumerable<Building> result = buildingRepository.GetAllBuildings();

            CollectionAssert.AreEqual(buildings, result.ToList());

        }

        [TestMethod]
        public void CreateBuildingTestOk()
        {
            Building building = new Building() { Id = Guid.NewGuid(), Name = "Building 1", Street = "Street", DoorNumber = 12, CornerStreet = "Another Street", ConstructorCompany = "City 1", SharedExpenses = 150, Flats = new List<Flat>() };

            mockContext.Setup(x => x.Buildings).ReturnsDbSet(new List<Building>() { });

            buildingRepository = new BuildingRepository(mockContext.Object);

            Building result = buildingRepository.CreateBuilding(building);

            Assert.AreEqual(building, result);
        }


        [TestMethod]
        public void UpdateBuildingTestOk()
        {
            Building expected = new Building() { Id = Guid.NewGuid(), Name = "Building 1", Street = "Street", DoorNumber = 12, CornerStreet = "Another Street", ConstructorCompany = "City 1", SharedExpenses = 200, Flats = new List<Flat>() };

            mockContext.Setup(x => x.Buildings).ReturnsDbSet(new List<Building>() { new Building() { Id = expected.Id, Name = "Building 1", Street = "Street", DoorNumber = 12, CornerStreet = "Another Street", ConstructorCompany = "City 1", SharedExpenses = 150, Flats = new List<Flat>() } });

            Building result = buildingRepository.UpdateBuilding(expected);

            Assert.AreEqual(expected.SharedExpenses, result.SharedExpenses);
        }

        [TestMethod]
        public void DeleteBuildingTestOk()
        {
            Guid id = Guid.NewGuid();

            Building toDeleteBuilding = new Building() { Id = id, Name = "Building 1", Street = "Street", DoorNumber = 12, CornerStreet = "Another Street", ConstructorCompany = "City 1", SharedExpenses = 150, Flats = new List<Flat>() };

            mockContext.Setup(x => x.Buildings).ReturnsDbSet(new List<Building>() { new Building() { Id = id, Name = "Building 1", Street = "Street", DoorNumber = 12, CornerStreet = "Another Street", ConstructorCompany = "City 1", SharedExpenses = 150, Flats = new List<Flat>() } });

            buildingRepository.DeleteBuilding(toDeleteBuilding);

            mockContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void GetBuildingByIdTestNotFound()
        {
            Guid id = Guid.NewGuid();

            mockContext.Setup(x => x.Buildings).ReturnsDbSet(new List<Building>() { });

            Exception exception = null;

            try
            {
                Building result = buildingRepository.GetBuildingById(id);
            }catch(Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.AreEqual(exception.Message, "Building not found");
        }

        [TestMethod]
        public void GetFlatByBuildingAndFlatIdTestOk()
        {
            Guid buildingId = Guid.NewGuid();
            Guid flatId = Guid.NewGuid();

            Building building = new Building()
            {
                Id = buildingId,
                Name = "Building 1",
                Street = "Street", DoorNumber = 12, CornerStreet = "Another Street",
                ConstructorCompany = "City 1",
                SharedExpenses = 150,
                Flats = new List<Flat>()
                    {
                        new Flat() { Id = flatId }
                    }
            };

            mockContext.Setup(x => x.Buildings).ReturnsDbSet(new List<Building> { building });

            Flat result = buildingRepository.GetFlatByBuildingAndFlatId(buildingId, flatId);

            Assert.AreEqual(building.Flats.First(), result);

        }

        [TestMethod]
        public void GetFlatByBuildingAndFlatIdTestBuildingNotFound()
        {
            Guid buildingId = Guid.NewGuid();
            Guid flatId = Guid.NewGuid();

            Building building = new Building()
            {
                Id = buildingId,
                Name = "Building 1",
                Street = "Street", DoorNumber = 12, CornerStreet = "Another Street",
                ConstructorCompany = "City 1",
                SharedExpenses = 150,
                Flats = new List<Flat>()
                    {
                        new Flat() { Id = flatId }
                    }
            };

            mockContext.Setup(x => x.Buildings).ReturnsDbSet(new List<Building> { });

            try
            {
                Flat result = buildingRepository.GetFlatByBuildingAndFlatId(buildingId, flatId);
            }catch(Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ArgumentException));
                Assert.AreEqual(e.Message, "Building not found");
            }

        }

        [TestMethod]
        public void GetFlatByBuildingAndFlatIdTestFlatNotFound()
        {
            Guid buildingId = Guid.NewGuid();
            Guid flatId = Guid.NewGuid();

            Building building = new Building()
            {
                Id = buildingId,
                Name = "Building 1",
                Street = "Street", 
                DoorNumber = 12, 
                CornerStreet = "Another Street",
                ConstructorCompany = "City 1",
                SharedExpenses = 150,
                Flats = new List<Flat>() { }
            };

            mockContext.Setup(x => x.Buildings).ReturnsDbSet(new List<Building> { building });

            try
            {
                Flat result = buildingRepository.GetFlatByBuildingAndFlatId(buildingId, flatId);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ArgumentException));
                Assert.AreEqual(e.Message, "Flat not found");
            }

        }

        [TestMethod]
        public void UpdateFlatTestOk()
        {
            Guid buildingId = Guid.NewGuid();
            Guid flatId = Guid.NewGuid();

            Building building = new Building()
            {
                Id = buildingId,
                Name = "Building 1",
                Street = "Street", 
                DoorNumber = 12, 
                CornerStreet = "Another Street",
                ConstructorCompany = "City 1",
                SharedExpenses = 150,
                Flats = new List<Flat>()
                {
                    new Flat() { Id = flatId }
                }
            };

            mockContext.Setup(x => x.Buildings).ReturnsDbSet(new List<Building> { building });

            Flat flat = new Flat()
            {
                Id = flatId,
                Floor = 3,
                Number = 304,
                HasBalcony = true,
                OwnerEmail = "pedro@mail.com",
                OwnerName = "Pedro",
                OwnerSurname = "De Los Naranjos"
            };

            Flat result = buildingRepository.UpdateFlat(flat);

            Assert.IsTrue(flat.Id.Equals(result.Id) &&
                flat.Floor.Equals(result.Floor) &&
                flat.Number.Equals(result.Number) &&
                flat.HasBalcony.Equals(result.HasBalcony) &&
                flat.OwnerEmail.Equals(result.OwnerEmail) &&
                flat.OwnerName.Equals(result.OwnerName) &&
                flat.OwnerSurname.Equals(result.OwnerSurname)
            );

        }


    }
}