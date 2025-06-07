using Application.DTO.Search;
using Application.UseCases.Queries.Apartment;
using Application.UseCases.Queries.Chat;
using Implementation.UseCases;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public ChatController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public IActionResult Get([FromServices] IGetChatListQuery query) => Ok(_handler.HandleQuery(query, null));

        [HttpGet("{id}")]
        public IActionResult Get([FromServices] IGetChatMessages query, int id) => Ok(_handler.HandleQuery(query, id));

    }
}
