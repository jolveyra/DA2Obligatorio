using BusinessLogic;
using RepositoryInterfaces;
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
    [ExpectedException(typeof(ArgumentException), "Building with same name already exists")]
    public void CreateBuildingTestBuildingWithAlreadyExistingName()
    {
        Building building = new Building() { Name = "Mirador" };
        Building expected = new Building();
        buildingRepositoryMock.Setup(x => x.GetAllBuildings()).Returns(new List<Building> { new Building() { Name = "Mirador" } });

        Building result = buildingLogic.CreateBuilding(building);

        buildingRepositoryMock.VerifyAll();
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
}