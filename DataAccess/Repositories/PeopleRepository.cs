using DataAccess.Context;
using Domain;
using RepositoryInterfaces;

namespace DataAccess.Repositories
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly BuildingBossContext _context;

        public PeopleRepository(BuildingBossContext context)
        {
            _context = context;
        }

        public Person CreatePerson(Person person)
        {
            _context.People.Add(person);
            _context.SaveChanges();

            return person;
        }

        public void DeletePerson(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> GetPeople()
        {
            return _context.People;
        }

        public Person GetPersonById(Guid id)
        {
            Person? person = _context.People.FirstOrDefault(u => u.Id == id);

            if (person == null)
            {
                throw new ArgumentException("Person not found");
            }

            return person;
        }

        public Person UpdatePerson(Person person)
        {
            _context.People.Update(person);
            _context.SaveChanges();

            return person;
        }
    }
}