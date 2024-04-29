using BusinessLogic;
using RepositoryInterfaces;
using CustomExceptions.BusinessLogic;
using Domain;
using Moq;


namespace BusinessLogicTest;

[TestClass]
public class BuildingLogicTest
{

    private Mock<IBuildingRepository> buildingRepositoryMock;
    private BuildingLogic buildingLogic;

    [TestInitialize]
    public void Initialize()
    {
        buildingRepositoryMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
        buildingLogic = new BuildingLogic(buildingRepositoryMock.Object);
    }

    [TestMethod]
    public void CreateBuildingTestOk()
    {
        Building building = new Building() { Id = Guid.NewGuid(), Name = "Mirador",
            ConstructorCompany = "A Company",
            Street = "Street", 
            DoorNumber = 12, 
            CornerStreet = "Another Street",
            SharedExpenses = 100
        };
        Building expected = new Building() { Id = building.Id, 
            Name = "Mirador",
            ConstructorCompany = "A Company",
            Street = "Street", 
            DoorNumber = 12, 
            CornerStreet = "Another Street",
            SharedExpenses = 100
        };

        buildingRepositoryMock.Setup(x => x.GetAllBuildings()).Returns(new List<Building>());
        buildingRepositoryMock.Setup(x => x.CreateBuilding(building)).Returns(expected);
        buildingRepositoryMock.Setup(x => x.CreateFlat(It.IsAny<Flat>())).Returns(new Flat() { Building = building });

        Building result = buildingLogic.CreateBuilding(building, amountOfFlats: 1, It.IsAny<Guid>());

        buildingRepositoryMock.VerifyAll();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void CreateBuildingTestBuildingWithEmptyName()
    {
        Building building = new Building() { Name = "", 
            ConstructorCompany = "A Company",
            Street = "Street", 
            DoorNumber = 12, 
            CornerStreet = "Another Street",
            SharedExpenses = 100
        };

        Exception exception = null;

        try
        {
            Building result = buildingLogic.CreateBuilding(building, amountOfFlats: 1, It.IsAny<Guid>());
        }
        catch (Exception e)
        {
            exception = e;
        }
        
        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Building name cannot be empty");
    }

    [TestMethod]
    public void CreateBuildingTestBuildingWithNegativeSharedExpenses()
    {
        Building building = new Building() { Name = "Mirador",
            SharedExpenses = -1,
            ConstructorCompany = "A Company",
            Street = "Street",
            DoorNumber = 12,
            CornerStreet = "Another Street"
        };

        Exception exception = null;

        try
        {
            Building result = buildingLogic.CreateBuilding(building, amountOfFlats: 1, It.IsAny<Guid>());
        }
        catch (Exception e)
        {
            exception = e;
        }

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Shared expenses cannot be negative");
    }

    [TestMethod]
    public void CreateBuildingTestBuildingWithAlreadyExistingName()
    {
        Building building = new Building() { Name = "Mirador",
            ConstructorCompany = "A Company",
            Street = "Street", 
            DoorNumber = 12, 
            CornerStreet = "Another Street",
            SharedExpenses = 100
        };
        buildingRepositoryMock.Setup(x => x.GetAllBuildings()).Returns(new List<Building> { new Building() { Name = "Mirador" } });

        Exception exception = null;

        try
        {
            Building result = buildingLogic.CreateBuilding(building, amountOfFlats: 1, It.IsAny<Guid>());
        }
        catch (Exception e)
        {
            exception = e;
        }

        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Building with same name already exists");
    }

    [TestMethod]
    public void CreateBuildingTestBuildingWithEmptyStreetName()
    {
        Building building = new Building() { Street = "",
            ConstructorCompany = "A Company",
            Name = "A name",
            SharedExpenses = 100
        };

        Exception exception = null;

        try
        {
            Building result = buildingLogic.CreateBuilding(building, amountOfFlats: 1, It.IsAny<Guid>());
        }
        catch (Exception e)
        {
            exception = e;
        }

        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Building street cannot be empty");
    }

    [TestMethod]
    public void CreateBuildingTestBuildingWithEmptyConstructorCompany()
    {
        Building building = new Building() { ConstructorCompany = "",
            Name = "A Name",
            Street = "Street", 
            DoorNumber = 12, 
            CornerStreet = "Another Street",
            SharedExpenses = 100
        };

        Exception exception = null;

        try
        {
            Building result = buildingLogic.CreateBuilding(building, amountOfFlats: 1, It.IsAny<Guid>());
        }
        catch (Exception e)
        {
            exception = e;
        }

        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Building constructor company cannot be empty");
    }

    [TestMethod]
    public void CreateBuildingTestBuildingWithNoFlats()
    {
        Building building = new Building();


        Exception exception = null;

        try
        {
            Building result = buildingLogic.CreateBuilding(building, amountOfFlats: 0, It.IsAny<Guid>());
        }
        catch (Exception e)
        {
            exception = e;
        }

        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "It is not possible to create a building with no flats");
    }

    [TestMethod]
    public void CreateBuildingTestBuildingWithAlreadyExistingAddress()
    {
        Building building = new Building()
        {
            Name = "Mirador2",
            ConstructorCompany = "A Company",
            Street = "Street",
            DoorNumber = 12,
            CornerStreet = "Another Street",
            SharedExpenses = 100
        };

        buildingRepositoryMock.Setup(x => x.GetAllBuildings()).Returns(new List<Building> { new Building() { Name = "Mirador",
            ConstructorCompany = "A Company",
            Street = "Street",
            DoorNumber = 12,
            CornerStreet = "Another",
            SharedExpenses = 100
        } });

        Exception exception = null;

        try
        {
            Building result = buildingLogic.CreateBuilding(building, amountOfFlats: 1, It.IsAny<Guid>());
        }
        catch (Exception e)
        {
            exception = e;
        }

        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Building with same address already exists");
    }

    [TestMethod]
    public void CreateBuildingTestBuildingWithAlreadyExistingCoordinates()
    {
        Building building = new Building()
        {
            Name = "Mirador2",
            ConstructorCompany = "A Company",
            Street = "Street",
            DoorNumber = 12,
            CornerStreet = "Another Street",
            Latitude = 32,
            Longitude = 34,
            SharedExpenses = 100
        };

        buildingRepositoryMock.Setup(x => x.GetAllBuildings()).Returns(new List<Building> { new Building() { Name = "Mirador",
            ConstructorCompany = "A Company",
            Street = "Street",
            DoorNumber = 132,
            CornerStreet = "Another",
            Latitude = 32,
            Longitude = 34,
            SharedExpenses = 100
        } });

        Exception exception = null;

        try
        {
            Building result = buildingLogic.CreateBuilding(building, amountOfFlats: 1, It.IsAny<Guid>());
        }
        catch (Exception e)
        {
            exception = e;
        }

        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Building with same coordinates already exists");
    }


    [TestMethod]
    public void CreateBuildingTestBuildingWithInvalidCoordinatesLatitudeGreaterThanNinety()
    {
        Building building = new Building()
        {
            Name = "Mirador2",
            ConstructorCompany = "A Company",
            Street = "Street",
            DoorNumber = 12,
            CornerStreet = "Another Street",
            Latitude = 91,
            Longitude = 34,
            SharedExpenses = 100
        };

        Exception exception = null;

        try
        {
            Building result = buildingLogic.CreateBuilding(building, amountOfFlats: 1, It.IsAny<Guid>());
        }
        catch (Exception e)
        {
            exception = e;
        }

        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Invalid latitude, must be between -90 and 90 degrees");
    }

    [TestMethod]
    public void CreateBuildingTestBuildingWithInvalidCoordinatesLatitudeSmallerThanMinusNinety()
    {
        Building building = new Building()
        {
            Name = "Mirador2",
            ConstructorCompany = "A Company",
            Street = "Street",
            DoorNumber = 12,
            CornerStreet = "Another Street",
            Latitude = -91,
            Longitude = 34,
            SharedExpenses = 100
        };

        Exception exception = null;

        try
        {
            Building result = buildingLogic.CreateBuilding(building, amountOfFlats: 1, It.IsAny<Guid>());
        }
        catch (Exception e)
        {
            exception = e;
        }

        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Invalid latitude, must be between -90 and 90 degrees");
    }

    [TestMethod]
    public void CreateBuildingTestBuildingWithInvalidCoordinatesLatitudeSmallerThanMinusAHundredAndEighty()
    {
        Building building = new Building()
        {
            Name = "Mirador2",
            ConstructorCompany = "A Company",
            Street = "Street",
            DoorNumber = 12,
            CornerStreet = "Another Street",
            Latitude = -90,
            Longitude = -181,
            SharedExpenses = 100
        };

        Exception exception = null;

        try
        {
            Building result = buildingLogic.CreateBuilding(building, amountOfFlats: 1, It.IsAny<Guid>());
        }
        catch (Exception e)
        {
            exception = e;
        }

        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Invalid longitude, must be between -180 and 180 degrees");
    }

    [TestMethod]
    public void CreateBuildingTestBuildingWithInvalidCoordinatesLongitudeGreaterThanAHundredAndEighty()
    {
        Building building = new Building()
        {
            Name = "Mirador2",
            ConstructorCompany = "A Company",
            Street = "Street",
            DoorNumber = 12,
            CornerStreet = "Another Street",
            Latitude = 90,
            Longitude = 181,
            SharedExpenses = 100
        };

        Exception exception = null;

        try
        {
            Building result = buildingLogic.CreateBuilding(building, amountOfFlats: 1, It.IsAny<Guid>());
        }
        catch (Exception e)
        {
            exception = e;
        }

        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Invalid longitude, must be between -180 and 180 degrees");
    }
    [TestMethod]
    public void GetAllBuildingsTestOk()
    {
        User user = new User() { Id = Guid.NewGuid(), Role = Role.Manager };
        IEnumerable<Building> buildings = new List<Building> { new Building() { Name = "Mirador", BuildingManager = user } };

        buildingRepositoryMock.Setup(x => x.GetAllBuildings()).Returns(buildings);

        IEnumerable<Building> result = buildingLogic.GetAllBuildings(user.Id);

        buildingRepositoryMock.VerifyAll();

        CollectionAssert.AreEqual(buildings.ToList(), result.ToList());

    }

    [TestMethod]
    public void GetBuildingByIdTestOk()
    {
        Building building = new Building() { Name = "Mirador" };

        buildingRepositoryMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Returns(building);

        Building result = buildingLogic.GetBuildingById(building.Id);

        buildingRepositoryMock.VerifyAll();

        Assert.AreEqual(building, result);
    }

    [TestMethod]
    public void UpdateBuildingTestOk()
    {
        Building building = new Building() { Name = "Mirador", SharedExpenses = 200 };
        Building expected = new Building() { Name = "Mirador", SharedExpenses = 300 };

        buildingRepositoryMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Returns(building);
        buildingRepositoryMock.Setup(x => x.UpdateBuilding(It.IsAny<Building>())).Returns(expected);

        Building result = buildingLogic.UpdateBuilding(It.IsAny<Guid>(), It.IsAny<float>());

        buildingRepositoryMock.VerifyAll();

        Assert.AreEqual(expected, result);
        Assert.AreEqual(expected.SharedExpenses, result.SharedExpenses);
    }


    [TestMethod]
    public void UpdateBuildingTestNotFound()
    {
        buildingRepositoryMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Throws(new ArgumentException("Building not found"));

        Exception exception = null;

        try
        {
            Building result = buildingLogic.UpdateBuilding(It.IsAny<Guid>(), 300);
        }
        catch (Exception e)
        {
            exception = e;
        }

        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(ArgumentException));
        Assert.AreEqual(exception.Message, "Building not found");
    }

    [TestMethod]
    public void UpdateBuildingTestNegativeSharedExpenses()
    {

        Building building = new Building();

        Exception exception = null;

        try
        {
            Building result = buildingLogic.UpdateBuilding(building.Id, sharedExpenses: -1);
        }
        catch (Exception e)
        {
            exception = e;
        }

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Shared expenses cannot be negative");
    }

    [TestMethod]
    public void DeleteBuildingTestOk()
    {
        buildingRepositoryMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Returns(new Building());
        buildingRepositoryMock.Setup(x => x.DeleteBuilding(It.IsAny<Building>()));

        buildingLogic.DeleteBuilding(It.IsAny<Guid>());

        buildingRepositoryMock.VerifyAll();

    }

    [TestMethod]
    public void DeleteBuildingTestNotFound()
    {
        buildingRepositoryMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Throws(new ArgumentException("Building not found"));

        Exception exception = null;

        try
        {
            buildingLogic.DeleteBuilding(It.IsAny<Guid>());
        }
        catch (Exception e)
        {
            exception = e;
        }


        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(ArgumentException));
        Assert.AreEqual(exception.Message, "Building not found");


    }

    [TestMethod]
    public void GetFlatByFlatIdTestOk()
    {
        Flat flat = new Flat();

        buildingRepositoryMock.Setup(x => x.GetFlatByFlatId(It.IsAny<Guid>())).Returns(flat);

        Flat result = buildingLogic.GetFlatByFlatId(It.IsAny<Guid>());

        buildingRepositoryMock.VerifyAll();

        Assert.AreEqual(flat, result);
    }

    [TestMethod]
    public void UpdateFlatByFlatIdTestOk()
    {
        Flat toUpdateFlat = new Flat() {
            Floor = 3, 
            Number = 303, 
            Bathrooms = 3, 
            HasBalcony = true, 
            OwnerEmail = "pedro@mail.com", 
            OwnerName = "Pedro", 
            OwnerSurname = "De Los Naranjos",
            Building = new Building()
        };
        Flat flat = new Flat() { 
            Floor = 3, 
            Number = 304,
            Bathrooms = 3,
            HasBalcony = true,
            OwnerEmail = "pedro@mail.com",
            OwnerName = "Pedro",
            OwnerSurname = "De Los Naranjos"
        };
        Flat expected = new Flat() { 
            Floor = 3, 
            Number = 304,
            Bathrooms = 3,
            HasBalcony = true,
            OwnerEmail = "pedro@mail.com",
            OwnerName = "Pedro",
            OwnerSurname = "De Los Naranjos"
        };

        buildingRepositoryMock.Setup(x => x.GetAllBuildingFlats(It.IsAny<Guid>())).Returns(new List<Flat>() { toUpdateFlat });
        buildingRepositoryMock.Setup(x => x.GetFlatByFlatId(It.IsAny<Guid>())).Returns(toUpdateFlat);
        buildingRepositoryMock.Setup(x => x.UpdateFlat(It.IsAny<Flat>())).Returns(expected);

        Flat result = buildingLogic.UpdateFlat(It.IsAny<Guid>(), flat);

        buildingRepositoryMock.VerifyAll();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void GetAllBuildingFlatsTestOk()
    {
        Guid buildingId = Guid.NewGuid();
        IEnumerable<Flat> flats = new List<Flat> { new Flat() { Building = new Building() { Id = buildingId } } };

        buildingRepositoryMock.Setup(x => x.GetAllBuildingFlats(buildingId)).Returns(flats);

        IEnumerable<Flat> result = buildingLogic.GetAllBuildingFlats(buildingId);

        buildingRepositoryMock.VerifyAll();

        Assert.AreEqual(flats, result);
    }

    [TestMethod]
    public void UpdateFlatByFlatIdTestNotFoundFlat()
    {
        Flat flat = new Flat()
        {
            Floor = 3,
            Number = 304,
            Bathrooms = 3,
            HasBalcony = true,
            OwnerEmail = "pedro@mail.com",
            OwnerName = "Pedro",
            OwnerSurname = "De Los Naranjos"
        };

        buildingRepositoryMock.Setup(x => x.GetFlatByFlatId(It.IsAny<Guid>())).Throws(new ArgumentException("Flat not found"));

        try
        {
            Flat result = buildingLogic.UpdateFlat(It.IsAny<Guid>(), flat);
        }
        catch (Exception e)
        {
            Assert.IsInstanceOfType(e, typeof(ArgumentException));
            Assert.AreEqual(e.Message, "Flat not found");
        }
    }

    [TestMethod]
    public void UpdateFlatTestFlatWithSameNumberAlreadyExists()
    {
        Flat flat = new Flat() {Floor = 3, Number = 303, Bathrooms = 3, HasBalcony = true, OwnerEmail = "pedro@mail.com", OwnerName = "Pedro", OwnerSurname = "De Los Naranjos", Building = new Building() };

        Flat toChangeFlat = new Flat() { Building = flat.Building };

        Flat anotherFlat = new Flat() { Id = Guid.NewGuid(), Floor = 3, Number = 303, Building = flat.Building };

        buildingRepositoryMock.Setup(x => x.GetFlatByFlatId(It.IsAny<Guid>())).Returns(toChangeFlat);

        buildingRepositoryMock.Setup(x => x.GetAllBuildingFlats(It.IsAny<Guid>())).Returns(new List<Flat>() { anotherFlat, toChangeFlat } );

        Exception exception = null;

        try
        {
            Flat result = buildingLogic.UpdateFlat(toChangeFlat.Id, flat);
        }
        catch(Exception e)
        {
            exception = e;
        }
        
        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Flat with same number already exists");
    }

    [TestMethod]
    public void UpdateFlatByFlatIdTestFlatWithInvalidFloorNumber()
    {
        Flat flat = new Flat() { Number = 303, Floor = 4,
            Bathrooms = 3,
            HasBalcony = true,
            OwnerEmail = "pedro@mail.com",
            OwnerName = "Pedro",
            OwnerSurname = "De Los Naranjos"
        };

        Flat toChangeFlat = new Flat();

        Building building = new Building();

        Exception exception = null;

        try
        {
            Flat result = buildingLogic.UpdateFlat(toChangeFlat.Id, flat);
        }
        catch (Exception e)
        {
            exception = e;
        }

        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Invalid flat number, first digit must be floor number");
    }

    [TestMethod]
    public void UpdateFlatByFlatIdTestFlatWithEmptyOwnerName()
    {
        Flat flat = new Flat() { Number = 303, 
            Floor = 3, 
            OwnerName = "",
            Bathrooms = 3,
            HasBalcony = true,
            OwnerEmail = "pedro@mail.com",
            OwnerSurname = "De Los Naranjos"
        };

        Flat toChangeFlat = new Flat();

        Building building = new Building();

        Exception exception = null;

        try
        {
            Flat result = buildingLogic.UpdateFlat(toChangeFlat.Id, flat);
        }
        catch (Exception e)
        {
            exception = e;
        }

        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Owner name cannot be empty");
    }

    [TestMethod]
    public void UpdateFlatByFlatIdTestFlatWithEmptyEmail()
    {
        Flat flat = new Flat()
        {
            Number = 303,
            Floor = 3,
            OwnerName = "Pedro",
            Bathrooms = 3,
            HasBalcony = true,
            OwnerEmail = "",
            OwnerSurname = "De Los Naranjos"
        };

        Flat toChangeFlat = new Flat();

        Building building = new Building();

        Exception exception = null;

        try
        {
            Flat result = buildingLogic.UpdateFlat(toChangeFlat.Id, flat);
        }
        catch (Exception e)
        {
            exception = e;
        }

        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Owner email cannot be empty");
    }


    [TestMethod]
    public void UpdateFlatByFlatIdTestFlatWithEmptyOwnerSurname()
    {
        Flat flat = new Flat()
        {
            Number = 303,
            Floor = 3,
            OwnerName = "Pedro Sin Naranjos",
            Bathrooms = 3,
            HasBalcony = true,
            OwnerEmail = "pedro@mail.com",
            OwnerSurname = ""
        };

        Flat toChangeFlat = new Flat();

        Building building = new Building();

        Exception exception = null;

        try
        {
            Flat result = buildingLogic.UpdateFlat(toChangeFlat.Id, flat);
        }
        catch (Exception e)
        {
            exception = e;
        }

        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Owner surname cannot be empty");
    }

    [TestMethod]
    public void UpdateFlatByFlatIdTestFlatWithNegativeNumberOfBathrooms()
    {
        Flat flat = new Flat()
        {
            Number = 303,
            Floor = 3,
            OwnerName = "Pedro",
            Bathrooms = 0,
            HasBalcony = true,
            OwnerEmail = "pedro@mail.com",
            OwnerSurname = "De Los Naranjos"
        };

        Flat toChangeFlat = new Flat();

        Building building = new Building();

        Exception exception = null;

        try
        {
            Flat result = buildingLogic.UpdateFlat(toChangeFlat.Id, flat);
        }
        catch (Exception e)
        {
            exception = e;
        }

        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Number of bathrooms cannot be negative or zero");
    }


    [TestMethod]
    public void UpdateFlatByFlatIdTestOwnerEmailWithNoAt()
    {
        Flat flat = new Flat()
        {
            Number = 303,
            Floor = 3,
            OwnerName = "Pedro",
            Bathrooms = 1,
            HasBalcony = true,
            OwnerEmail = "pedromail.com",
            OwnerSurname = "De Los Naranjos"
        };

        Flat toChangeFlat = new Flat();

        Building building = new Building();

        Exception exception = null;

        try
        {
            Flat result = buildingLogic.UpdateFlat(toChangeFlat.Id, flat);
        }
        catch (Exception e)
        {
            exception = e;
        }

        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Invalid email, must contain @");
    }

    [TestMethod]
    public void UpdateFlatByFlatIdTestOwnerEmailWithNoDot()
    {
        Flat flat = new Flat()
        {
            Number = 303,
            Floor = 3,
            OwnerName = "Pedro",
            Bathrooms = 1,
            HasBalcony = true,
            OwnerEmail = "pedro@mailcom",
            OwnerSurname = "De Los Naranjos"
        };

        Flat toChangeFlat = new Flat();

        Building building = new Building();

        Exception exception = null;

        try
        {
            Flat result = buildingLogic.UpdateFlat(toChangeFlat.Id, flat);
        }
        catch (Exception e)
        {
            exception = e;
        }

        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Invalid email, must contain .");
    }

}