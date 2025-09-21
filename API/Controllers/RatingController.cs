using Application.DTO.Ratings;
using Application.UseCases.Commands.Apartments;
using Application.UseCases.Queries.Apartment;
using Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public RatingController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] RatingSearchDto search, [FromServices] IGetRatingsQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CreateRatingDto request, [FromServices] ICreateRatingCommand command)
        {
            _handler.HandleCommand(command, request);
            return Created();
        }

    }
}
