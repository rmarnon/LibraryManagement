using LibraryManagement.Application.Commands.Users;
using LibraryManagement.Application.Queries.Users;
using LibraryManagement.Application.ViewModels;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace LibraryManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator) => _mediator = mediator;

        // <summary>Login user data.</summary>
        /// <param name="command">Properties to login.</param>
        /// <response code="200">User found.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="500">Server error.</response>
        [AllowAnonymous]
        [HttpPut("login")]
        [ProducesResponseType(typeof(LoginUserViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Errors);
        }

        /// <summary>
        /// Register a new user in the system.
        /// </summary>
        /// <param name="command">User to register.</param>
        /// <response code="201">Registered successfully.</response>
        /// <response code="400">Invalid data.</response>
        /// <response code="500">Server error.</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsFailed
                ? BadRequest(result.Errors)
                : CreatedAtAction(nameof(CreateUser), result.Value);
        }

        // <summary>Update user data.</summary>
        /// <param name="command">Properties to update</param>
        /// <response code="204">User object updated.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="500">Server error.</response>
        [HttpPut]
        [Authorize(Roles = nameof(Role.Admin))]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUser([FromBody][Required] UpdateUserCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess
                ? NoContent()
                : BadRequest(result.Errors);
        }

        /// <summary>Return specified user.</summary>
        /// <param name="id" example="bc847776-9b58-460c-8eda-b221d3644a7a">User id.</param>
        /// <response code="200">User found.</response>
        /// <response code="404">There is no registered user with specified id.</response>
        /// <response code="500">Server error.</response>
        [HttpGet("{id}")]
        [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.User)}")]
        [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserById([FromRoute][Required] Guid id)
        {
            var query = new GetUserQuery(id);
            var result = await _mediator.Send(query);

            return result.IsSuccess
                ? Ok(result.Value)
                : NotFound(result.Errors);
        }

        /// <summary>Get all users.</summary>
        /// <param name="query">Query of the file</param>
        /// <param name="pagination">Pagination input.</param>
        /// <response code="200">A list of users attending the specified parameters.</response>
        /// <response code="404">There is no registered users.</response>
        /// <response code="500">Server error.</response>
        [HttpGet]
        [Authorize(Roles = nameof(Role.Admin))]
        [ProducesResponseType(typeof(List<UserViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUsers([FromQuery] string? query, [FromQuery] PaginationInput pagination)
        {
            var usersQuery = new GetAllUsersQuery(query, pagination);
            var result = await _mediator.Send(usersQuery);

            return result.IsSuccess
                ? Ok(result.Value)
                : NotFound(result.Errors);
        }

        // <summary>Delete user data.</summary>
        /// <param name="id" example="bc847776-9b58-460c-8eda-b221d3644a7a">User id.</param>
        /// <response code="204">User object deleted.</response>
        /// <response code="404">There is no registered user with specified id.</response>
        /// <response code="500">Server error.</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(Role.Admin))]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
