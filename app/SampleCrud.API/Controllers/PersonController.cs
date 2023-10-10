using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public JsonResult Get()
        {
            return new JsonResult(new { message = "Hello World" });
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            return new JsonResult(new { message = "Hello World" });
        }

        [HttpPost]
        public JsonResult Post([FromBody] Person person)
        {
            _personService.Add(person);
            return new JsonResult(new { message = person });
        }

        [HttpPut("{id}")]
        public JsonResult Put(int id, [FromBody] Person person)
        {
            _personService.Update(person);
            return new JsonResult(new { message = person });
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id, [FromBody] Person person)
        {
            _personService.Remove(person);
            return new JsonResult(new { message = "Hello World" });
        }

    }
}