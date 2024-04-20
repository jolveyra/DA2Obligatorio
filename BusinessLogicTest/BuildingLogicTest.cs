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
        buildingRepositoryMock.Setup(x => x.CreateBuilding(building)).Returns(expected);
        BuildingLogic buildingLogic = new BuildingLogic(buildingRepositoryMock.Object);

        Building result = buildingLogic.CreateBuilding(building);

        buildingRepositoryMock.VerifyAll();

        Assert.AreEqual(expected, result);
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
}