using Application.DTO.Ratings;
using Application.UseCases.Queries.Apartment;
using Application.UseCases.Queries.Home;
using Implementation.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public HomeController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        [HttpGet("stats")]
        public IActionResult GetStats([FromServices] IGetHomeStatsQuery query)
        {
            return Ok(_handler.HandleQuery(query, 0));
        }

        [HttpGet("testimonials")]
        public IActionResult GetTestimonials([FromServices] IGetHomeTestimonialsQuery query)
        {
            return Ok(_handler.HandleQuery(query, 0));
        }

    }
}
