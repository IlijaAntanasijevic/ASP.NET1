using Application.DTO.Admin;
using Application.DTO.Users;
using Application.UseCases.Commands.Admin;
using Application.UseCases.Commands.Users;
using Application.UseCases.Queries.Admin;
using Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API.Controllers
{
    [Route("api/admin/openai")]
    [ApiController]
    public class OpenAiController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public OpenAiController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get([FromServices] IGetOpenAiSetupQuery query)
        {
            return Ok(_handler.HandleQuery(query,0));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post(OpenAiSetupDataDto data, [FromServices] IUpdateOpenAiSetupCommand command)
        {
            _handler.HandleCommand(command, data);
            return NoContent();
        }

        [Authorize]
        [HttpGet]
        [Route("~/api/admin/openai-conversations")]
        public IActionResult GetUserConversations([FromServices] IGetOpenAiConversationsQuery query)
        {
            return Ok(_handler.HandleQuery(query,0));
        }

    }
}
