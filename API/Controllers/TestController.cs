using API.Core.JWT;
using Application.DTO;
using Application.UseCases.Commands.ApartmentType;
using Application.UseCases.Commands.Users;
using Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private UseCaseHandler _handler;

        public TestController(UseCaseHandler handler)
        {
            _handler = handler;
        }


        // POST api/<TestController>
     
        [HttpPost("/api/apartment")]
        public IActionResult Post([FromBody] NamedDto data, [FromServices] ICreateApartmentTypeCommand command)
        {
            _handler.HandleCommand(command,data);
            return Created();
        }

        [HttpPost("/auth/register")]
        public IActionResult Post([FromBody] RegisterUserDto data, [FromServices] IRegisterUserCommand command)
        {
            _handler.HandleCommand(command,data);
            return Ok();
        }

        [HttpPost("/auth")]
        public IActionResult Post([FromBody] AuthRequest data, [FromServices] JwtTokenCreator tokenCreator) 
        {
            var token = tokenCreator.Create(data.Email, data.Password);

            return Ok(new AuthResponse { Token = token });
        }

       
    }
}
