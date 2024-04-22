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
        Building building = new Building();
        Building expected = new Building();
        buildingRepositoryMock.Setup(x => x.GetAllBuildings()).Returns(new List<Building>());
        buildingRepositoryMock.Setup(x => x.CreateBuilding(building)).Returns(expected);

        Building result = buildingLogic.CreateBuilding(building);

        buildingRepositoryMock.VerifyAll();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void CreateBuildingTestBuildingWithAlreadyExistingName()
    {
        Building building = new Building() { Name = "Mirador" };
        buildingRepositoryMock.Setup(x => x.GetAllBuildings()).Returns(new List<Building> { new Building() { Name = "Mirador" } });

        Exception exception = null;

        try
        {
            Building result = buildingLogic.CreateBuilding(building);
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
        Building building = new Building() { Name = "Mirador" };
        Building expected = new Building() { Name = "Mirador" };

        buildingRepositoryMock.Setup(x => x.UpdateBuilding(It.IsAny<Guid>(), It.IsAny<float>())).Returns(expected);

        Building result = buildingLogic.UpdateBuilding(It.IsAny<Guid>(), It.IsAny<float>());

        buildingRepositoryMock.VerifyAll();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void DeleteBuildingTestOk()
    {
        buildingRepositoryMock.Setup(x => x.DeleteBuilding(It.IsAny<Guid>()));

        buildingLogic.DeleteBuilding(It.IsAny<Guid>());

        buildingRepositoryMock.VerifyAll();

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
        Flat flat = new Flat();
        Flat expected = new Flat();

        buildingRepositoryMock.Setup(x => x.UpdateFlat(It.IsAny<Guid>(), It.IsAny<Guid>(), flat)).Returns(expected);

        Flat result = buildingLogic.UpdateFlat(It.IsAny<Guid>(), It.IsAny<Guid>(), flat);

        buildingRepositoryMock.VerifyAll();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void UpdateFlatByBuildingAndFlatIdTestFlatWithSameNumberAlreadyExists()
    {
        Flat flat = new Flat() { Number = 303 };

        Flat toChangeFlat = new Flat();

        Flat anotherFlat = new Flat() { Id = Guid.NewGuid(), Number = 303 };

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

}