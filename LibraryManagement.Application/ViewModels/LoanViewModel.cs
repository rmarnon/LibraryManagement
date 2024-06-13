namespace LibraryManagement.Application.ViewModels
{
    public record LoanViewModel
    {
        public DateTime LoanDate { get; private init; }
        public DateTime? DevolutionDate { get; private init; }
        public UserViewModel User { get; private init; }
        public int TotalLoanBooks => Books.Count;
        public List<BookViewModel> Books { get; private init; } = [];

        public LoanViewModel(DateTime loanDate, DateTime? devolutionDate, UserViewModel user)
        {
            LoanDate = loanDate;
            DevolutionDate = devolutionDate;
            User = user;
        }
    }
}
