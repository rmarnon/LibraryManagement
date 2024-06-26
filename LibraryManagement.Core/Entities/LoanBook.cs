namespace LibraryManagement.Core.Entities
{
    public class LoanBook
    {
        public Guid BookId { get; private set; }
        public Book Book { get; set; }
        public Guid LoanId { get; private set; }
        public Loan Loan { get; set; }

        public LoanBook()
        {
        }

        public LoanBook(Guid bookId, Guid loanId)
        {
            BookId = bookId;
            LoanId = loanId;
        }
    }
}
