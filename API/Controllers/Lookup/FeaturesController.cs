using Application.DTO;
using Application.DTO.Search;
using Application.UseCases.Commands.Lookup;
using Application.UseCases.Queries.Lookup;
using Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.Lookup
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
        public IActionResult Get([FromQuery] BasicSearch search, [FromServices] IGetFeaturesQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));

        }


        // POST api/<FeaturesController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] NamedDto data, [FromServices] ICreateFeaturesCommand command)
        {
            _handler.HandleCommand(command, data);

            return NoContent();
        }
    }
}
