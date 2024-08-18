using LibraryManagement.Application.Commands.Books;
using LibraryManagement.Application.Queries.Books;
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
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Register a new book in the system.
        /// </summary>
        /// <remarks>Request:
        /// {
        ///      "title": "Some title",
        ///      "author": "Some author",
        ///      "isbn": "1234567890",
        ///      "publicationYear": 2000
        /// }
        /// </remarks>
        /// <param name="command">Book to register.</param>
        /// <returns>Book created</returns>
        /// <response code="201">Registered successfully.</response>
        /// <response code="400">Invalid data.</response>
        /// <response code="500">Server error.</response>
        [HttpPost]
        [Authorize(Roles = $"{nameof(Role.Admin)}")]
        [ProducesResponseType(typeof(BookViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsFailed
                ? BadRequest(result.Errors)
                : CreatedAtAction(nameof(CreateBook), result.Value);
        }

        // <summary>Update book data.</summary>
        /// <param name="command">Properties to update</param>
        /// <response code="204">Book object updated.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="500">Server error.</response>
        [HttpPut]
        [Authorize(Roles = $"{nameof(Role.Admin)}")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess
                ? NoContent()
                : BadRequest(result.Errors);
        }

        /// <summary>Get all books.</summary>
        /// <param name="query">Query of the file</param>
        /// <param name="pagination">Pagination input.</param>
        /// <response code="200">A list of books attending the specified parameters.</response>
        /// <response code="404">There is no registered books.</response>
        /// <response code="500">Server error.</response>
        [HttpGet]
        [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.User)}")]
        [ProducesResponseType(typeof(List<BookViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBooks([FromQuery] string? query, [FromQuery] PaginationInput pagination)
        {
            var booksQuery = new GetAllBooksQuery(query, pagination);
            var result = await _mediator.Send(booksQuery);

            return result.IsSuccess
                ? Ok(result.Value)
                : NotFound(result.Errors);
        }

        /// <summary>Return specified book.</summary>
        /// <param name="id" example="bc847776-9b58-460c-8eda-b221d3644a7a">Book id.</param>
        /// <response code="200">Book found.</response>
        /// <response code="404">There is no registered book with specified id.</response>
        /// <response code="500">Server error.</response>
        [HttpGet("{id}")]
        [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.User)}")]
        [ProducesResponseType(typeof(BookViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBookById([FromRoute] Guid id)
        {
            var query = new GetBookQuery(id);
            var result = await _mediator.Send(query);

            return result.IsSuccess
                ? Ok(result.Value)
                : NotFound(result.Errors);
        }

        // <summary>Delete book data.</summary>
        /// <param name="id" example="bc847776-9b58-460c-8eda-b221d3644a7a">Book id.</param>
        /// <response code="204">Book object deleted.</response>
        /// <response code="404">There is no registered book with specified id.</response>
        /// <response code="500">Server error.</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.User)}")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
