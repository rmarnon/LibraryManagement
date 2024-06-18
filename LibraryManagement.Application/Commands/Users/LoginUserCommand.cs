using FluentResults;
using LibraryManagement.Application.ViewModels;
using MediatR;

namespace LibraryManagement.Application.Commands.Users
{
    public class LoginUserCommand : IRequest<Result<LoginUserViewModel>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
