using LibraryManagement.Application.Commands.Users;
using LibraryManagement.Application.Queries.Users;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator) => _mediator = mediator;

        [AllowAnonymous]
        [HttpPut("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Errors);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsFailed
                ? BadRequest(result.Errors)
                : CreatedAtAction(nameof(CreateUser), result.Value);
        }

        [HttpPut]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> UpdateUser([FromBody][Required] UpdateUserCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess
                ? NoContent()
                : BadRequest(result.Errors);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.User)}")]
        public async Task<IActionResult> GetUserById([FromRoute][Required] Guid id)
        {
            var query = new GetUserQuery(id);
            var result = await _mediator.Send(query);

            return result.IsSuccess
                ? Ok(result.Value)
                : NotFound(result.Errors);
        }

        [HttpGet]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> GetAllUsers([FromQuery] string? query, [FromQuery] PaginationInput pagination)
        {
            var usersQuery = new GetAllUsersQuery(query, pagination);
            var result = await _mediator.Send(usersQuery);

            return result.IsSuccess
                ? Ok(result.Value)
                : NotFound(result.Errors);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> DeleteUser([FromRoute][Required] Guid id)
        {
            var command = new DeleteUserCommand(id);
            var result = await _mediator.Send(command);

            return result.IsSuccess
                ? NoContent()
                : NotFound(result.Errors);
        }
    }
}
