namespace LibraryManagement.Application.ViewModels
{
    public record UserViewModel
    {
        public string Name { get; private init; }
        public string Email { get; private init; }
        public int TotalLoans { get; private set; }

        public UserViewModel(string name, string email, int totalLoans)
        {
            Name = name;
            Email = email;
            TotalLoans = totalLoans;
        }
    }
}
