using LibraryManagement.Application.Commands.Books;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    }
}
