using API.Core;
using API.Core.JWT;
using API.DTO;
using Application.DTO;
using Application.UseCases.Commands.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
    

        // POST api/<AuthController>
        [HttpPost]
        [Route("/api/login")]
        public IActionResult Post([FromBody] AuthRequest data, [FromServices] JwtTokenCreator tokenCreator)
        {
            var token = tokenCreator.Create(data.Email, data.Password);

            return Ok(new AuthResponse { Token = token });
        }

        // DELETE api/<AuthController>/5
        [Authorize]
        [HttpDelete]
        public IActionResult Delete([FromServices] ITokenStorage storage)
        {
            storage.Remove(this.Request.GetTokenId().Value);

            return NoContent();
        }
    }
}
