using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Commands.Loans
{
    public class UpdateLoanCommandHandler : IRequestHandler<UpdateLoanCommand, Unit>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IBookRepository _bookRepository;

        public UpdateLoanCommandHandler(ILoanRepository loanRepository, IBookRepository bookRepository)
        {
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
        }

        public async Task<Unit> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.GetOneAsync(request.Id);
            if (loan != null)
            {
                var bookExists = await ValidateIfBooksExistsAsync(request.BookIds);
                if (bookExists)
                    await UpdateLoanAsync(request, loan);
            }

            return Unit.Value;
        }

        private async Task<bool> ValidateIfBooksExistsAsync(List<Guid> bookIds)
        {
            var books = await _bookRepository.GetAllAsync();
            var dataBookIds = books.Select(book => book.Id).ToList();
            return bookIds.TrueForAll(id => dataBookIds.Contains(id));
        }

        private async Task UpdateLoanAsync(UpdateLoanCommand request, Loan loan)
        {
            loan.Update(request.LoanDate, request.DevolutionDate, request.BookIds, loan.UserId);
            await _loanRepository.InactivateAsync(request.Id);
            await _loanRepository.AddAsync(loan);
        }
    }
}
