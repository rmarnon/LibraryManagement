using LibraryManagement.Application.ViewModels;
using MediatR;

namespace LibraryManagement.Application.Commands.Users
{
    public class LoginUserCommand : IRequest<LoginUserViewModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
