namespace LibraryManagement.Core.Entities
{
    public class Loan : BaseEntity
    {
        public DateTime LoanDate { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; set; }
        public virtual List<LoanBook> BorrowedBooks { get; private set; } = [];
        public DateTime DevolutionDate { get; private set; } = DateTime.Today.AddDays(7);

        public Loan()
        {            
        }

        public Loan(DateTime loanDate, Guid userId)
        {
            UserId = userId;
            LoanDate = loanDate;
        }

        public void Update(DateTime loanDate, DateTime devolutionDate, List<Guid> bookIds, Guid userId)
        {
            User = null;
            Id = Guid.NewGuid();
            LoanDate = loanDate;
            DevolutionDate = devolutionDate;
            BorrowedBooks.Clear();

            foreach (var bookId in bookIds)
            {
                BorrowedBooks.Add(new(bookId, Id));
            }
        }
    }
}
