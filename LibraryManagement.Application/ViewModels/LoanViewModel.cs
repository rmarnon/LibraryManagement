namespace LibraryManagement.Application.ViewModels
{
    public record LoanViewModel
    {
        public DateTime LoanDate { get; private init; }
        public DateTime? DevolutionDate { get; private init; }
        public UserViewModel User { get; private init; }
        public int TotalLoanBooks { get; private set; }
        public List<BookViewModel> Books { get; private init; } = [];

        public LoanViewModel(DateTime loanDate, DateTime? devolutionDate, UserViewModel user, int totalLoanBooks)
        {
            LoanDate = loanDate;
            DevolutionDate = devolutionDate;
            TotalLoanBooks = totalLoanBooks;
            User = user;
        }
    }
}
