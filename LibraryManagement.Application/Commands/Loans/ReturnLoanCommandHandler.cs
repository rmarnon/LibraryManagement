using FluentResults;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Commands.Loans
{
    public class ReturnLoanCommandHandler : IRequestHandler<ReturnLoanCommand, Result>
    {
        private readonly ILoanRepository _loanRepository;

        public ReturnLoanCommandHandler(ILoanRepository loanRepository) => _loanRepository = loanRepository;

        public async Task<Result> Handle(ReturnLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.GetLoanByUserIdAsync(request.UserId);
            var bookIds = loan?.BorrowedBooks?.Select(x => x.BookId).ToList();

            if (bookIds != null && bookIds.TrueForAll(id => request.BookIds.Contains(id)))
            {
                await _loanRepository.InactivateAsync(loan.Id);
                return Result.Ok();
            }

            return Result.Fail("Returned books differ from borrowed books");
        }
    }
}
