using SampleCrud.Domain.Entities;

namespace SampleCrud.Domain.Repositories
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetPersons();
        Task<Person?> GetById(Guid? id);

        void Add(Person person);
        void Update(Person person);
        void Remove(Person person);
    }
}