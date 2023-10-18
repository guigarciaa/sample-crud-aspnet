using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

                if (persons.Count() == 0)
                    return NotFound();

                return Ok(persons);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<Person>> Get(Guid id)
        {
            try
            {
                var person = await _personService.GetById(id);

                if (person == null)
                    return NotFound("Person not found.");

                return Ok(person);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<Person> Post([FromBody]Person person)
        {
            try
            {
                if (person == null)
                    return BadRequest("Person is null.");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState.ValidationState);

                _personService.Add(person);
                return CreatedAtAction(nameof(Get), new { id = person.Id }, person);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest("Person id is null.");

                var personToDelete = await _personService.GetById(id);
                if (personToDelete == null)
                    return NotFound("Person not found.");

                return Ok("Person deleted successfully.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}