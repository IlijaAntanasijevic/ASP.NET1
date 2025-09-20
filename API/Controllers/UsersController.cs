using API.DTO;
using Application.DTO.Search;
using Application.DTO.Users;
using Application.UseCases.Commands.Users;
using Application.UseCases.Queries.Users;
using Implementation.UseCases;
using Implementation.UseCases.Commands.Users;
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
            => Ok(_handler.HandleQuery(query, id));


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


        //api/1 => Update user
        [HttpPut("{id}")]
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

        [HttpPut("avatar")]
        public IActionResult ChangeProfilePhoto([FromForm] FileUploadDto request, [FromServices] IChangeProfilePhotoCommand command)
        {
            IEnumerable<string> allowedExtensions = new List<string>
            {
                ".jpg", ".jpeg", ".png"
            };

            var extension = Path.GetExtension(request.File.FileName);

            if (!allowedExtensions.Contains(extension))
            {
                return new UnsupportedMediaTypeResult();
            }

            var fileName = Guid.NewGuid().ToString() + extension;
            var savePath = Path.Combine("wwwroot", "users", fileName);
            using var fs = new FileStream(savePath, FileMode.Create);
            request.File.CopyTo(fs);

            _handler.HandleCommand(command, fileName);

            return NoContent();
        }
    }
}
