using LibraryManagement.Application.Commands.Loans;
using LibraryManagement.Application.Queries.Loans;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoanController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> CreateLoan([FromBody] CreateLoanCommand command)
        {
            await _mediator.Send(command);
            return Created(string.Empty, command);
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnLoan([FromBody] ReturnLoanCommand command)
        {
            await _mediator.Send(command);
            return Created(string.Empty, command);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoanById([FromRoute] Guid id)
        {
            var query = new GetLoanQuery(id);
            var loan = await _mediator.Send(query);
            return loan is null
                ? NotFound()
                : Ok(loan);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLoans([FromQuery] string? query)
        {
            var loanQuery = new GetAllLoansQuery(query);
            var users = await _mediator.Send(loanQuery);
            return Ok(users);
        }
    }
}
