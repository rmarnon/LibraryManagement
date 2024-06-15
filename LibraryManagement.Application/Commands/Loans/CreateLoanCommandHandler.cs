using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Commands.Loans
{
    public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, Unit>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;

        public CreateLoanCommandHandler(ILoanRepository loanRepository, IUserRepository userRepository, IBookRepository bookRepository)
        {
            _loanRepository = loanRepository;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
        }

        public async Task<Unit> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            var userLoan = await _loanRepository.ExistsLoanByUserIdAsync(request.UserId);
            var userExist = await _userRepository.ExistsAsync(request.UserId);

            if (userExist && !userLoan)
            {
                var bookExists = await ValidateIfBooksExistsAsync(request.BookIds);

                if (bookExists)
                    await CreateNewLoanAsync(request);
            }

            return Unit.Value;
        }

        private async Task<bool> ValidateIfBooksExistsAsync(List<Guid> bookIds)
        {
            var books = await _bookRepository.GetAllAsync();
            var dataBookIds = books.Select(book => book.Id).ToList();
            return bookIds.TrueForAll(id => dataBookIds.Contains(id));
        }

        private async Task CreateNewLoanAsync(CreateLoanCommand request)
        {
            var loan = new Loan(request.LoanDate, request.UserId);
            foreach (var bookId in request.BookIds)
            {
                loan.BorrowedBooks.Add(new(bookId, loan.Id));
            }

            await _loanRepository.AddAsync(loan);
        }
    }
}
