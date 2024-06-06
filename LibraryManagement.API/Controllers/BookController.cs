using LibraryManagement.Application.Commands.Books;
using LibraryManagement.Application.Commands.Users;
using LibraryManagement.Application.Queries.Books;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookCommand command)
        {
            await _mediator.Send(command);
            return Created(string.Empty, command);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks([FromQuery] string? query)
        {
            var booksQuery = new GetAllBooksQuery(query);
            var books = await _mediator.Send(booksQuery);
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute] Guid id)
        {
            var query = new GetBookQuery(id);
            var book = await _mediator.Send(query);
            return book is null
                ? NotFound() 
                : Ok(book);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute][Required] Guid id)
        {
            var command = new DeleteBookCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
