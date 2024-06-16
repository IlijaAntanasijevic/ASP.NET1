using Application.DTO.Apartments;
using Application.UseCases.Commands.Apartments;
using Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public ApartmentController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<ApartmentController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ApartmentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ApartmentController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CreateApartmentDto data, ICreateApartmentCommand command)
        {
            _handler.HandleCommand(command, data);
            return Created();
        }

        // PUT api/<ApartmentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ApartmentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
