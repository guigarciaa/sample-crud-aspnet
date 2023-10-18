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
            if (!person.IsValid())
                throw new Exception(person.ShowErrors());

            _personRepository.Add(person);
        }

        public async Task<Person?> GetById(Guid id)
        {
            try
            {
                return await _personRepository.GetById(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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
                Console.WriteLine("Erro ao buscar pessoas");
                throw new Exception();
            }
        }

        public async void Remove(Guid id)
        {
            try
            {
                var _person = await _personRepository.GetById(id);
                if (_person != null)
                {
                    _personRepository.Remove(_person);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Person person)
        {
            try
            {
                _personRepository.Update(person);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}