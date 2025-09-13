using Application.DTO.Admin;
using Application.DTO.Search;
using Application.DTO.Users;
using Application.UseCases.Commands.Users;
using Application.UseCases.Queries;
using Application.UseCases.Queries.Admin;
using Implementation.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AdminController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public AdminController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<AdminController>
        [HttpGet]
        public IActionResult Get([FromQuery] BasicSearch search, [FromServices] IGetUseCaseLogsQuery query)
        {
            return Ok(_handler.HandleQuery(query,search));

        }

        // GET api/<AdminController>/5
        [HttpGet("errors")]
        public IActionResult GetErrors([FromQuery] BasicSearch search, [FromServices] IGetErrorLogsQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));

        }

        [HttpGet("dashboard")]
        public IActionResult GetDashboardData([FromQuery] BasicSearch search, [FromServices] IGetAdminDashboardQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        [HttpGet("apartments")]
        public IActionResult GetApartmentsDashboard([FromQuery] AdminApartmentsSearchDto search, [FromServices] IGetAdminApartmentsQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        [HttpGet("apartments/filters")]
        public IActionResult GetApartmentFilters([FromQuery] BasicSearch search, [FromServices] IGetAdminApartmentsFiltersQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        [HttpGet("users")]
        public IActionResult GetAllUsers([FromQuery] BasicSearch search, [FromServices] IGetUsersAdminQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        [HttpGet("users/use-cases/{id}")]
        public IActionResult GetAllUsers(int id, [FromServices] IGetUserUseCasesQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }

        //api/1/access => Modify user access 
        [HttpPut("users/use-cases/{id}")]
        public IActionResult ModifyAccess(int id, [FromBody] UpdateUserAccessDto data,
                                                  [FromServices] IUpdateUseAccessCommand command)
        {
            data.UserId = id;
            _handler.HandleCommand(command, data);
            return NoContent();
        }

        [HttpGet("bookings")]
        public IActionResult GetBookings([FromQuery] AdminBookingsSearchDto search, [FromServices] IGetAdminBookingsQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

    }
}
