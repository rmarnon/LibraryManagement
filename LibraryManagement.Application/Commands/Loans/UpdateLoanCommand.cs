using MediatR;

namespace LibraryManagement.Application.Commands.Loans
{
    public class UpdateLoanCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DevolutionDate { get; set; }
        public List<Guid> BookIds { get; set; } = [];
    }
}
