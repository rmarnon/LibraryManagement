namespace LibraryManagement.Application.ViewModels
{
    public record LoanViewModel
    {
        public DateTime LoanDate { get; private init; }
        public DateTime DevolutionDate { get; private init; }
        public UserViewModel User { get; private init; }
        public List<BookViewModel> Books { get; private init; } = [];
        public sbyte TotalLoanBooks => (sbyte)Books.Count;
        public string Situation => GetSituation();

        public LoanViewModel(DateTime loanDate, DateTime devolutionDate, UserViewModel user)
        {
            LoanDate = loanDate;
            DevolutionDate = devolutionDate;
            User = user;
        }

        private string GetSituation()
        {
            if (DevolutionDate >= DateTime.Today)
                return "Books up to date";

            var differenceInDays = DateTime.Today.Subtract(DevolutionDate);
            return $"Loan is {differenceInDays.TotalDays} days late!";
        }
    }
}
