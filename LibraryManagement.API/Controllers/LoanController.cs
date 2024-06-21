using LibraryManagement.Application.Commands.Loans;
using LibraryManagement.Application.Queries.Loans;
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
    public class LoanController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoanController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> CreateLoan([FromBody] CreateLoanCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsFailed
                ? BadRequest(result.Errors)
                : CreatedAtAction(nameof(CreateLoan), result.Value);
        }

        [HttpPost("return")]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> ReturnLoan([FromBody] ReturnLoanCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess
                ? NoContent()
                : BadRequest(result.Errors);
        }

        [HttpPut]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> UpdateLoan([FromBody][Required] UpdateLoanCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess
                ? NoContent()
                : BadRequest(result.Errors);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.User)}")]
        public async Task<IActionResult> GetLoanById([FromRoute] Guid id)
        {
            var query = new GetLoanQuery(id);
            var result = await _mediator.Send(query);
            return result.IsSuccess
                ? Ok(result.Value)
                : NotFound(result.Errors);
        }

        [HttpGet]
        [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.User)}")]
        public async Task<IActionResult> GetAllLoans([FromQuery] string? query)
        {
            var loanQuery = new GetAllLoansQuery(query);
            var result = await _mediator.Send(loanQuery);
            return result.IsSuccess
                ? Ok(result.Value)
                : NotFound(result.Errors);
        }
    }
}
