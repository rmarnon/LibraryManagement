namespace LibraryManagement.Core.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public List<Loan> Loans { get; private set; } = [];

        public User(string name, string email)
        {
            Name = name.Trim();
            Email = email.Trim();
        }

        public void Update(string name, string email, bool isDeleted)
        {
            Name = name.Trim();
            Email = email.Trim();
            IsDeleted = isDeleted;
        }
    }
}
