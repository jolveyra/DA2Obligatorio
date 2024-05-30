using BusinessLogic;
using RepositoryInterfaces;
using CustomExceptions;
using Domain;
using Moq;
using System;

namespace BusinessLogicTest;

[TestClass]
public class BuildingLogicTest
{

    private Mock<IBuildingRepository> buildingRepositoryMock;
    private Mock<IUserRepository> userRepositoryMock;
    private Mock<IPeopleRepository> peopleRepositoryMock;
    private BuildingLogic buildingLogic;

    [TestInitialize]
    public void Initialize()
    {
        buildingRepositoryMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
        userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
        peopleRepositoryMock = new Mock<IPeopleRepository>(MockBehavior.Strict);
        buildingLogic = new BuildingLogic(buildingRepositoryMock.Object, userRepositoryMock.Object, peopleRepositoryMock.Object);
    }

    [TestMethod]
    public void CreateBuildingTestOk()
    {
        Building building = new Building() { Id = Guid.NewGuid(), Name = "Mirador",
            ConstructorCompany = new ConstructorCompany { Id = Guid.NewGuid(), Name = "A Company" },
            SharedExpenses = 100,
            Address = new Address()
            {
                Street = "Street", 
                DoorNumber = 12, 
                CornerStreet = "Another Street",
                Latitude = 80,
                Longitude = -80,
            }
        };
        Building expected = new Building() { Id = building.Id, 
            Name = "Mirador",
            ConstructorCompany = new ConstructorCompany { Id = Guid.NewGuid(), Name = "A Company" },
            Address = building.Address,
            SharedExpenses = 100
        };

        buildingRepositoryMock.Setup(x => x.GetAllBuildings()).Returns(new List<Building>());
        buildingRepositoryMock.Setup(x => x.CreateBuilding(building)).Returns(expected);
        buildingRepositoryMock.Setup(x => x.CreateFlat(It.IsAny<Flat>())).Returns(new Flat() { Building = building });
        userRepositoryMock.Setup(x => x.GetUserById(It.IsAny<Guid>())).Returns(new User() { Role = Role.Manager });

        Building result = buildingLogic.CreateBuilding(building, amountOfFlats: 1, It.IsAny<Guid>());

        buildingRepositoryMock.VerifyAll();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void CreateBuildingTestBuildingWithEmptyName()
    {
        Building building = new Building() { Id = Guid.NewGuid(), Name = "",
            ConstructorCompany = new ConstructorCompany { Id = Guid.NewGuid(), Name = "A Company" },
            SharedExpenses = 100,
            Address = new Address()
            {
                Street = "Street", 
                DoorNumber = 12, 
                CornerStreet = "Another Street",
                Latitude = 80,
                Longitude = -80,
            }
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
        Assert.AreEqual(exception.Message, "Building name cannot be empty");
    }

    [TestMethod]
    public void CreateBuildingTestBuildingWithNegativeSharedExpenses()
    {
        Building building = new Building() { Id = Guid.NewGuid(), Name = "Mirador",
            ConstructorCompany = new ConstructorCompany { Id = Guid.NewGuid(), Name = "A Company" },
            SharedExpenses = -100,
            Address = new Address()
            {
                Street = "Street", 
                DoorNumber = 12, 
                CornerStreet = "Another Street",
                Latitude = 80,
                Longitude = -80,
            }
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
        Assert.AreEqual(exception.Message, "Shared expenses cannot be negative");
    }

    [TestMethod]
    public void CreateBuildingTestBuildingWithAlreadyExistingName()
    {
        Building building = new Building() { Id = Guid.NewGuid(), Name = "Mirador",
            ConstructorCompany = new ConstructorCompany { Id = Guid.NewGuid(), Name = "A Company" },
            SharedExpenses = 100,
            Address = new Address()
            {
                Street = "Street", 
                DoorNumber = 12, 
                CornerStreet = "Another Street",
                Latitude = 80,
                Longitude = -80,
            }
        };
        buildingRepositoryMock.Setup(x => x.GetAllBuildings()).Returns(new List<Building> { new Building() { Id = Guid.NewGuid(), Name = "Mirador",
            ConstructorCompany = new ConstructorCompany { Id = Guid.NewGuid(), Name = "A Company" },
            SharedExpenses = 100,
            Address = new Address()
            {
                Street = "Street", 
                DoorNumber = 13, 
                CornerStreet = "Another Street",
                Latitude = 80,
                Longitude = -80,
            }
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
        Assert.AreEqual("Building with same name already exists", exception.Message);
    }

    [TestMethod]
    public void CreateBuildingTestBuildingWithEmptyStreetName()
    {
        Building building = new Building() { Address = new Address() {Street = ""},
            ConstructorCompany = new ConstructorCompany { Id = Guid.NewGuid(), Name = "A Company" },
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
        Assert.AreEqual(exception.Message, "Building's street cannot be empty");
    }

    [TestMethod]
    public void CreateBuildingTestBuildingWithEmptyConstructorCompany()
    {
        Building building = new Building() { ConstructorCompany = new ConstructorCompany(),
            Name = "A Name",
            Address = new Address() {
            Street = "Street", 
            DoorNumber = 12, 
            CornerStreet = "Another Street",
            },
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
    public void CreateBuildingTestBuildingWithConstructorCompanyWithMoreThan100Characters()
    {
        Building building = new Building()
        {
            ConstructorCompany = new ConstructorCompany { Id = Guid.NewGuid(), Name = "12345678911234567891123456789112345678911234567891123456789112345678911234567891123456789112345678911" },
            Name = "A Name",
            Address = new Address()
            {
                Street = "Street",
                DoorNumber = 12,
                CornerStreet = "Another Street",
            },
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
        Assert.AreEqual(exception.Message, "Building constructor company cannot be longer than 100 characters");
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
            ConstructorCompany = new ConstructorCompany { Id = Guid.NewGuid(), Name = "A Company" },
            Address = new Address()
            {
                Street = "Street",
                DoorNumber = 12,
                CornerStreet = "Another Street",
            },
            SharedExpenses = 100
        };

        buildingRepositoryMock.Setup(x => x.GetAllBuildings()).Returns(new List<Building> { new Building() { Name = "Mirador",
            ConstructorCompany = new ConstructorCompany { Id = Guid.NewGuid(), Name = "A Company" },
            Address = new Address()
            {
                Street = "Street",
                DoorNumber = 12,
                CornerStreet = "Another Street",
            },
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
            ConstructorCompany = new ConstructorCompany { Id = Guid.NewGuid(), Name = "A Company" },
            Address = new Address() {
            Street = "Street",
            DoorNumber = 12,
            CornerStreet = "Another Street",
            Latitude = 32,
            Longitude = 34,
            },
            SharedExpenses = 100
        };

        buildingRepositoryMock.Setup(x => x.GetAllBuildings()).Returns(new List<Building> { new Building() { Name = "Mirador",
            ConstructorCompany = new ConstructorCompany { Id = Guid.NewGuid(), Name = "A Company" },
            Address = new Address()
            {
            Street = "Street",
            DoorNumber = 132,
            CornerStreet = "Another",
            Latitude = 32,
            Longitude = 34,
            },
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
            ConstructorCompany = new ConstructorCompany { Id = Guid.NewGuid(), Name = "A Company" },
            Address = new Address()
            {
                Street = "Street",
                DoorNumber = 12,
                CornerStreet = "Another Street",
                Latitude = 91,
                Longitude = 34,
            },
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
            ConstructorCompany = new ConstructorCompany { Id = Guid.NewGuid(), Name = "A Company" },
            Address = new Address()
            {
            Street = "Street",
            DoorNumber = 12,
            CornerStreet = "Another Street",
            Latitude = -91,
            Longitude = 34,
            },
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
            ConstructorCompany = new ConstructorCompany { Id = Guid.NewGuid(), Name = "A Company" },
            Address = new Address()
            {
                Street = "Street",
                DoorNumber = 12,
                CornerStreet = "Another Street",
                Latitude = 90,
                Longitude = -181,
            },
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
            ConstructorCompany = new ConstructorCompany { Id = Guid.NewGuid(), Name = "A Company" },
            Address = new Address()
            {
            Street = "Street",
            DoorNumber = 12,
            CornerStreet = "Another Street",
            Latitude = 90,
            Longitude = 181,
            },
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
        IEnumerable<Building> buildings = new List<Building> { new Building() { Name = "Mirador", Manager = user } };

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

        ConstructorCompany constructorCompany = new ConstructorCompany { Id = Guid.NewGuid(), Name = "Saciim" };

        Building toUpdateBuilding = new Building() { SharedExpenses = 200, ConstructorCompany = constructorCompany };
        Building expected = new Building() { Name = "Mirador", SharedExpenses = 300, ConstructorCompany = constructorCompany };
        
        List<Guid> guids = new List<Guid> { Guid.NewGuid() };

        userRepositoryMock.Setup(x => x.GetUserById(It.IsAny<Guid>())).Returns(new User() { Id = guids.First(), Role = Role.MaintenanceEmployee });
        buildingRepositoryMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Returns(building);
        buildingRepositoryMock.Setup(x => x.UpdateBuilding(It.IsAny<Building>())).Returns(expected);

        Building result = buildingLogic.UpdateBuilding(It.IsAny<Guid>(), toUpdateBuilding);

        buildingRepositoryMock.VerifyAll();

        Assert.AreEqual(expected, result);
        Assert.AreEqual(expected.SharedExpenses, result.SharedExpenses);
        Assert.AreEqual(expected.ConstructorCompany, expected.ConstructorCompany);
        CollectionAssert.AreEqual(expected.MaintenanceEmployees.ToList(), result.MaintenanceEmployees.ToList());
    }

    [TestMethod]
    public void UpdateBuildingTestNotAMaintenanceEmployeeInList()
    {
        Building building = new Building() { Name = "Mirador", SharedExpenses = 200 };
        ConstructorCompany constructorCompany = new ConstructorCompany { Id = Guid.NewGuid(), Name = "Saciim" };
        Building toUpdateBuilding = new Building() { SharedExpenses = 200, ConstructorCompany = constructorCompany, MaintenanceEmployees = new List<Guid>() { Guid.NewGuid() } };
        Building expected = new Building() { Name = "Mirador", SharedExpenses = 300, ConstructorCompany = constructorCompany };

        userRepositoryMock.Setup(x => x.GetUserById(It.IsAny<Guid>())).Returns(new User() { Id = toUpdateBuilding.MaintenanceEmployees.First(), Role = Role.Administrator });
        buildingRepositoryMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Returns(building);

        try
        {

            Building result = buildingLogic.UpdateBuilding(It.IsAny<Guid>(), toUpdateBuilding);
        }catch(Exception e)
        {
            buildingRepositoryMock.VerifyAll();

            Assert.IsInstanceOfType(e, typeof(BuildingException));
            Assert.AreEqual(e.Message, "User in maintenance employees list is not a maintenance employee");
        }
    }


    [TestMethod]
    public void UpdateBuildingTestNegativeSharedExpenses()
    {

        Building building = new Building();
        Building toUpdateData = new Building() { SharedExpenses = -1 };

        Exception exception = null;

        try
        {
            Building result = buildingLogic.UpdateBuilding(building.Id, toUpdateData);
        }
        catch (Exception e)
        {
            exception = e;
        }

        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Shared expenses cannot be negative");
    }

    [TestMethod]
    public void UpdateBuildingTestRepeatedIdInList()
    {
        Guid id = Guid.NewGuid();
        Building building = new Building() { SharedExpenses = 200, ConstructorCompany = new ConstructorCompany { Id = Guid.NewGuid(), Name = "Saciim" }, MaintenanceEmployees = new List<Guid>() { id, id }};
        
        try
        {
            Building result = buildingLogic.UpdateBuilding(It.IsAny<Guid>(), building);
        }
        catch (Exception e)
        {
            buildingRepositoryMock.VerifyAll();

            Assert.IsInstanceOfType(e, typeof(BuildingException));
            Assert.AreEqual(e.Message, "Maintenance employees list contains repeated ids");
        }

    }

    [TestMethod]
    public void DeleteBuildingNonExistingTest()
    {
        
        userRepositoryMock.Setup(x => x.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>())).Returns(new ConstructorCompanyAdministrator() { Role = Role.ConstructorCompanyAdmin, Id = Guid.NewGuid() });

        buildingRepositoryMock.Setup(x => x.GetAllBuildings()).Returns(new List<Building>() { });

        try
        {
            buildingLogic.DeleteConstructorCompanyBuilding(It.IsAny<Guid>(), It.IsAny<Guid>());
        }
        catch(Exception e)
        {
            buildingRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(e, typeof(DeleteException));

        }
    }

    [TestMethod]
    public void GetFlatByBuildingAndFlatIdTestOk()
    {
        Flat flat = new Flat();

        buildingRepositoryMock.Setup(x => x.GetAllBuildingFlats(It.IsAny<Guid>())).Returns(new List<Flat>() { flat });

        Flat result = buildingLogic.GetFlatByBuildingAndFlatId(It.IsAny<Guid>(), It.IsAny<Guid>());

        buildingRepositoryMock.VerifyAll();

        Assert.AreEqual(flat, result);
    }


    [TestMethod]
    public void GetFlatByBuildingAndFlatIdTestFlatNotFoundInBuilding()
    {
        Flat flat = new Flat();

        buildingRepositoryMock.Setup(x => x.GetAllBuildingFlats(It.IsAny<Guid>())).Returns(new List<Flat>() { });

        try
        {

            Flat result = buildingLogic.GetFlatByBuildingAndFlatId(It.IsAny<Guid>(), It.IsAny<Guid>());
        }catch(Exception e)
        {
            buildingRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(e, typeof(BuildingException));
            Assert.AreEqual(e.Message, "Flat not found in building");
        }
    }

    [TestMethod]
    public void UpdateFlatByFlatIdTestOk()
    {
        Flat toUpdateFlat = new Flat() {
            Floor = 3, 
            Number = 303, 
            Bathrooms = 3, 
            Rooms = 1,
            HasBalcony = true, 
            Owner = new Person()
            {
                Email = "pedro@mail.com", 
                Name = "Pedro", 
                Surname = "De Los Naranjos",
            },
            Building = new Building()
        };
        Flat flat = new Flat() { 
            Floor = 3, 
            Number = 304,
            Bathrooms = 3,
            Rooms = 1,
            HasBalcony = true,
            Owner = new Person()
            {
                Email = "pedro@mail.com", 
                Name = "Pedro", 
                Surname = "De Los Naranjos",
            },
        };
        Flat expected = new Flat() { 
            Floor = 3, 
            Number = 304,
            Bathrooms = 3,
            Rooms = 1,
            HasBalcony = true,
            Owner = new Person()
            {
                Email = "pedro@mail.com", 
                Name = "Pedro", 
                Surname = "De Los Naranjos",
            },
        };

        buildingRepositoryMock.Setup(x => x.GetAllBuildingFlats(It.IsAny<Guid>())).Returns(new List<Flat>() { toUpdateFlat });
        buildingRepositoryMock.Setup(x => x.UpdateFlat(It.IsAny<Flat>())).Returns(expected);
        peopleRepositoryMock.Setup(repo => repo.GetPeople()).Returns(new List<Person>());
        peopleRepositoryMock.Setup(repo => repo.UpdatePerson(It.IsAny<Person>())).Returns(expected.Owner);

        Flat result = buildingLogic.UpdateFlat(It.IsAny<Guid>(), It.IsAny<Guid>(), flat, false);

        buildingRepositoryMock.VerifyAll();

        Assert.AreEqual(expected, result);
    }



    [TestMethod]
    public void UpdateFlatByFlatIdTestFlatNotFoundInBuilding()
    {
        Flat toUpdateFlat = new Flat()
        {
            Floor = 3,
            Number = 303,
            Bathrooms = 3,
            Rooms = 1,
            HasBalcony = true,
            Owner = new Person()
            {
                Email = "pedro@mail.com",
                Name = "Pedro",
                Surname = "De Los Naranjos",
            },
            Building = new Building()
        };
        Flat flat = new Flat()
        {
            Floor = 3,
            Number = 304,
            Bathrooms = 3,
            Rooms = 1,
            HasBalcony = true,
            Owner = new Person()
            {
                Email = "pedro@mail.com",
                Name = "Pedro",
                Surname = "De Los Naranjos",
            },
        };
        Flat expected = new Flat()
        {
            Floor = 3,
            Number = 304,
            Bathrooms = 3,
            Rooms = 1,
            HasBalcony = true,
            Owner = new Person()
            {
                Email = "pedro@mail.com",
                Name = "Pedro",
                Surname = "De Los Naranjos",
            },
        };

        buildingRepositoryMock.Setup(x => x.GetAllBuildingFlats(It.IsAny<Guid>())).Returns(new List<Flat>() {  });
        peopleRepositoryMock.Setup(repo => repo.GetPeople()).Returns(new List<Person>());
        peopleRepositoryMock.Setup(repo => repo.UpdatePerson(It.IsAny<Person>())).Returns(expected.Owner);

        try
        {
            Flat result = buildingLogic.UpdateFlat(It.IsAny<Guid>(), It.IsAny<Guid>(), flat, false);
        }catch(Exception e)
        {
            buildingRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(e, typeof(BuildingException));
            Assert.AreEqual(e.Message, "Flat not found in building");
        }
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
    public void UpdateFlatTestFlatWithSameNumberAlreadyExists()
    {
        Flat flat = new Flat() {Floor = 3, Number = 303, Bathrooms = 3, HasBalcony = true, 
            Owner = new Person()
            {
                Email = "pedro@mail.com", 
                Name = "Pedro", 
                Surname = "De Los Naranjos",
            }, Building = new Building(),
            Rooms = 1 };

        Flat toChangeFlat = new Flat() { Building = flat.Building };

        Flat anotherFlat = new Flat() { Id = Guid.NewGuid(), Floor = 3, Number = 303, Building = flat.Building };

        buildingRepositoryMock.Setup(x => x.GetAllBuildingFlats(It.IsAny<Guid>())).Returns(new List<Flat>() { anotherFlat, toChangeFlat } );
        peopleRepositoryMock.Setup(repo => repo.GetPeople()).Returns(new List<Person>());
        peopleRepositoryMock.Setup(repo => repo.UpdatePerson(It.IsAny<Person>())).Returns(flat.Owner);

        Exception exception = null;

        try
        {
            Flat result = buildingLogic.UpdateFlat(toChangeFlat.Building.Id, toChangeFlat.Id, flat, false);
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
            Rooms = 1,
            HasBalcony = true,
            Owner = new Person()
            {
                Email = "pedro@mail.com", 
                Name = "Pedro", 
                Surname = "De Los Naranjos",
            },
            Building = new Building()
        };

        Flat toChangeFlat = new Flat();

        Exception exception = null;

        try
        {
            Flat result = buildingLogic.UpdateFlat(flat.Building.Id, toChangeFlat.Id, flat, false);
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
    public void UpdateFlatByFlatIdTestFlatFromAnotherBuilding()
    {
        Flat flat = new Flat()
        {
            Number = 303,
            Floor = 3,
            Bathrooms = 3,
            Rooms = 1,
            HasBalcony = true,
            Owner = new Person()
            {
                Email = "pedro@mail.com",
                Name = "",
                Surname = "De Los Naranjos",
            },
            Building = new Building() { Id = Guid.NewGuid() }
        };

        Flat toChangeFlat = new Flat();

        Exception exception = null;

        try
        {
            Flat result = buildingLogic.UpdateFlat(Guid.NewGuid(), toChangeFlat.Id, flat, false);
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
    public void UpdateFlatByFlatIdTestFlatWithEmptyOwnerName()
    {
        Flat flat = new Flat() { Number = 303, 
            Floor = 3, 
            Bathrooms = 3,
            Rooms = 1,
            HasBalcony = true,
            Owner = new Person()
            {
                Email = "pedro@mail.com", 
                Name = "", 
                Surname = "De Los Naranjos",
            },
            Building = new Building() { Id = Guid.NewGuid() }
        };

        Flat toChangeFlat = new Flat();

        Exception exception = null;

        try
        {
            Flat result = buildingLogic.UpdateFlat(flat.Building.Id, toChangeFlat.Id, flat, false);
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
            Id = Guid.NewGuid(),
            Number = 303,
            Building = new Building() { Id = Guid.NewGuid() },
            Floor = 3,
            Bathrooms = 3,
            Rooms = 1,
            HasBalcony = true,
            Owner = new Person()
            {
                Email = "", 
                Name = "Pedro", 
                Surname = "De Los Naranjos",
            },
        };

        Flat toChangeFlat = new Flat()
        {
            Id = flat.Id,
            Building = flat.Building,
            Number = 303,
            Floor = 3,
            Bathrooms = 3,
            HasBalcony = true,
            Owner = new Person()
            {
                Email = "juan@gmail.com",
                Name = "Pedro",
                Surname = "De Los Naranjos",
            },
        };

        Exception exception = null;

        try
        {
            buildingLogic.UpdateFlat(toChangeFlat.Building.Id, toChangeFlat.Id, flat, false);
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
            Bathrooms = 3,
            Rooms = 1,
            HasBalcony = true,
            Owner = new Person()
            {
                Email = "pedro@mail.com", 
                Name = "Pedro", 
                Surname = "",
            },
            Building = new Building() { Id = Guid.NewGuid() }
        };

        Flat toChangeFlat = new Flat();

        Exception exception = null;

        try
        {
            Flat result = buildingLogic.UpdateFlat(flat.Building.Id, toChangeFlat.Id, flat, false);
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
            Bathrooms = 0,
            HasBalcony = true,
            Owner = new Person()
            {
                Email = "pedro@mail.com", 
                Name = "Pedro", 
                Surname = "De Los Naranjos",
            },
            Building = new Building() { Id = Guid.NewGuid() }
        };

        Flat toChangeFlat = new Flat();

        Exception exception = null;

        try
        {
            Flat result = buildingLogic.UpdateFlat(flat.Building.Id, toChangeFlat.Id, flat, false);
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
    public void UpdateFlatByFlatIdTestFlatWithNegativeNumberOfRooms()
    {
        Flat flat = new Flat()
        {
            Number = 303,
            Floor = 3,
            Bathrooms = 1,
            Rooms = -1,
            HasBalcony = true,
            Owner = new Person()
            {
                Email = "pedro@mail.com", 
                Name = "Pedro", 
                Surname = "De Los Naranjos",
            },
            Building = new Building() { Id = Guid.NewGuid() }
        };

        Flat toChangeFlat = new Flat();

        Exception exception = null;

        try
        {
            Flat result = buildingLogic.UpdateFlat(flat.Building.Id, toChangeFlat.Id, flat, false);
        }
        catch (Exception e)
        {
            exception = e;
        }

        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Number of rooms cannot be negative or zero");
    }


    [TestMethod]
    public void UpdateFlatByFlatIdTestOwnerEmailWithNoAt()
    {
        Flat flat = new Flat()
        {
            Number = 303,
            Floor = 3,
            Bathrooms = 1,
            Rooms = 1,
            HasBalcony = true,
            Owner = new Person()
            {
                Email = "pedromail.com", 
                Name = "Pedro", 
                Surname = "De Los Naranjos",
            },
            Building = new Building() { Id = Guid.NewGuid() }
        };

        Flat toChangeFlat = new Flat();

        Exception exception = null;

        try
        {
            Flat result = buildingLogic.UpdateFlat(flat.Building.Id, toChangeFlat.Id, flat, false);
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
            Bathrooms = 1,
            Rooms = 1,
            HasBalcony = true,
            Owner = new Person()
            {
                Email = "pedro@mailcom", 
                Name = "Pedro", 
                Surname = "De Los Naranjos",
            },
            Building = new Building() { Id = Guid.NewGuid() }
        };

        Flat toChangeFlat = new Flat();

        Exception exception = null;

        try
        {
            Flat result = buildingLogic.UpdateFlat(flat.Building.Id, toChangeFlat.Id, flat, false);
        }
        catch (Exception e)
        {
            exception = e;
        }

        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Invalid email, must contain .");
    }

    [TestMethod]
    public void UpdateFlatWithNonExistingNewOwnerByFlatIdTest()
    {
        Guid flatId = Guid.NewGuid();
        Flat flat = new Flat()
        {
            Id = flatId,
            Number = 303,
            Floor = 3,
            Bathrooms = 1,
            Rooms = 1,
            HasBalcony = true,
            Owner = new Person()
            {
                Name = "Pedro",
                Surname = "De Los Naranjos",
                Email = "pedro@gmail.com"
            },
            Building = new Building() { Id = Guid.NewGuid() }
        };
        Flat flatToUpdate = new Flat()
        {
            Id = flatId,
            Number = 303,
            Floor = 3,
            Bathrooms = 1,
            Rooms = 1,
            HasBalcony = true,
            Owner = new Person()
            {
                Id = Guid.NewGuid(),
                Name = "Juan",
                Surname = "De Los Naranjos",
                Email = "juan@gmail.com"
            },
            Building = new Building() { Id = flat.Building.Id }
        };

        peopleRepositoryMock.Setup(x => x.GetPeople()).Returns(new List<Person>() { flatToUpdate.Owner });
        peopleRepositoryMock.Setup(x => x.CreatePerson(It.IsAny<Person>())).Returns(new Person() { Id = Guid.NewGuid(), Name = "Pedro", Surname = "De Los Naranjos", Email = "pedro@gmail.com" });
        buildingRepositoryMock.Setup(x => x.GetAllBuildingFlats(It.IsAny<Guid>())).Returns(new List<Flat>() { flatToUpdate });
        buildingRepositoryMock.Setup(x => x.UpdateFlat(It.IsAny<Flat>())).Returns(flat);

        Flat result = buildingLogic.UpdateFlat(flatToUpdate.Building.Id, flatToUpdate.Id, flat, true);

        buildingRepositoryMock.VerifyAll();
        peopleRepositoryMock.VerifyAll();
        Assert.IsTrue(result.Equals(flatToUpdate) && result.Owner.Name.Equals("Pedro"));
    }

    [TestMethod]
    public void UpdateFlatWithExistingNewOwnerByFlatIdTest()
    {
        Guid flatId = Guid.NewGuid();
        Flat flat = new Flat()
        {
            Id = flatId,
            Number = 303,
            Floor = 3,
            Bathrooms = 1,
            Rooms = 1,
            HasBalcony = true,
            Owner = new Person()
            {
                Name = "Pedro",
                Surname = "De Los Naranjos",
                Email = "pedro@gmail.com"
            },
            Building = new Building() { Id = Guid.NewGuid() }
        };
        Flat flatToUpdate = new Flat()
        {
            Id = flatId,
            Number = 303,
            Floor = 3,
            Bathrooms = 1,
            Rooms = 1,
            HasBalcony = true,
            Owner = new Person()
            {
                Id = Guid.NewGuid(),
                Name = "Juan",
                Surname = "De Los Naranjos",
                Email = "juan@gmail.com"
            },
            Building = new Building() { Id = flat.Building.Id }
        };

        peopleRepositoryMock.Setup(x => x.GetPeople()).Returns(new List<Person>() { flat.Owner }); 
        buildingRepositoryMock.Setup(x => x.GetAllBuildingFlats(It.IsAny<Guid>())).Returns(new List<Flat>() { flatToUpdate });
        buildingRepositoryMock.Setup(x => x.UpdateFlat(It.IsAny<Flat>())).Returns(flat);

        Flat result = buildingLogic.UpdateFlat(flat.Building.Id, flatToUpdate.Id, flat, true);

        buildingRepositoryMock.VerifyAll();
        peopleRepositoryMock.VerifyAll();
        Assert.IsTrue(result.Equals(flatToUpdate) && result.Owner.Name.Equals("Pedro"));
    }

    [TestMethod]
    public void UpdateFlatWithNewOwnerPropertiesByFlatIdTest()
    {
        Guid flatId = Guid.NewGuid();
        Flat flat = new Flat()
        {
            Id = flatId,
            Number = 303,
            Floor = 3,
            Bathrooms = 1,
            Rooms = 1,
            HasBalcony = true,
            Owner = new Person()
            {
                Name = "Pedro",
                Surname = "De Las Manzanas",
                Email = "pedro@gmail.com"
            },
            Building = new Building() { Id = Guid.NewGuid() }
        };
        Flat flatToUpdate = new Flat()
        {
            Id = flatId,
            Number = 303,
            Floor = 3,
            Bathrooms = 1,
            Rooms = 1,
            HasBalcony = true,
            Owner = new Person()
            {
                Id = Guid.NewGuid(),
                Name = "Juan",
                Surname = "De Los Naranjos",
                Email = "juan@gmail.com"
            },
            Building = new Building() { Id = flat.Building.Id }
        };

        peopleRepositoryMock.Setup(x => x.GetPeople()).Returns(new List<Person>() { flatToUpdate.Owner });
        peopleRepositoryMock.Setup(x => x.UpdatePerson(It.IsAny<Person>())).Returns(flat.Owner);
        buildingRepositoryMock.Setup(x => x.GetAllBuildingFlats(It.IsAny<Guid>())).Returns(new List<Flat>() { flatToUpdate });
        buildingRepositoryMock.Setup(x => x.UpdateFlat(It.IsAny<Flat>())).Returns(flat);

        Flat result = buildingLogic.UpdateFlat(flat.Building.Id, flatToUpdate.Id, flat, false);

        buildingRepositoryMock.VerifyAll();
        peopleRepositoryMock.VerifyAll();
        Assert.IsTrue(result.Equals(flatToUpdate) && result.Owner.Name.Equals(flat.Owner.Name) && result.Owner.Surname.Equals(flat.Owner.Surname) && result.Owner.Email.Equals(flat.Owner.Email));
    }

    [TestMethod]
    public void UpdateFlatWithNewOwnerForTheFirstTimeByFlatIdTest()
    {
        Guid flatId = Guid.NewGuid();
        Flat flat = new Flat()
        {
            Id = flatId,
            Number = 303,
            Floor = 3,
            Bathrooms = 1,
            Rooms = 1,
            HasBalcony = true,
            Owner = new Person()
            {
                Name = "Pedro",
                Surname = "De Las Manzanas",
                Email = "pedro@gmail.com"
            },
            Building = new Building() { Id = Guid.NewGuid() }
        };
        Flat flatToUpdate = new Flat()
        {
            Id = flatId,
            Number = 303,
            Floor = 3,
            Bathrooms = 1,
            Rooms = 1,
            HasBalcony = true,
            Owner = new Person()
            {
                Id = Guid.NewGuid()
            },
            Building = new Building() { Id = flat.Building.Id }
        };

        peopleRepositoryMock.Setup(x => x.GetPeople()).Returns(new List<Person>() { flat.Owner });
        peopleRepositoryMock.Setup(x => x.DeletePerson(It.IsAny<Guid>()));
        buildingRepositoryMock.Setup(x => x.GetAllBuildingFlats(It.IsAny<Guid>())).Returns(new List<Flat>() { flatToUpdate });
        buildingRepositoryMock.Setup(x => x.UpdateFlat(It.IsAny<Flat>())).Returns(flat);

        Flat result = buildingLogic.UpdateFlat(flat.Building.Id, flatToUpdate.Id, flat, true);

        buildingRepositoryMock.VerifyAll();
        peopleRepositoryMock.VerifyAll();
        Assert.IsTrue(result.Equals(flatToUpdate) && result.Owner.Name.Equals(flat.Owner.Name) && result.Owner.Surname.Equals(flat.Owner.Surname) && result.Owner.Email.Equals(flat.Owner.Email));
    }

    [TestMethod]
    public void UpdateFlatWithNewOwnerExistingEmailByFlatIdTest()
    {
        Guid flatId = Guid.NewGuid();
        Flat flat = new Flat()
        {
            Id = flatId,
            Number = 303,
            Floor = 3,
            Bathrooms = 1,
            Rooms = 1,
            HasBalcony = true,
            Owner = new Person()
            {
                Name = "Pedro",
                Surname = "De Las Manzanas",
                Email = "pedro@gmail.com"
            },
            Building = new Building() { Id = Guid.NewGuid() }
        };
        Flat flatToUpdate = new Flat()
        {
            Id = flatId,
            Number = 303,
            Floor = 3,
            Bathrooms = 1,
            Rooms = 1,
            HasBalcony = true,
            Owner = new Person()
            {
                Id = Guid.NewGuid(),
                Name = "Juan",
                Surname = "De Los Naranjos",
                Email = "juan@gmail.com"
            },
            Building = new Building() { Id = flat.Building.Id }
        };

        peopleRepositoryMock.Setup(x => x.GetPeople()).Returns(new List<Person>() { new Person() { Email = "pedro@gmail.com", Id = Guid.NewGuid() } });
        buildingRepositoryMock.Setup(x => x.GetAllBuildingFlats(It.IsAny<Guid>())).Returns(new List<Flat>() { flatToUpdate });
        Exception exception = null;

        try
        {
            buildingLogic.UpdateFlat(flat.Building.Id, flatToUpdate.Id, flat, false);
        }
        catch (Exception e)
        {
            exception = e;
        } 

        buildingRepositoryMock.VerifyAll();
        peopleRepositoryMock.VerifyAll();
        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Another owner with same email already exists");
    }

    [TestMethod]
    public void GetAllConstructorCompanyBuildingsTestOk()
    {
        Guid constructorCompanyId = Guid.NewGuid();
        ConstructorCompany constructorCompany = new ConstructorCompany() { Id = constructorCompanyId };
        IEnumerable<Building> buildings = new List<Building> { new Building() { ConstructorCompany = constructorCompany } };

        ConstructorCompanyAdministrator user = new ConstructorCompanyAdministrator() { Id = Guid.NewGuid(), ConstructorCompany = constructorCompany };
        buildingRepositoryMock.Setup(x => x.GetAllBuildings()).Returns(buildings);
        
        userRepositoryMock.Setup(userRepositoryMock => userRepositoryMock.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>())).Returns(user);

        IEnumerable<Building> result = buildingLogic.GetAllConstructorCompanyBuildings(user.Id);

        buildingRepositoryMock.VerifyAll();

        CollectionAssert.AreEqual(buildings.ToList(), result.ToList());
    }

    [TestMethod]
    public void GetConstructorCompanyBuildingByIdTestOk()
    {
        Guid constructorCompanyId = Guid.NewGuid();
        ConstructorCompany constructorCompany = new ConstructorCompany() { Id = constructorCompanyId };
        Building building = new Building() { ConstructorCompany = constructorCompany };

        ConstructorCompanyAdministrator user = new ConstructorCompanyAdministrator() { Id = Guid.NewGuid(), ConstructorCompany = constructorCompany };
        buildingRepositoryMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Returns(building);

        userRepositoryMock.Setup(userRepositoryMock => userRepositoryMock.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>())).Returns(user);

        Building result = buildingLogic.GetConstructorCompanyBuildingById(user.Id, building.Id);

        buildingRepositoryMock.VerifyAll();

        Assert.AreEqual(building, result);
    }

    [TestMethod]
    public void GetConstructorCompanyBuildingByIdTestBuildingNotFromConstructorCompany()
    {
        Guid constructorCompanyId = Guid.NewGuid();
        ConstructorCompany constructorCompany = new ConstructorCompany() { Id = constructorCompanyId };
        Building building = new Building() { ConstructorCompany = constructorCompany };

        ConstructorCompanyAdministrator user = new ConstructorCompanyAdministrator() { Id = Guid.NewGuid(), ConstructorCompany = new ConstructorCompany() { Id = Guid.NewGuid() } };
        buildingRepositoryMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Returns(building);

        userRepositoryMock.Setup(userRepositoryMock => userRepositoryMock.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>())).Returns(user);

        Exception exception = null;

        try
        {
            Building result = buildingLogic.GetConstructorCompanyBuildingById(user.Id, building.Id);
        }
        catch (Exception e)
        {
            exception = e;
        }

        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Building does not belong to user's constructor company");

    }

    [TestMethod]
    public void CreateConstructorCompanyBuildingTestOk()
    {
        Guid constructorCompanyId = Guid.NewGuid();
        ConstructorCompany constructorCompany = new ConstructorCompany() { Id = constructorCompanyId, Name = "A Constructor Company" };

        Building building = new Building()
        {
            Id = Guid.NewGuid(),
            Name = "Mirador",
            ConstructorCompany = constructorCompany,
            SharedExpenses = 100,
            Address = new Address()
            {
                Street = "Street",
                DoorNumber = 12,
                CornerStreet = "Another Street",
                Latitude = 80,
                Longitude = -80,
            }
        };
        Building expected = new Building()
        {
            Id = building.Id,
            Name = "Mirador",
            ConstructorCompany = constructorCompany,
            Address = building.Address,
            SharedExpenses = 100
        };

        buildingRepositoryMock.Setup(x => x.GetAllBuildings()).Returns(new List<Building>());
        buildingRepositoryMock.Setup(x => x.CreateBuilding(It.IsAny<Building>())).Returns(expected);
        buildingRepositoryMock.Setup(x => x.CreateFlat(It.IsAny<Flat>())).Returns(new Flat() { Building = building });
        userRepositoryMock.Setup(x => x.GetUserById(It.IsAny<Guid>())).Returns(new User() { Role = Role.Manager });

        ConstructorCompanyAdministrator user = new ConstructorCompanyAdministrator() { Id = Guid.NewGuid(), ConstructorCompany = constructorCompany };
        buildingRepositoryMock.Setup(x => x.CreateBuilding(It.IsAny<Building>())).Returns(building);

        userRepositoryMock.Setup(userRepositoryMock => userRepositoryMock.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>())).Returns(user);

        Building result = buildingLogic.CreateConstructorCompanyBuilding(building, 1, user.Id);

        buildingRepositoryMock.VerifyAll();

        Assert.AreEqual(building, result);
    }

    [TestMethod]
    public void UpdateConstructorCompanyBuildingTestOk()
    {
        Guid constructorCompanyId = Guid.NewGuid();

        ConstructorCompany constructorCompany = new ConstructorCompany() { 
            Id = constructorCompanyId, 
            Name = "A Constructor Company" 
        };

        ConstructorCompanyAdministrator constructorCompanyAdministrator = new ConstructorCompanyAdministrator()
        {
            Id = Guid.NewGuid(),
            ConstructorCompany = constructorCompany
        };

        User newManager = new User() { Id = Guid.NewGuid(), 
            Role = Role.Manager, 
            Name = "New Manager", 
            Email = "newmanager@mail.com", 
            Password = "Password12345", 
            Surname = "Surname"
        };

        Building building = new Building()
        {
            Id = Guid.NewGuid(),
            Name = "Mirador",
            ConstructorCompany = constructorCompany,
            SharedExpenses = 100,
            Address = new Address()
            {
                Street = "Street",
                DoorNumber = 12,
                CornerStreet = "Another Street",
                Latitude = 80,
                Longitude = -80,
            },
            Manager = newManager
        };

        Building toUpdateBuilding = new Building()
        {
            Id = building.Id,
            Name = "Mirador",
            ConstructorCompany = constructorCompany,
            Address = building.Address,
            SharedExpenses = 100,
            Manager = new User() { 
                Id = Guid.NewGuid(),
                Role = Role.Manager,
                Name = "Manager2",
                Email = "manager@mail.com",
                Password = "Password12345",
                Surname = "Surname2"
            }
        };

        Building expected = new Building()
        {
            Id = building.Id,
            Name = building.Name,
            ConstructorCompany = constructorCompany,
            Address = building.Address,
            SharedExpenses = building.SharedExpenses,
            Manager = newManager
        };

        userRepositoryMock.Setup(userRepositoryMock => userRepositoryMock.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>())).Returns(constructorCompanyAdministrator);
        buildingRepositoryMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Returns(building);
        buildingRepositoryMock.Setup(x => x.UpdateBuilding(It.IsAny<Building>())).Returns(expected);
        userRepositoryMock.Setup(userRepositoryMock => userRepositoryMock.GetUserById(It.IsAny<Guid>())).Returns(newManager);

        Building result = buildingLogic.UpdateConstructorCompanyBuilding(building, toUpdateBuilding.Id, constructorCompanyAdministrator.Id);

        buildingRepositoryMock.VerifyAll();

        Assert.AreEqual(expected, result);

    }

    [TestMethod]
    public void UpdateConstructorCompanyBuildingTestBuildingNotFromConstructorCompany()
    {
        Guid constructorCompanyId = Guid.NewGuid();

        ConstructorCompany constructorCompany = new ConstructorCompany()
        {
            Id = constructorCompanyId,
            Name = "A Constructor Company"
        };

        ConstructorCompanyAdministrator constructorCompanyAdministrator = new ConstructorCompanyAdministrator()
        {
            Id = Guid.NewGuid(),
            ConstructorCompany = constructorCompany
        };

        Building building = new Building()
        {
            Id = Guid.NewGuid(),
            Name = "Mirador",
            ConstructorCompany = new ConstructorCompany() { Id = Guid.NewGuid() },
            SharedExpenses = 100,
            Address = new Address()
            {
                Street = "Street",
                DoorNumber = 12,
                CornerStreet = "Another Street",
                Latitude = 80,
                Longitude = -80,
            }
        };

        Building toUpdateBuilding = new Building()
        {
            Id = building.Id,
            Name = "Mirador",
            ConstructorCompany = constructorCompany,
            Address = building.Address,
            SharedExpenses = 100
        };

        userRepositoryMock.Setup(userRepositoryMock => userRepositoryMock.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>())).Returns(constructorCompanyAdministrator);
        buildingRepositoryMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Returns(building);

        Exception exception = null;

        try
        {
            Building result = buildingLogic.UpdateConstructorCompanyBuilding(toUpdateBuilding, toUpdateBuilding.Id, constructorCompanyAdministrator.Id);
        }
        catch (Exception e)
        {
            exception = e;
        }

        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Building does not belong to user's constructor company");

    }

    [TestMethod]
    public void UpdateConstructorCompanyBuildingTestManagerToUpdateIsNotAManager()
    {
        Guid constructorCompanyId = Guid.NewGuid();

        ConstructorCompany constructorCompany = new ConstructorCompany()
        {
            Id = constructorCompanyId,
            Name = "A Constructor Company"
        };

        ConstructorCompanyAdministrator constructorCompanyAdministrator = new ConstructorCompanyAdministrator()
        {
            Id = Guid.NewGuid(),
            ConstructorCompany = constructorCompany
        };

        User newManager = new User()
        {
            Id = Guid.NewGuid(),
            Role = Role.MaintenanceEmployee,
            Name = "New Manager",
            Email = "newmanager@mail.com",
            Password = "Password12345",
            Surname = "Surname"
        };

        Building building = new Building()
        {
            Id = Guid.NewGuid(),
            Name = "Mirador",
            ConstructorCompany = constructorCompany,
            SharedExpenses = 100,
            Address = new Address()
            {
                Street = "Street",
                DoorNumber = 12,
                CornerStreet = "Another Street",
                Latitude = 80,
                Longitude = -80,
            },
            Manager = newManager
        };

        Building toUpdateBuilding = new Building()
        {
            Id = building.Id,
            Name = "Mirador",
            ConstructorCompany = constructorCompany,
            Address = building.Address,
            SharedExpenses = 100,
            Manager = new User()
            {
                Id = Guid.NewGuid(),
                Role = Role.Manager,
                Name = "Manager2",
                Email = "manager@mail.com",
                Password = "Password12345",
                Surname = "Surname2"
            }
        };

        Building expected = new Building()
        {
            Id = building.Id,
            Name = building.Name,
            ConstructorCompany = constructorCompany,
            Address = building.Address,
            SharedExpenses = building.SharedExpenses,
            Manager = newManager
        };

        userRepositoryMock.Setup(userRepositoryMock => userRepositoryMock.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>())).Returns(constructorCompanyAdministrator);
        buildingRepositoryMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Returns(building);
        userRepositoryMock.Setup(userRepositoryMock => userRepositoryMock.GetUserById(It.IsAny<Guid>())).Returns(newManager);

        Exception exception = null;

        try
        {
            Building result = buildingLogic.UpdateConstructorCompanyBuilding(toUpdateBuilding, toUpdateBuilding.Id, constructorCompanyAdministrator.Id);
        }
        catch (Exception e)
        {
            exception = e;
        }

        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "User to update must be a manager");

    }

    [TestMethod]
    public void DeleteConstructorCompanyBuildingTestOk()
    {
        Guid constructorCompanyId = Guid.NewGuid();

        ConstructorCompany constructorCompany = new ConstructorCompany()
        {
            Id = constructorCompanyId,
            Name = "A Constructor Company"
        };

        ConstructorCompanyAdministrator constructorCompanyAdministrator = new ConstructorCompanyAdministrator()
        {
            Id = Guid.NewGuid(),
            ConstructorCompany = constructorCompany
        };

        Building building = new Building()
        {
            Id = Guid.NewGuid(),
            Name = "Mirador",
            ConstructorCompany = constructorCompany,
            SharedExpenses = 100,
            Address = new Address()
            {
                Street = "Street",
                DoorNumber = 12,
                CornerStreet = "Another Street",
                Latitude = 80,
                Longitude = -80,
            }
        };

        buildingRepositoryMock.Setup(x => x.GetAllBuildings()).Returns(new List<Building>() { building });
        buildingRepositoryMock.Setup(x => x.DeleteBuilding(It.IsAny<Building>()));
        buildingRepositoryMock.Setup(x => x.GetAllBuildingFlats(It.IsAny<Guid>())).Returns(new List<Flat>());
        buildingRepositoryMock.Setup(x => x.DeleteFlats(It.IsAny<List<Flat>>()));
        userRepositoryMock.Setup(userRepositoryMock => userRepositoryMock.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>())).Returns(constructorCompanyAdministrator);

        buildingLogic.DeleteConstructorCompanyBuilding(building.Id, constructorCompanyAdministrator.Id);

        buildingRepositoryMock.VerifyAll();
    }



    [TestMethod]
    public void DeleteConstructorCompanyBuildingWithFlatsTestOk()
    {
        Guid constructorCompanyId = Guid.NewGuid();

        ConstructorCompany constructorCompany = new ConstructorCompany()
        {
            Id = constructorCompanyId,
            Name = "A Constructor Company"
        };

        ConstructorCompanyAdministrator constructorCompanyAdministrator = new ConstructorCompanyAdministrator()
        {
            Id = Guid.NewGuid(),
            ConstructorCompany = constructorCompany
        };

        Building building = new Building()
        {
            Id = Guid.NewGuid(),
            Name = "Mirador",
            ConstructorCompany = constructorCompany,
            SharedExpenses = 100,
            Address = new Address()
            {
                Street = "Street",
                DoorNumber = 12,
                CornerStreet = "Another Street",
                Latitude = 80,
                Longitude = -80,
            }
        };

        buildingRepositoryMock.Setup(x => x.GetAllBuildings()).Returns(new List<Building>() { building });
        buildingRepositoryMock.Setup(x => x.DeleteBuilding(It.IsAny<Building>()));
        buildingRepositoryMock.Setup(x => x.GetAllBuildingFlats(It.IsAny<Guid>())).Returns(new List<Flat>() { new Flat() { Owner = new Person() { Id = Guid.NewGuid() } } });
        buildingRepositoryMock.Setup(x => x.DeleteFlats(It.IsAny<List<Flat>>()));
        peopleRepositoryMock.Setup(p => p.DeletePerson(It.IsAny<Guid>()));
        userRepositoryMock.Setup(userRepositoryMock => userRepositoryMock.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>())).Returns(constructorCompanyAdministrator);

        buildingLogic.DeleteConstructorCompanyBuilding(building.Id, constructorCompanyAdministrator.Id);

        buildingRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void DeleteConstructorCompanyBuildingTestBuildingNotFromConstructorCompany()
    {
        Guid constructorCompanyId = Guid.NewGuid();

        ConstructorCompany constructorCompany = new ConstructorCompany()
        {
            Id = constructorCompanyId,
            Name = "A Constructor Company"
        };

        ConstructorCompanyAdministrator constructorCompanyAdministrator = new ConstructorCompanyAdministrator()
        {
            Id = Guid.NewGuid(),
            ConstructorCompany = constructorCompany
        };

        Building building = new Building()
        {
            Id = Guid.NewGuid(),
            Name = "Mirador",
            ConstructorCompany = new ConstructorCompany() { Id = Guid.NewGuid() },
            SharedExpenses = 100,
            Address = new Address()
            {
                Street = "Street",
                DoorNumber = 12,
                CornerStreet = "Another Street",
                Latitude = 80,
                Longitude = -80,
            }
        };

        buildingRepositoryMock.Setup(x => x.GetAllBuildings()).Returns(new List<Building> { building });

        userRepositoryMock.Setup(x => x.GetConstructorCompanyAdministratorByUserId(It.IsAny<Guid>())).Returns(constructorCompanyAdministrator);

        Exception exception = null;

        try
        {
            buildingLogic.DeleteConstructorCompanyBuilding(building.Id, constructorCompanyAdministrator.Id);
        }
        catch (Exception e)
        {
            exception = e;
        }

        buildingRepositoryMock.VerifyAll();

        Assert.IsInstanceOfType(exception, typeof(BuildingException));
        Assert.AreEqual(exception.Message, "Building does not belong to user's constructor company");
    }
}