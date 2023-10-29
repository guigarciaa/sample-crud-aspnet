using Microsoft.Extensions.Logging;
using SampleCrud.Domain.Entities;
using SampleCrud.Domain.Repositories;
using SampleCrud.Domain.Services;

namespace SampleCrud.Application.Services
{
    public class PersonServices : IPersonService
    {
        private readonly ILogger<PersonServices> _logger;
        private readonly IPersonRepository _personRepository;
        public PersonServices(ILogger<PersonServices> logger, IPersonRepository personRepository)
        {
            _logger = logger;
            _personRepository = personRepository;
        }

        public void Add(Person person)
        {
            try
            {
                _logger.LogInformation($"Adding person: {person}");
                if (!person.IsValid()) {
                    _logger.LogError($"Person is not valid: {person}, {person.ShowErrors()}");
                    throw new Exception(person.ShowErrors());
                }

                _personRepository.Add(person);
                _logger.LogInformation($"Person added: {person}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Data: {person} Error: {e}");
                throw new Exception("Error adding person!") ;
            }
        }

        public async Task<Person?> GetById(Guid id)
        {
            try
            {
                _logger.LogInformation($"Getting person by id: {id}");
                var result = await _personRepository.GetById(id);
                _logger.LogInformation($"Person found: {result}");
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error gettting person in service! Data: {id} Error: {e}");  
                throw new Exception("Error gettting a person!");
            }
        }

        public async Task<IEnumerable<Person>> GetPersons()
        {
            try
            {
                _logger.LogInformation("Init process getting all persons");
                var result = await _personRepository.GetPersons();
                _logger.LogInformation($"Persons found: {result.Count()}, {result}");
                return result ?? new List<Person>(); 
            }
            catch (Exception e)
            {
                _logger.LogError($"Error gettting persons in service! Error: {e}");
                throw new Exception("Error gettting all persons!");
            }
        }

        public async void Remove(Guid id)
        {
            try
            {
                _logger.LogInformation($"Removing person by id: {id}");
                var _person = await _personRepository.GetById(id);
                if (_person != null)
                {
                    _logger.LogInformation($"Person found: {_person}");
                    _personRepository.Remove(_person);
                    _logger.LogInformation($"Person removed: {_person}");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error removing person in service! Data: {id} Error: {e}");
                throw new Exception("Error removing a person!");
            }
        }

        public void Update(Person person)
        {
            try
            {
                _logger.LogInformation($"Updating person: {person}");
                _personRepository.Update(person);
                _logger.LogInformation($"Person updated: {person}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Error updating person in service! Data: {person} Error: {e}");   
                throw new Exception("Error updating a person!");
            }
        }
    }
}