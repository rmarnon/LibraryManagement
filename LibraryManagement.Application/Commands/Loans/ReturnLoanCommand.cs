using FluentResults;
using MediatR;

namespace LibraryManagement.Application.Commands.Loans
{
    public class ReturnLoanCommand : IRequest<Result>
    {
        public Guid UserId { get; set; }
        public DateTime DevolutionDate { get; set; }
        public List<Guid> BookIds { get; set; } = [];
    }
}
