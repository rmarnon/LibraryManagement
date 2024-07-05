using LibraryManagement.Application.Commands.Loans;
using LibraryManagement.Application.Queries.Loans;
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
    public class LoanController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoanController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Register a new loan in the system.
        /// </summary>
        /// <param name="command">Loan to register.</param>
        /// <response code="201">Registered successfully.</response>
        /// <response code="400">Invalid data.</response>
        /// <response code="500">Server error.</response>
        [HttpPost]
        [Authorize(Roles = nameof(Role.Admin))]
        [ProducesResponseType(typeof(LoanViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateLoan([FromBody] CreateLoanCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsFailed
                ? BadRequest(result.Errors)
                : CreatedAtAction(nameof(CreateLoan), result.Value);
        }

        /// <summary>Register a new devolution loan.</summary>
        /// <param name="command">Devolution to register.</param>
        /// <response code="204">Registered devolution successfully.</response>
        /// <response code="400">Invalid data.</response>
        /// <response code="500">Server error.</response>
        [HttpPost("return")]
        [Authorize(Roles = nameof(Role.Admin))]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReturnLoan([FromBody] ReturnLoanCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess
                ? NoContent()
                : BadRequest(result.Errors);
        }

        // <summary>Update loan data.</summary>
        /// <param name="command">Properties to update</param>
        /// <response code="204">Loan object updated.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="500">Server error.</response>
        [HttpPut]
        [Authorize(Roles = nameof(Role.Admin))]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateLoan([FromBody][Required] UpdateLoanCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess
                ? NoContent()
                : BadRequest(result.Errors);
        }

        /// <summary>Return specified loan.</summary>
        /// <param name="id" example="bc847776-9b58-460c-8eda-b221d3644a7a">Loan id.</param>
        /// <response code="200">Loan found.</response>
        /// <response code="404">There is no registered loan with specified id.</response>
        /// <response code="500">Server error.</response>
        [HttpGet("{id}")]
        [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.User)}")]
        [ProducesResponseType(typeof(LoanViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLoanById([FromRoute] Guid id)
        {
            var query = new GetLoanQuery(id);
            var result = await _mediator.Send(query);

            return result.IsSuccess
                ? Ok(result.Value)
                : NotFound(result.Errors);
        }

        /// <summary>Get all loans.</summary>
        /// <param name="query">Query of the file</param>
        /// <param name="pagination">Pagination input.</param>
        /// <response code="200">A list of loans attending the specified parameters.</response>
        /// <response code="404">There is no registered loans.</response>
        /// <response code="500">Server error.</response>
        [HttpGet]
        [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.User)}")]
        [ProducesResponseType(typeof(List<LoanViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllLoans([FromQuery] string? query, [FromQuery] PaginationInput pagination)
        {
            var loanQuery = new GetAllLoansQuery(query, pagination);
            var result = await _mediator.Send(loanQuery);

            return result.IsSuccess
                ? Ok(result.Value)
                : NotFound(result.Errors);
        }
    }
}
