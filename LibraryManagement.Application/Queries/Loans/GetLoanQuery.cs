using LibraryManagement.Application.ViewModels;
using MediatR;

namespace LibraryManagement.Application.Queries.Loans
{
    public record GetLoanQuery(Guid Id) : IRequest<LoanViewModel>
    {
        public Guid Id { get; private set; } = Id;
    }
}
