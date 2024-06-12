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
            var loan = await _loanRepository.GetLoanByIdAsync(request.Id);

            if (loan is null) return null;

            var books = loan.BorrowedBooks.Select(x => x.Book);

            var user = new UserViewModel(loan.User.Name, loan.User.Email);
            var loanViewModel = new LoanViewModel(loan.LoanDate, loan.DevolutionDate, user, books.Count());

            foreach (var book in books)
            {
                loanViewModel.Books.Add(new(book.Title, book.Author, book.Isbn, book.PublicationYear));
            }

            return loanViewModel;
        }
    }
}
