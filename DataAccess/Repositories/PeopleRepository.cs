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
    }
}