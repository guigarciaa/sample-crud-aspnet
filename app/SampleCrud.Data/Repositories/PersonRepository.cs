using SampleCrud.Domain.Entities;
using SampleCrud.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using SampleCrud.Infra.Data.Context;

namespace SampleCrud.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationContext _context;

        public PersonRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Add(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Person> GetById(Guid? id)
        {
            return await _context.Person.FirstOrDefaultAsync(m => m.Id == id) ?? throw new ArgumentNullException(nameof(id));
        }

        public async Task<IEnumerable<Person>> GetPersons()
        {
            return await _context.Person.ToListAsync();
        }

        public void Remove(Person person)
        {
            try
            {
                _context.Remove(person);
                _context.SaveChanges();
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
                _context.Update(person);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}