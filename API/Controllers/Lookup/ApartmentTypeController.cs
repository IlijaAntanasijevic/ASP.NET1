using Application.DTO;
using Application.DTO.Search;
using Application.UseCases.Commands.Lookup;
using Application.UseCases.Queries.Lookup;
using Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.Lookup
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


        // POST api/<ApartmentTypeController>
        [HttpPost]
        public IActionResult Post([FromBody] LookupDto data, [FromServices] ICreateApartmentTypeCommand command)
        {
            _handler.HandleCommand(command, data);
            return Created();
        }

        // PUT api/<ApartmentTypeController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] LookupDto data, [FromServices] IUpdateApartmentTypeCommand command)
        {
            data.Id = id;
            _handler.HandleCommand(command, data);
            return Created();
        }

        // DELETE api/<ApartmentTypeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
