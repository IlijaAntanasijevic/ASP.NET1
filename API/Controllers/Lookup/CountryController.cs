using Application.DTO;
using Application.DTO.Search;
using Application.UseCases.Commands.Lookup;
using Application.UseCases.Queries.Lookup;
using Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.Lookup
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private UseCaseHandler _handler;

        public CountryController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] BasicSearch search, [FromServices] IGetCountriesQuery query)
        {
            return Ok(_handler.HandleQuery(query,search));
        }


        // POST api/<CountryController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] NamedDto data, ICreateCountryCommand command)
        {
            _handler.HandleCommand(command, data);

            return NoContent();
        }

    
    }
}
