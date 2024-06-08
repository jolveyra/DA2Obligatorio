using Domain;
using DataAccess.Repositories;
using DataAccess.Context;
using Moq;
using RepositoryInterfaces;
using Moq.EntityFrameworkCore;

namespace DataAccessTest
{
    [TestClass]
    public class ImporterRepositoryTest
    {
        private IImporterRepository importerRepository;
        private Mock<BuildingBossContext> mockContext;

        [TestInitialize]
        public void Initialize()
        {
            mockContext = new Mock<BuildingBossContext>();

            importerRepository = new ImporterRepository(mockContext.Object);
        }


        [TestMethod]
        public void GetAllImportersTestOk()
        {
            List<Importer> categories = new List<Importer>()
            {
                new Importer() { Id = Guid.NewGuid(), Name = "Importer 1" },
                new Importer() { Id = Guid.NewGuid(), Name = "Importer 2" }

            };

            mockContext.Setup(x => x.Importers).ReturnsDbSet(categories);

            IEnumerable<Importer> result = importerRepository.GetAllImporters();

            CollectionAssert.AreEqual(categories, result.ToList());
        }

        [TestMethod]
        public void CreateImporterTestOk()
        {
            Importer importer = new Importer()
            {
                Id = Guid.NewGuid(),
                Name = "Importer1"
            };

            mockContext.Setup(x => x.Importers.Add(importer));

            Importer result = importerRepository.CreateImporter(importer);

            mockContext.Verify(x => x.SaveChanges(), Times.Once);

            Assert.AreEqual(importer, result);
        }

        [TestMethod]
        public void GetImporterByNameTest()
        {
            Importer importer = new Importer()
            {
                Id = Guid.NewGuid(),
                Name = "Importer1"
            };

            mockContext.Setup(x => x.Importers).ReturnsDbSet(new List<Importer>() { importer });

            Importer result = importerRepository.GetImporterByName(importer.Name);

            mockContext.Verify(x => x.Importers, Times.Once);
            Assert.AreEqual(importer, result);
        }

        [TestMethod]
        public void GetNonExistingImporterByNameTest()
        {
            mockContext.Setup(x => x.Importers).ReturnsDbSet(new List<Importer>() { });
            Exception exception = null;

            try
            {
                importerRepository.GetImporterByName("Importer1");
            } catch (Exception e)
            {
                exception = e;
            }

            mockContext.Verify(x => x.Importers, Times.Once);
            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.AreEqual("Importer not found", exception.Message);
        }
    }
}
