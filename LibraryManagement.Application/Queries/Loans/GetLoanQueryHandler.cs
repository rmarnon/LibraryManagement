using LibraryManagement.Application.ViewModels;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Queries.Loans
{
    public class GetLoanQueryHandler : IRequestHandler<GetLoanQuery, LoanViewModel>
    {
        private readonly ILoanRepository _loanRepository;

        public GetLoanQueryHandler(ILoanRepository loanRepository) => _loanRepository = loanRepository;

        public async Task<LoanViewModel> Handle(GetLoanQuery request, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.GetOneAsync(request.Id);

            if (loan is null) return null;

            var books = loan.BorrowedBooks.Select(x => x.Book).ToList();
            var user = new UserViewModel(loan.User.Name, loan.User.Email);
            var loanViewModel = new LoanViewModel(loan.LoanDate, loan.DevolutionDate, user, books.Count);
            var booksViewModel = books.Select(b => new BookViewModel(b.Title, b.Author, b.Isbn, b.PublicationYear));
            loanViewModel.Books.AddRange(booksViewModel);

            return loanViewModel;
        }
    }
}
