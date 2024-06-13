namespace LibraryManagement.Application.ViewModels
{
    public record LoginUserViewModel
    {
        public string Email { get; private init; }
        public string Token { get; private init; }

        public LoginUserViewModel(string email, string token)
        {
            Email = email;
            Token = token;
        }
    }
}
