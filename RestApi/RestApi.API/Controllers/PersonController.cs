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
        public async Task<IActionResult> GetAll()
        {
            var people = await _personServices.GetAll();
            return Ok(people);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var person = await _personServices.GetById(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var person = await _personServices.GetByName(name);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Person person)
        {
            var createdPerson = await _personServices.Create(person);
            return CreatedAtAction(nameof(GetById), new { id = createdPerson.Id }, createdPerson);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Person person)
        {
            var updatedPerson = await _personServices.Update(person);
            return Ok(updatedPerson);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _personServices.Delete(id);
            return NoContent();
        }

    }
}