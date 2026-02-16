using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApi.API.Model;
using RestApi.API.Services;

namespace RestApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonServices _personServices;

        public PersonController(IPersonServices personServices)
        {
            _personServices = personServices;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var people = _personServices.GetAll();
            return Ok(people);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var person = _personServices.GetById(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpGet("name/{name}")]
        public IActionResult GetByName(string name)
        {
            var person = _personServices.GetByName(name);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPost]
        public IActionResult Create(Person person)
        {
            var createdPerson = _personServices.Create(person);
            return CreatedAtAction(nameof(GetById), new { id = createdPerson.Id }, createdPerson);
        }

        [HttpPut]
        public IActionResult Update(Person person)
        {
            var updatedPerson = _personServices.Update(person);
            return Ok(updatedPerson);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _personServices.Delete(id);
            return NoContent();
        }

    }
}