using SampleCrud.Domain.Entities;

namespace SampleCrud.Domain.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetPersons();
        Task<Person?> GetById(Guid id);
        void Add(Person person);
        void Update(Person person);
        void Remove(Guid id);
    }
}