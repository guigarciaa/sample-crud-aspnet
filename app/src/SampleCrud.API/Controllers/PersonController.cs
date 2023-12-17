using Microsoft.AspNetCore.Cors;
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

                _logger.LogInformation($"Persons found: {persons.Count()}, {persons}");
                return Ok(persons);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error gettting persons in controller! Error: {e}");
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        [EnableCors("AllowOrigin")]
        public async Task<ActionResult<Person>> Get(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest("Person id is null.");

                _logger.LogInformation($"Getting person by id: {id}");
                var person = await _personService.GetById(id);
                if (person == null)
                {
                    _logger.LogError($"Person not found: {id}");
                    return NotFound("Person not found.");
                }

                _logger.LogInformation($"Person found: {person}");
                return Ok(person);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error gettting person in controller! Data: {id} Error: {e}");
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Person>> Post([FromBody] Person person)
        {
            try
            {
                if (person == null)
                    return BadRequest("Person is null.");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState.ValidationState);

                _logger.LogInformation($"Adding person: {person}");
                await _personService.Add(person);
                _logger.LogInformation($"Person added: {person}");
                return CreatedAtAction(nameof(Get), new { id = person.Id }, person);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error adding person! Data: {person} {e}");
                person.Id = Guid.Empty;
                return BadRequest($"Error adding person! Data: {person}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Person person)
        {
            try
            {
                if (person == null)
                    return BadRequest("Person is null.");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState.ValidationState);

                var personToUpdate = _personService.GetById(person.Id);
                if (personToUpdate == null)
                   return NotFound("Person not found.");

                _logger.LogInformation($"Updating person: {person}");   
                return Ok("Person updated successfully.");
            }
            catch (Exception e)
            {
                _logger.LogError($"Error updating person in controller! Data: {person} Error: {e}");    
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

                _logger.LogInformation($"Removing person by id: {id}");
                return Ok("Person deleted successfully.");
            }
            catch (Exception e)
            {
                _logger.LogError($"Error removing person in controller! Data: {id} Error: {e}");
                return BadRequest(e.Message);
            }
        }
    }
}