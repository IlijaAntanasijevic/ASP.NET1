using API.Core.JWT;
using Application.DTO;
using Application.UseCases.Commands.Users;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // GET: api/<AuthController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AuthController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        //Register
        //public IActionResult Post([FromBody] RegisterUserDto data, [FromServices] IRegisterUserCommand command)
        //{
        //    _handler.HandleCommand(command, data);
        //    return Ok();
        //}

        // POST api/<AuthController>
        [HttpPost]
        public IActionResult Post([FromBody] AuthRequest data, [FromServices] JwtTokenCreator tokenCreator)
        {
            var token = tokenCreator.Create(data.Email, data.Password);

            return Ok(new AuthResponse { Token = token });
        }

        // PUT api/<AuthController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
