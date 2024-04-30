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
                new Building() { Id = Guid.NewGuid(), Name = "Building 1", Street = "Street", DoorNumber = 12, CornerStreet = "Another Street", ConstructorCompany = "City 1", SharedExpenses = 150 },
                new Building() { Id = Guid.NewGuid(), Name = "Building 2", Street = "Street2", DoorNumber = 12, CornerStreet = "Another Street", ConstructorCompany = "City 2", SharedExpenses = 152 }

            };

            mockContext.Setup(x => x.Buildings).ReturnsDbSet(buildings);

            IEnumerable<Building> result = buildingRepository.GetAllBuildings();

            mockContext.Verify(x => x.Buildings, Times.Once());

            CollectionAssert.AreEqual(buildings, result.ToList());

        }

        [TestMethod]
        public void CreateBuildingTestOk()
        {
            Building building = new Building() { Id = Guid.NewGuid(), Name = "Building 1", Street = "Street", DoorNumber = 12, CornerStreet = "Another Street", Latitude = -34.88449565, Longitude = -56.1587038155517, ConstructorCompany = "City 1", SharedExpenses = 150 };

            mockContext.Setup(x => x.Buildings).ReturnsDbSet(new List<Building>() { });

            buildingRepository = new BuildingRepository(mockContext.Object);

            Building result = buildingRepository.CreateBuilding(building);

            mockContext.Verify(x => x.Buildings, Times.Once());
            mockContext.Verify(x => x.Buildings.Add(building), Times.Once());
            mockContext.Verify(x => x.SaveChanges(), Times.Once());

            Assert.AreEqual(building, result);
        }


        [TestMethod]
        public void UpdateBuildingTestOk()
        {
            Building expected = new Building() { Id = Guid.NewGuid(), Name = "Building 1", Street = "Street", DoorNumber = 12, CornerStreet = "Another Street", ConstructorCompany = "City 1", SharedExpenses = 200 };

            mockContext.Setup(x => x.Buildings).ReturnsDbSet(new List<Building>() { new Building() { Id = expected.Id, Name = "Building 1", Street = "Street", DoorNumber = 12, CornerStreet = "Another Street", ConstructorCompany = "City 1", SharedExpenses = 150 } });
            mockContext.Setup(x => x.Update(expected));
            mockContext.Setup(x => x.SaveChanges()).Returns(1);


            Building result = buildingRepository.UpdateBuilding(expected);

            mockContext.Verify(x => x.Update(expected), Times.Once());
            mockContext.Verify(x => x.SaveChanges(), Times.Once());
            Assert.AreEqual(expected.SharedExpenses, result.SharedExpenses);
        }

        [TestMethod]
        public void DeleteBuildingTestOk()
        {
            Guid id = Guid.NewGuid();

            Building toDeleteBuilding = new Building() { Id = id, Name = "Building 1", Street = "Street", DoorNumber = 12, CornerStreet = "Another Street", ConstructorCompany = "City 1", SharedExpenses = 150 };

            mockContext.Setup(x => x.Flats).ReturnsDbSet(new List<Flat>() { new Flat() { Building = toDeleteBuilding } });
            mockContext.Setup(x => x.Buildings).ReturnsDbSet(new List<Building>() { toDeleteBuilding });
            mockContext.Setup(mockContext => mockContext.SaveChanges()).Returns(1);

            buildingRepository.DeleteBuilding(toDeleteBuilding);

            mockContext.Verify(x => x.SaveChanges(), Times.Exactly(2));
            mockContext.Verify(x => x.Buildings.Remove(toDeleteBuilding), Times.Once());
            mockContext.Verify(x => x.Flats.Remove(It.IsAny<Flat>()), Times.Once());
            mockContext.Verify(mockContext => mockContext.Flats, Times.Exactly(2));
            mockContext.Verify(mockContext => mockContext.Buildings, Times.Once());
        }

        [TestMethod]
        public void GetBuildingByIdTestOk()
        {
            Guid id = Guid.NewGuid();

            Building building = new Building()
            {
                Id = id,
                Name = "Building 1",
                Street = "Street",
                DoorNumber = 12,
                CornerStreet = "Another Street",
                ConstructorCompany = "City 1",
                SharedExpenses = 150
            };

            mockContext.Setup(x => x.Buildings).ReturnsDbSet(new List<Building>() { building });

            Building result = buildingRepository.GetBuildingById(id);

            mockContext.Verify(x => x.Buildings, Times.Once());
            Assert.AreEqual(building, result);
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

            mockContext.Verify(x => x.Buildings, Times.Once());
            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.AreEqual(exception.Message, "Building not found");
        }

        [TestMethod]
        public void GetFlatByFlatIdTestOk()
        {
            Guid buildingId = Guid.NewGuid();
            Guid flatId = Guid.NewGuid();

            Building building = new Building()
            {
                Id = buildingId,
                Name = "Building 1",
                Street = "Street", DoorNumber = 12, CornerStreet = "Another Street",
                ConstructorCompany = "City 1",
                SharedExpenses = 150
            };

            Flat flat = new Flat()
            {
                Id = flatId,
                Floor = 3,
                Number = 304,
                HasBalcony = true,
                OwnerEmail = "pedro@mail.com",
                OwnerName = "Pedro",
                OwnerSurname = "De Los Naranjos",
                Building = building
            };

            mockContext.Setup(x => x.Flats).ReturnsDbSet(new List<Flat> { flat });

            Flat result = buildingRepository.GetFlatByFlatId(flatId);

            mockContext.Verify(x => x.Flats, Times.Once());

            Assert.AreEqual(flat, result);

        }

        [TestMethod]
        public void GetFlatByFlatIdTestBuildingNotFound()
        {
            Guid buildingId = Guid.NewGuid();
            Guid flatId = Guid.NewGuid();

            Building building = new Building()
            {
                Id = buildingId,
                Name = "Building 1",
                Street = "Street", DoorNumber = 12, CornerStreet = "Another Street",
                ConstructorCompany = "City 1",
                SharedExpenses = 150
            };

            mockContext.Setup(x => x.Flats).ReturnsDbSet(new List<Flat> { });

            try
            {
                Flat result = buildingRepository.GetFlatByFlatId(flatId);
            }catch(Exception e)
            {
                mockContext.Verify(x => x.Flats, Times.Once());
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
                SharedExpenses = 150
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
                OwnerSurname = "De Los Naranjos",
                Building = building
            };

            Flat result = buildingRepository.UpdateFlat(flat);

            mockContext.Verify(x => x.Update(flat), Times.Once());
            mockContext.Verify(x => x.SaveChanges(), Times.Once());

            Assert.IsTrue(flat.Id.Equals(result.Id) &&
                flat.Floor.Equals(result.Floor) &&
                flat.Number.Equals(result.Number) &&
                flat.HasBalcony.Equals(result.HasBalcony) &&
                flat.OwnerEmail.Equals(result.OwnerEmail) &&
                flat.OwnerName.Equals(result.OwnerName) &&
                flat.OwnerSurname.Equals(result.OwnerSurname) &&
                flat.Building.Equals(result.Building)
            );

        }

        [TestMethod]
        public void GetAllBuildingFlatsTestOk()
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
                SharedExpenses = 150
            };

            Flat flat = new Flat()
            {
                Id = flatId,
                Floor = 3,
                Number = 304,
                HasBalcony = true,
                OwnerEmail = "",
                OwnerName = "Pedro",
                OwnerSurname = "De Los Naranjos",
                Building = building
            };

            mockContext.Setup(x => x.Flats).ReturnsDbSet(new List<Flat> { flat });

            IEnumerable<Flat> result = buildingRepository.GetAllBuildingFlats(buildingId);

            mockContext.Verify(x => x.Flats, Times.Once());

            CollectionAssert.AreEqual(new List<Flat> { flat }, result.ToList());

        }

        [TestMethod]
        public void CreateFlatTestOk()
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
                SharedExpenses = 150
            };

            Flat flat = new Flat()
            {
                Id = flatId,
                Floor = 3,
                Number = 304,
                HasBalcony = true,
                OwnerEmail = "",
                OwnerName = "Pedro",
                OwnerSurname = "De Los Naranjos",
                Building = building
            };

            mockContext.Setup(x => x.Flats).ReturnsDbSet(new List<Flat> { });

            Flat result = buildingRepository.CreateFlat(flat);

            mockContext.Verify(x => x.Flats, Times.Once());
            mockContext.Verify(x => x.Flats.Add(flat), Times.Once());

            Assert.AreEqual(flat, result);

        }

        [TestMethod]
        public void GetAllFlatsTestOk()
        {
            List<Flat> expectedList = new List<Flat> { new Flat() { Id = Guid.NewGuid(), Floor = 3, Number = 304, HasBalcony = true, OwnerEmail = "", OwnerName = "Pedro", OwnerSurname = "De Los Naranjos" } };
            mockContext.Setup(x => x.Flats).ReturnsDbSet(expectedList);

            List<Flat> result = buildingRepository.GetAllFlats().ToList();

            CollectionAssert.AreEqual(expectedList.ToList(), result);
        }
    }
}