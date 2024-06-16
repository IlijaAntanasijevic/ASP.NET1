using Application.DTO;
using Application.UseCases.Commands.Lookup;
using Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public CityController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<CityController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CityController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CityController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] NamedDto dto, [FromServices] ICreateCityCommand command)
        {
            _handler.HandleCommand(command, dto);

            return Created();
        }

        [HttpPost("/api/citycountry")]
        [Authorize]
        public IActionResult CityCoutry([FromBody] CityCountryDto data, [FromServices] ICreateCityCountryCommand commad)
        {
            _handler.HandleCommand(commad,data);
            return Created();
        }
     

        // PUT api/<CityController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CityController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
