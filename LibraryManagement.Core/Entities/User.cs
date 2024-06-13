using LibraryManagement.Core.Enums;

namespace LibraryManagement.Core.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public Role Role { get; private set; }
        public List<Loan> Loans { get; private set; } = [];

        public User(string name, string email, string password, Role role)
        {
            Name = name.Trim();
            Email = email.Trim();
            Password = password.Trim();
            Role = role;
        }

        public void Update(string name, string email, string password, Role role)
        {
            Name = name.Trim();
            Email = email.Trim();
            Password = password.Trim();
            Role = role;
        }
    }
}
