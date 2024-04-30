using Domain;

namespace RepositoryInterfaces
{
    public interface IPeopleRepository
    {
        public IEnumerable<Person> GetPeople();
        public Person GetPersonById(Guid id);
        public Person CreatePerson(Person owner);
        public Person UpdatePerson(Person owner);
    }
}
