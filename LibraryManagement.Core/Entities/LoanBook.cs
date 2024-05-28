namespace LibraryManagement.Core.Entities
{
    public class LoanBook
    {
        public Guid BookId { get; private set; }
        public Book Book { get; private set; }
        public Guid LoanId { get; private set; }
        public Loan Loan { get; private set; }
    }
}
