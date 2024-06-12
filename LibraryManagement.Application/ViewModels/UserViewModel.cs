namespace LibraryManagement.Application.ViewModels
{
    public record UserViewModel
    {
        public string Name { get; private init; }
        public string Email { get; private init; }

        public UserViewModel(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
