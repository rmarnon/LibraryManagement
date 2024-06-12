using MediatR;

namespace LibraryManagement.Application.Commands.Loans
{
    public class ReturnLoanCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public DateTime DevolutionDate { get; set; }
        public List<Guid> BookIds { get; set; } = [];
    }
}
