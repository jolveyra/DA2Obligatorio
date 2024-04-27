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
            Direction = "A direction",
            SharedExpenses = 100
        };
        Building expected = new Building() { Id = building.Id, Name = "Mirador",
            ConstructorCompany = "A Company",
            Direction = "A direction",
            SharedExpenses = 100
        };

        buildingRepositoryMock.Setup(x => x.GetAllBuildings()).Returns(new List<Building>());
        buildingRepositoryMock.Setup(x => x.CreateBuilding(building)).Returns(expected);

        Building result = buildingLogic.CreateBuilding(building, amountOfFlats: 1);

        buildingRepositoryMock.VerifyAll();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void CreateBuildingTestBuildingWithEmptyName()
    {
        Building building = new Building() { Name = "", 
            ConstructorCompany = "A Company",
            Direction = "A direction",
            SharedExpenses = 100
        };

        Exception exception = null;

        try
        {
            Building result = buildingLogic.CreateBuilding(building, amountOfFlats: 1);
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
            Direction = "A direction"
        };

        Exception exception = null;

        try
        {
            Building result = buildingLogic.CreateBuilding(building, amountOfFlats: 1);
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
            Direction = "A direction",
            SharedExpenses = 100
        };
        buildingRepositoryMock.Setup(x => x.GetAllBuildings()).Returns(new List<Building> { new Building() { Name = "Mirador" } });

        Exception exception = null;

        try
        {
            Building result = buildingLogic.CreateBuilding(building, amountOfFlats: 1);
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
    public void CreateBuildingTestBuildingWithEmptyDirection()
    {
        Building building = new Building() { Direction = "",
            ConstructorCompany = "A Company",
            Name = "A name",
            SharedExpenses = 100
        };

        Exception exception = null;

        try
        {
            Building result = buildingLogic.CreateBuilding(building, amountOfFlats: 1);
        }
        catch (Exception e)
        {
            exception = e;
        }

        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Building direction cannot be empty");
    }

    [TestMethod]
    public void CreateBuildingTestBuildingWithEmptyConstructorCompany()
    {
        Building building = new Building() { ConstructorCompany = "",
            Name = "A Name",
            Direction = "A direction",
            SharedExpenses = 100
        };

        Exception exception = null;

        try
        {
            Building result = buildingLogic.CreateBuilding(building, amountOfFlats: 1);
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
            Building result = buildingLogic.CreateBuilding(building, amountOfFlats: 0);
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
    public void GetAllBuildingsTestOk()
    { 
        IEnumerable<Building> buildings = new List<Building> { new Building() { Name = "Mirador" } };

        buildingRepositoryMock.Setup(x => x.GetAllBuildings()).Returns(buildings);

        IEnumerable<Building> result = buildingLogic.GetAllBuildings();

        buildingRepositoryMock.VerifyAll();

        Assert.AreEqual(buildings, result);

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
    public void GetFlatByBuildingAndFlatIdTestOk()
    {
        Flat flat = new Flat();

        buildingRepositoryMock.Setup(x => x.GetFlatByBuildingAndFlatId(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(flat);

        Flat result = buildingLogic.GetFlatByBuildingAndFlatId(It.IsAny<Guid>(), It.IsAny<Guid>());

        buildingRepositoryMock.VerifyAll();

        Assert.AreEqual(flat, result);
    }

    [TestMethod]
    public void UpdateFlatByBuildingAndFlatIdTestOk()
    {
        Flat toUpdateFlat = new Flat() {
            Floor = 3, 
            Number = 303, 
            Bathrooms = 3, 
            HasBalcony = true, 
            OwnerEmail = "pedro@mail.com", 
            OwnerName = "Pedro", 
            OwnerSurname = "De Los Naranjos" 
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
            HasBalcony = true,
            OwnerEmail = "pedro@mail.com",
            OwnerName = "Pedro",
            OwnerSurname = "De Los Naranjos"
        };

        buildingRepositoryMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Returns(new Building() { Flats = new List<Flat>() { new Flat() { Id = toUpdateFlat.Id } } });
        buildingRepositoryMock.Setup(x => x.GetFlatByBuildingAndFlatId(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(toUpdateFlat);
        buildingRepositoryMock.Setup(x => x.UpdateFlat(It.IsAny<Flat>())).Returns(expected);

        Flat result = buildingLogic.UpdateFlat(It.IsAny<Guid>(), It.IsAny<Guid>(), flat);

        buildingRepositoryMock.VerifyAll();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void UpdateFlatByBuildingAndFlatIdTestNotFoundBuilding()
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

        buildingRepositoryMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Throws(new ArgumentException("Building not found"));

        try
        {
            Flat result = buildingLogic.UpdateFlat(It.IsAny<Guid>(), It.IsAny<Guid>(), flat);
        }catch(Exception e)
        {
            Assert.IsInstanceOfType(e, typeof(ArgumentException));
            Assert.AreEqual(e.Message, "Building not found");
        }
    }

    [TestMethod]
    public void UpdateFlatByBuildingAndFlatIdTestNotFoundFlat()
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

        buildingRepositoryMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Throws(new ArgumentException("Building not found"));

        try
        {
            Flat result = buildingLogic.UpdateFlat(It.IsAny<Guid>(), It.IsAny<Guid>(), flat);
        }
        catch (Exception e)
        {
            Assert.IsInstanceOfType(e, typeof(ArgumentException));
            Assert.AreEqual(e.Message, "Building not found");
        }
    }

    [TestMethod]
    public void UpdateFlatByBuildingAndFlatIdTestFlatWithSameNumberAlreadyExists()
    {
        Flat flat = new Flat() {Floor = 3, Number = 303, Bathrooms = 3, HasBalcony = true, OwnerEmail = "pedro@mail.com", OwnerName = "Pedro", OwnerSurname = "De Los Naranjos" };

        Flat toChangeFlat = new Flat();

        Flat anotherFlat = new Flat() { Id = Guid.NewGuid(), Floor = 3, Number = 303 };

        Building building = new Building() { Flats = new List<Flat>() { toChangeFlat, anotherFlat } };

        buildingRepositoryMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Returns(building);

        Exception exception = null;

        try
        {
            Flat result = buildingLogic.UpdateFlat(building.Id, toChangeFlat.Id, flat);
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
    public void UpdateFlatByBuildingAndFlatIdTestFlatWithInvalidFloorNumber()
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
            Flat result = buildingLogic.UpdateFlat(building.Id, toChangeFlat.Id, flat);
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
    public void UpdateFlatByBuildingAndFlatIdTestFlatWithEmptyOwnerName()
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
            Flat result = buildingLogic.UpdateFlat(building.Id, toChangeFlat.Id, flat);
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
    public void UpdateFlatByBuildingAndFlatIdTestFlatWithEmptyEmail()
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
            Flat result = buildingLogic.UpdateFlat(building.Id, toChangeFlat.Id, flat);
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
    public void UpdateFlatByBuildingAndFlatIdTestFlatWithEmptyOwnerSurname()
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
            Flat result = buildingLogic.UpdateFlat(building.Id, toChangeFlat.Id, flat);
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
    public void UpdateFlatByBuildingAndFlatIdTestFlatWithNegativeNumberOfBathrooms()
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
            Flat result = buildingLogic.UpdateFlat(building.Id, toChangeFlat.Id, flat);
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
    public void UpdateFlatByBuildingAndFlatIdTestOwnerEmailWithNoAt()
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
            Flat result = buildingLogic.UpdateFlat(building.Id, toChangeFlat.Id, flat);
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
    public void UpdateFlatByBuildingAndFlatIdTestOwnerEmailWithNoDot()
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
            Flat result = buildingLogic.UpdateFlat(building.Id, toChangeFlat.Id, flat);
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