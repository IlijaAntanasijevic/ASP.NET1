using API.DTO;
using Application.DTO.Users;
using Application.UseCases.Commands.Users;
using Implementation;
using Implementation.UseCases;
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

        [HttpPost("/register")]
        public IActionResult Post([FromBody] RegisterUserDto data, [FromServices] IRegisterUserCommand command)
        {
            _handler.HandleCommand(command, data);
            return Ok();
        }


        [HttpGet("/usecases/info")]
        public IActionResult Get() => Ok(UseCaseInfo.AllUseCases);

    }
}
