namespace LibraryManagement.Core.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public List<Loan> Loans { get; private set; } = [];
    }
}
