using SampleCrud.Domain.Entities;
using SampleCrud.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using SampleCrud.Infra.Data.Context;
using Microsoft.Extensions.Logging;

namespace SampleCrud.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ILogger<PersonRepository> _logger;

        private readonly ApplicationContext _context;

        public PersonRepository(ILogger<PersonRepository> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void Add(Person person)
        {
            try
            {
                _logger.LogInformation($"Adding person: {person}");
                _context.Person.Add(person);
                _context.SaveChanges();
                _logger.LogInformation($"Person added: {person}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Data: {person} Error: {e}");
                throw new Exception("Error adding person!");
            }
        }

        public async Task<Person?> GetById(Guid? id)
        {
            try
            {
                _logger.LogInformation($"Getting person by id: {id}");
                return await _context.Person.FindAsync(id) ?? null;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error gettting person! Data: {id} Error: {e}");
                throw new Exception("Error gettting person!");
            }
        }

        public async Task<IEnumerable<Person>> GetPersons()
        {
            try
            {
                _logger.LogInformation("Init process getting all persons in repository");
                var result = await _context.Person.ToListAsync();
                _logger.LogInformation($"Persons found: {result.Count}, {result}");   
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error getting all persons in repository! Error: {e}");
                throw new Exception("Error getting all persons!");
            }
        }

        public void Remove(Person person)
        {
            try
            {
                _logger.LogInformation($"Removing person: {person}");
                _context.Person.Remove(person);
                _context.SaveChanges();
                _logger.LogInformation($"Person removed: {person}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in removing a person to the repository! Data: {person} Error: {e}");
                throw new Exception("Error in removing person!");
            }
        }

        public void Update(Person person)
        {
            try
            {
                _logger.LogInformation($"Updating person: {person}");
                _context.Person.Update(person);
                _context.SaveChanges();
                _logger.LogInformation($"Person updated: {person}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in updating a person to the repository! Data: {person} Error: {e}");
                throw new Exception("Error in updating person!");
            }
        }
    }
}