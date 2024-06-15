using Application.DTO;
using Application.DTO.Search;
using Application.UseCases.Commands.Users;
using Application.UseCases.Queries.Users;
using Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UseCaseHandler _handler;

        public UsersController(UseCaseHandler handler)
        {
            _handler = handler;
        }


          
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindUserQuery query) 
            => Ok(_handler.HandleQuery(query,id));
     

        [HttpGet]
        public IActionResult Get([FromQuery] UserSearch search, [FromServices] IGetUsersQuery query)
            => Ok(_handler.HandleQuery(query, search));



        [HttpPost]
        public IActionResult Post([FromBody] RegisterUserDto data, [FromServices] IRegisterUserCommand command)
        {
            _handler.HandleCommand(command, data);
            return Created();
        }

   
        [HttpPut("{id}/access")]
        [Authorize]
        
        public IActionResult ModifyAccess(int id, [FromBody] UpdateUserAccessDto data,
                                                  [FromServices] IUpdateUseAccessCommand command)
        {
            data.UserId = id;
            _handler.HandleCommand(command, data);
            return NoContent();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
