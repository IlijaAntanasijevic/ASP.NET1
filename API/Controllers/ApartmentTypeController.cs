using Application.DTO;
using Application.DTO.Search;
using Application.UseCases.Commands.Lookup;
using Application.UseCases.Queries.ApartmentType;
using Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentTypeController : ControllerBase
    {
        // GET: api/<ApartmentTypeController>
        private readonly UseCaseHandler _handler;

        public ApartmentTypeController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] BasicSearch search, [FromServices] IGetApartmentTypesQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        // GET api/<ApartmentTypeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ApartmentTypeController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] NamedDto data, [FromServices] ICreateApartmentTypeCommand command)
        {
            _handler.HandleCommand(command, data);
            return Created();
        }

        // PUT api/<ApartmentTypeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ApartmentTypeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
