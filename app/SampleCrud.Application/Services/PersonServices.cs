using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public Task<Person> GetById(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Person>> GetPersons()
        {
            throw new NotImplementedException();
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