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
    public class CityController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public CityController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] BasicSearch search, [FromServices] IGetCitiesQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

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
            _handler.HandleCommand(commad, data);
            return Created();
        }

        [HttpGet("/api/city/country/{id}")]
        public IActionResult GetCityByCountryId(int id, [FromServices] IGetCitiesByCountryQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }

    }
}
