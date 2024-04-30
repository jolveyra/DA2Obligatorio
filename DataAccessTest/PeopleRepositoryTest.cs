using DataAccess.Context;
using DataAccess.Repositories;
using Domain;
using Moq;
using Moq.EntityFrameworkCore;

namespace DataAccessTest
{
    [TestClass]
    public class PeopleRepositoryTest
    {
        private Mock<BuildingBossContext> _contextMock;
        private PeopleRepository _peopleRepository;

        [TestInitialize]
        public void Initialize()
        {
            _contextMock = new Mock<BuildingBossContext>();
            _peopleRepository = new PeopleRepository(_contextMock.Object);
        }

        [TestMethod]
        public void CreatePersonTest()
        {
            Person person = new User { Id = Guid.NewGuid(), Email = "juan@gmail.com", Name = "Juan" };

            _contextMock.Setup(context => context.People.Add(person));
            _contextMock.Setup(context => context.SaveChanges()).Returns(1);

            Person result = _peopleRepository.CreatePerson(person);

            _contextMock.Verify(context => context.People.Add(person), Times.Once());
            _contextMock.Verify(context => context.SaveChanges(), Times.Once());
            Assert.AreEqual(person, result);
        }
    }
}
