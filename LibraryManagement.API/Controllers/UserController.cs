using LibraryManagement.Application.Commands.Users;
using LibraryManagement.Application.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            await _mediator.Send(command);
            return Created(string.Empty, command);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody][Required] UpdateUserCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute][Required] Guid id)
        {
            var query = new GetUserQuery(id);
            var user = await _mediator.Send(query);
            return user is null
                ? NotFound()
                : Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] string? query)
        {
            var usersQuery = new GetAllUsersQuery(query);
            var users = await _mediator.Send(usersQuery);
            return Ok(users);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute][Required] Guid id)
        {
            var command = new DeleteUserCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
