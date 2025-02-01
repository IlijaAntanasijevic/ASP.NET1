using Application.DTO.Search;
using Application.DTO.Users;
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


        //api/users/1 => Find user  
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindUserQuery query) 
            => Ok(_handler.HandleQuery(query,id));
     

        //api/users => Get All
        [HttpGet]
        public IActionResult Get([FromQuery] UserSearch search, [FromServices] IGetUsersQuery query)
            => Ok(_handler.HandleQuery(query, search));


        //api/register => Register
        [HttpPost]
        [Route("/api/register")]
        public IActionResult Post([FromBody] RegisterUserDto data, [FromServices] IRegisterUserCommand command)
        {
            _handler.HandleCommand(command, data);
            return Created();
        }

        //api/1/access => Modify user access 
        [HttpPut("{id}/access")]
        [Authorize]
        public IActionResult ModifyAccess(int id, [FromBody] UpdateUserAccessDto data,
                                                  [FromServices] IUpdateUseAccessCommand command)
        {
            data.UserId = id;
            _handler.HandleCommand(command, data);
            return NoContent();
        }
        //api/1 => Update user
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] UpdateUserDto data, 
                                         [FromServices] IUpdateUserCommand command)
        {
            data.Id = id;
            _handler.HandleCommand(command, data);
            return NoContent();
        }

        //api/5 => Delete 
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id, [FromServices] IDeleteUserCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
