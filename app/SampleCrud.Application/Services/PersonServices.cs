using SampleCrud.Domain.Entities;
using SampleCrud.Domain.Repositories;
using SampleCrud.Domain.Services;

namespace SampleCrud.Application.Services
{
    public class PersonServices : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        public PersonServices(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public void Add(Person person)
        {
            _personRepository.Add(person);
        }

        public async Task<Person> GetById(Guid? id)
        {
            try
            {
                return await _personRepository.GetById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Person>> GetPersons()
        {
            try
            {
                return await _personRepository.GetPersons();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Remove(Person person)
        {
            throw new NotImplementedException();
        }

        public void Update(Person person)
        {
            throw new NotImplementedException();
        }
    }
}