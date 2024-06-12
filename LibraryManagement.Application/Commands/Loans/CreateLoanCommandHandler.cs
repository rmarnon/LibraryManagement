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
            var userLoan = await _loanRepository.ExistsLoanAsync(request.UserId);
            var userExist = await _userRepository.ExistsAsync(request.UserId);

            if (userExist && !userLoan)
            {
                var books = await _bookRepository.GetAllAsync();
                var bookIds = books.Select(x => x.Id);
                var bookExists = request.BookIds.All(id => bookIds.Contains(id));

                if (bookExists)
                {
                    var loan = new Loan(request.LoanDate, request.UserId);
                    foreach (var bookId in request.BookIds)
                    {
                        loan.BorrowedBooks.Add(new(bookId, loan.Id));
                    }

                    await _loanRepository.AddAsync(loan);
                }
            }

            return Unit.Value;
        }
    }
}
