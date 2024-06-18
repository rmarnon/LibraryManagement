using FluentResults;
using LibraryManagement.Application.ViewModels;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Commands.Users
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserViewModel>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public CreateUserCommandHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<Result<UserViewModel>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var exist = await _userRepository.CheckEmailExsistsAsync(command.Email);
            if (!exist)
            {
                var password = _authService.GenerateSha256Hash(command.Password);
                var user = new User(command.Name, command.Email, password, command.Role);
                await _userRepository.AddAsync(user);

                return Result.Ok(new UserViewModel(user.Name, user.Email));
            }

            return Result.Fail<UserViewModel>("Email alredy exists");
        }
    }
}
