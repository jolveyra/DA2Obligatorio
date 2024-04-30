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
        public void GetAllUsersTest()
        {
            List<Person> people = new List<Person>
            {
                new User { Id = Guid.NewGuid(), Email = "juan@gmail.com", Name = "Juan" }
            };

            _contextMock.Setup(context => context.People).ReturnsDbSet(people);

            IEnumerable<Person> result = _peopleRepository.GetAllPeople();

            _contextMock.Verify(context => context.People, Times.Once());
            Assert.IsTrue(result.SequenceEqual(people));
        }

        [TestMethod]
        public void GetPersonByIdTest()
        {
            Person person = new User { Id = Guid.NewGuid(), Email = "juan@gmail.com", Name = "Juan" };

            _contextMock.Setup(context => context.People).ReturnsDbSet(new List<Person> { person });
            
            Person result = _peopleRepository.GetPersonById(person.Id);

            _contextMock.Verify(context => context.People, Times.Once());
            Assert.AreEqual(person, result);
        }

        [TestMethod]
        public void GetNonExistingPersonByIdTest()
        {
            Guid id = Guid.NewGuid();
            _contextMock.Setup(context => context.People).ReturnsDbSet(new List<Person>());

            Exception exception = null;

            try
            {
                _peopleRepository.GetPersonById(id);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
            Assert.AreEqual("Person not found", exception.Message);
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
