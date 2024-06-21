using FluentResults;
using LibraryManagement.Application.ViewModels;
using MediatR;

namespace LibraryManagement.Application.Commands.Loans
{
    public class CreateLoanCommand : IRequest<Result<LoanViewModel>>
    {
        public Guid UserId { get; set; }
        public DateTime LoanDate { get; set; }
        public List<Guid> BookIds { get; set; } = [];
    }
}
