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
    public class FeaturesController : ControllerBase
    {
        private UseCaseHandler _handler;

        public FeaturesController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<FeaturesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FeaturesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FeaturesController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] NamedDto data, [FromServices] ICreateFeaturesCommand command)
        {
            _handler.HandleCommand(command, data);

            return NoContent();
        }

        // PUT api/<FeaturesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FeaturesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
