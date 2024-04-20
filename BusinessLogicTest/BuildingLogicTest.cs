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

    [TestMethod]
    public void GetBuildingByIdTestOk()
    {
        Building building = new Building() { Name = "Mirador" };

        buildingRepositoryMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Returns(building);

        Building result = buildingLogic.GetBuildingById(It.IsAny<Guid>());

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

        buildingRepositoryMock.Setup(x => x.GetAllBuildings()).Returns(new List<Building>());

        buildingLogic.DeleteBuilding(It.IsAny<Guid>());

        IEnumerable<Building> repositoryBuildings = buildingLogic.GetAllBuildings();

        buildingRepositoryMock.VerifyAll();

        Assert.IsTrue(buildingLogic.GetAllBuildings().FirstOrDefault(x => x.Id == It.IsAny<Guid>()) is null);
    }
}