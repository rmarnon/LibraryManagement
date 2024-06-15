﻿using LibraryManagement.Application.Commands.Loans;
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
            await _mediator.Send(command);
            return Created(string.Empty, command);
        }

        [HttpPost("return")]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> ReturnLoan([FromBody] ReturnLoanCommand command)
        {
            await _mediator.Send(command);
            return Created(string.Empty, command);
        }

        [HttpPut]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> UpdateLoan([FromBody][Required] UpdateLoanCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.User)}")]
        public async Task<IActionResult> GetLoanById([FromRoute] Guid id)
        {
            var query = new GetLoanQuery(id);
            var loan = await _mediator.Send(query);
            return loan is null
                ? NotFound()
                : Ok(loan);
        }

        [HttpGet]
        [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.User)}")]
        public async Task<IActionResult> GetAllLoans([FromQuery] string? query)
        {
            var loanQuery = new GetAllLoansQuery(query);
            var users = await _mediator.Send(loanQuery);
            return Ok(users);
        }
    }
}
