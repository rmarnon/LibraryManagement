using LibraryManagement.Application.ViewModels;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Queries.Loans
{
    public class GetAllLoanQueryHandler : IRequestHandler<GetAllLoansQuery, List<LoanViewModel>>
    {
        private readonly ILoanRepository _loanRepository;

        public GetAllLoanQueryHandler(ILoanRepository loanRepository) => _loanRepository = loanRepository;

        public async Task<List<LoanViewModel>> Handle(GetAllLoansQuery request, CancellationToken cancellationToken)
        {
            var loans = await _loanRepository.GetAllAsync();
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

            return loanViewModels;
        }
    }
}
