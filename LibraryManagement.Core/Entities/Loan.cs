namespace LibraryManagement.Core.Entities
{
    public class Loan : BaseEntity
    {
        public DateTime LoanDate { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public Guid BookId { get; private set; }
        public Book Book { get; private set; }
    }
}
