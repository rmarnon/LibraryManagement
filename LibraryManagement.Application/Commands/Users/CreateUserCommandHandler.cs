using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Commands.Users
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<Unit> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var exist = await _userRepository.CheckEmailExsistsAsync(command.Email);
            if (!exist)
            {
                var user = new User(command.Name, command.Email);
                await _userRepository.AddAsync(user);
            }

            return Unit.Value;
        }
    }
}
