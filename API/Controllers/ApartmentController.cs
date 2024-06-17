using Application.DTO.Apartments;
using Application.DTO.Search;
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
    public class ApartmentController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public ApartmentController(UseCaseHandler handler)
        {
            _handler = handler;
        }


        [HttpGet]
        public IActionResult Get([FromQuery] ApartmentSearch search, [FromServices] IGetApartmentsQuery query)
            => Ok(_handler.HandleQuery(query, search));




        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindApartmentQuery query) => Ok(_handler.HandleQuery(query,id));
        


        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CreateApartmentDto data, ICreateApartmentCommand command)
        {
            _handler.HandleCommand(command, data);
            return Created();
        }



        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }


   
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteApartmentCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
