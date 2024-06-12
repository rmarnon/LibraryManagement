using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Commands.Loans
{
    public class ReturnLoanCommandHandler : IRequestHandler<ReturnLoanCommand, Unit>
    {
        private readonly ILoanRepository _loanRepository;

        public ReturnLoanCommandHandler(ILoanRepository loanRepository) => _loanRepository = loanRepository;

        public async Task<Unit> Handle(ReturnLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.GetLoanAsync(request.UserId);
            var bookIds = loan?.BorrowedBooks?.Select(x => x.BookId).ToList();

            if (bookIds != null && bookIds.All(id => request.BookIds.Contains(id)))
            {
                loan.Inactivate();
                await _loanRepository.UpdateAsync(loan);
            }

            return Unit.Value;
        }
    }
}
