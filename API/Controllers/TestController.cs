using Application.DTO;
using Application.UseCases.Commands.ApartmentType;
using Application.UseCases.Commands.Users;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
       
        // POST api/<TestController>

        [HttpPost]
        public void Post([FromBody] NamedDto data, [FromServices] ICreateApartmentTypeCommand command) => command.Execute(data);

        [HttpPost("/auth/register")]
        public IActionResult Post([FromBody] RegisterUserDto data, [FromServices] IRegisterUserCommand command)
        {
            command.Execute(data);
            return Ok();
        }

       
    }
}
