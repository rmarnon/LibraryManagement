using MediatR;

namespace LibraryManagement.Application.Commands.Users
{
    public class CreateUserCommand : IRequest
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
    }
}
