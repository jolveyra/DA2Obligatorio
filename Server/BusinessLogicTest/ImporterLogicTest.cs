using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using CustomExceptions;
using Domain;
using LogicInterfaces;
using Moq;
using RepositoryInterfaces;


namespace BusinessLogicTest
{
    [TestClass]
    public class ImporterLogicTest
    {
        private Mock<IImporterRepository> importerRepositoryMock;
        private ImporterLogic importerLogic;

        [TestInitialize]
        public void Initialize()
        {
            importerRepositoryMock = new Mock<IImporterRepository>(MockBehavior.Strict);
            importerLogic = new ImporterLogic(importerRepositoryMock.Object);
        }

        [TestMethod]
        public void CreateImporterTestOk()
        {
            Importer importer = new Importer()
            {
                Id = Guid.NewGuid(),
                Name = "Importer 1"
            };
            importerRepositoryMock.Setup(x => x.GetAllImporters()).Returns(new List<Importer> { });
            importerRepositoryMock.Setup(x => x.CreateImporter(It.IsAny<Importer>())).Returns(importer);

            Importer result = importerLogic.CreateImporter(importer);

            importerRepositoryMock.VerifyAll();

            Assert.AreEqual(importer, result);
        }

        [TestMethod]
        public void CreateImporterImporterWithSameNameAlreadyExistsTest()
        {
            Importer importer = new Importer()
            {
                Id = Guid.NewGuid(),
                Name = "Importer 1"
            };

            Importer newImporter = new Importer()
            {
                Id = Guid.NewGuid(),
                Name = "Importer 1"
            };

            importerRepositoryMock.Setup(x => x.GetAllImporters()).Returns(new List<Importer> { importer });

            Exception exception = null;

            try
            {
                importerLogic.CreateImporter(newImporter);
            }
            catch (BusinessLogicException e)
            {
                exception = e;
            }


            importerRepositoryMock.VerifyAll();

            Assert.IsInstanceOfType(exception, typeof(ImporterException));
            Assert.AreEqual(exception.Message, "Importer with same name already exists");

        }




        //[TestMethod]
        //public void CreateCategoryTestOk()
        //{
        //    Category category = new Category()
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Category1"
        //    };
        //    Category expected = new Category()
        //    {
        //        Id = category.Id,
        //        Name = "Category1"
        //    };
        //    categoryRepositoryMock.Setup(x => x.GetAllCategories()).Returns(new List<Category> { });
        //    categoryRepositoryMock.Setup(x => x.CreateCategory(category)).Returns(expected);

        //    Category result = categoryLogic.CreateCategory(category);

        //    categoryRepositoryMock.VerifyAll();

        //    Assert.AreEqual(expected, result);
        //}
    }
}
