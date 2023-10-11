using Microsoft.AspNetCore.Mvc;
using SampleCrud.Domain.Entities;
using SampleCrud.Domain.Services;

namespace SampleCrud.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonService _personService;
        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> Get()
        {
            try
            {
                var persons = await _personService.GetPersons();

                if (persons.Count() <= 0)
                    return NotFound();

                return Ok(persons);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get(Guid id)
        {
            try
            {
                var person = await _personService.GetById(id);

                if (person == null)
                    return NotFound();

                return Ok(person);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<Person> Post([FromBody] Person person)
        {
            _personService.Add(person);
            return Ok(person);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Person person)
        {
            try
            {
                var personToUpdate = _personService.GetById(person.Id);
                if (personToUpdate == null)
                    NotFound("Person not found.");

                return Ok("Person updated successfully.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var personToDelete = _personService.GetById(id);
                if (personToDelete == null)
                    NotFound("Person not found.");

                return Ok("Person deleted successfully.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}