using LibraryManagement.Application.Commands.Books;
using LibraryManagement.Application.Queries.Books;
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
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [Authorize(Roles = $"{nameof(Role.Admin)}")]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsFailed
                ? BadRequest(result.Errors)
                : CreatedAtAction(nameof(CreateBook), result.Value);
        }

        [HttpPut]
        [Authorize(Roles = $"{nameof(Role.Admin)}")]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess
                ? NoContent()
                : BadRequest(result.Errors);
        }

        [HttpGet]
        [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.User)}")]
        public async Task<IActionResult> GetAllBooks([FromQuery] string? query, [FromQuery] PaginationInput pagination)
        {
            var booksQuery = new GetAllBooksQuery(query, pagination);
            var result = await _mediator.Send(booksQuery);

            return result.IsSuccess
                ? Ok(result.Value)
                : NotFound(result.Errors);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.User)}")]
        public async Task<IActionResult> GetBookById([FromRoute] Guid id)
        {
            var query = new GetBookQuery(id);
            var result = await _mediator.Send(query);

            return result.IsSuccess
                ? Ok(result.Value)
                : NotFound(result.Errors);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.User)}")]
        public async Task<IActionResult> DeleteBook([FromRoute][Required] Guid id)
        {
            var command = new DeleteBookCommand(id);
            var result = await _mediator.Send(command);

            return result.IsSuccess
                ? NoContent()
                : NotFound(result.Errors);
        }
    }
}
