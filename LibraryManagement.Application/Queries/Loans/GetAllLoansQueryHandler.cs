using FluentResults;
using LibraryManagement.Application.ViewModels;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Queries.Loans
{
    public class GetAllLoansQueryHandler : IRequestHandler<GetAllLoansQuery, Result<List<LoanViewModel>>>
    {
        private readonly ILoanRepository _loanRepository;

        public GetAllLoansQueryHandler(ILoanRepository loanRepository) => _loanRepository = loanRepository;

        public async Task<Result<List<LoanViewModel>>> Handle(GetAllLoansQuery request, CancellationToken cancellationToken)
        {
            var loans = await _loanRepository.GetAllAsync();

            if (loans is null) 
                return Result.Fail<List<LoanViewModel>>("Empty loan list");

            var loanViewModels = new List<LoanViewModel>();
            LoanViewModel loanViewModel;

            foreach (var loan in loans)
            {
                var books = loan.BorrowedBooks.Select(x => x.Book).ToList();
                var booksViewModel = books.Select(b => new BookViewModel(b.Title, b.Author, b.Isbn, b.PublicationYear));
                var user = new UserViewModel(loan.User.Name, loan.User.Email);
                loanViewModel = new(loan.LoanDate, loan.DevolutionDate, user);
                loanViewModel.Books.AddRange(booksViewModel);
                loanViewModels.Add(loanViewModel);
            }

            return Result.Ok(loanViewModels);
        }
    }
}
