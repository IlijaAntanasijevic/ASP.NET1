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
    public class PaymentController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public PaymentController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<PaymentController>
        [HttpGet]
        public IActionResult Get([FromQuery] BasicSearch search, [FromServices] IGetPaymentsQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));

        }


        // POST api/<PaymentController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] NamedDto data, [FromServices] ICreatePaymentCommand command)
        {
            _handler.HandleCommand(command, data);
            return NoContent();
        }

    }
}
