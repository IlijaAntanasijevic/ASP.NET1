using Application.DTO.Search;
using Application.UseCases.Queries;
using Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public AdminController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<AdminController>
        [HttpGet]
        public IActionResult Get([FromQuery] BasicSearch search, [FromServices] IGetUseCaseLogsQuery query)
        {
            return Ok(_handler.HandleQuery(query,search));

        }

        // GET api/<AdminController>/5
        [HttpGet("errors")]
        public IActionResult GetErrors([FromQuery] BasicSearch search, [FromServices] IGetErrorLogsQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));

        }

    }
}
