using MediatR;

namespace LibraryManagement.Application.Commands.Loans
{
    public class CreateLoanCommand : IRequest
    {
        public Guid UserId { get; set; }
        public DateTime LoanDate { get; set; }
        public List<Guid> BookIds { get; set; } = [];
    }
}
