using FluentResults;
using LibraryManagement.Application.ViewModels;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Queries.Loans
{
    public class GetLoanQueryHandler : IRequestHandler<GetLoanQuery, Result<LoanViewModel>>
    {
        private readonly ILoanRepository _loanRepository;

        public GetLoanQueryHandler(ILoanRepository loanRepository) => _loanRepository = loanRepository;

        public async Task<Result<LoanViewModel>> Handle(GetLoanQuery request, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.GetOneAsync(request.Id);

            if (loan is null) 
                return Result.Fail("Loan not found");

            var books = loan.BorrowedBooks.Select(x => x.Book).ToList();
            var user = new UserViewModel(loan.User.Name, loan.User.Email);
            var loanViewModel = new LoanViewModel(loan.LoanDate, loan.DevolutionDate, user);
            var booksViewModel = books.Select(b => new BookViewModel(b.Title, b.Author, b.Isbn, (ushort)b.PublicationYear));
            loanViewModel.Books.AddRange(booksViewModel);

            return Result.Ok(loanViewModel);
        }
    }
}
