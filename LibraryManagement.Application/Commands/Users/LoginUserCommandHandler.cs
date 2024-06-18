using FluentResults;
using LibraryManagement.Application.ViewModels;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Commands.Users
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<LoginUserViewModel>>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public LoginUserCommandHandler(IUserRepository userRepository, IAuthService authService)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<Result<LoginUserViewModel>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _authService.GenerateSha256Hash(request.Password);
            var user = await _userRepository.GetUserByEmailAndPasswordHashAsync(request.Email, passwordHash);

            if (user is null) 
                return Result.Fail<LoginUserViewModel>("User email not found");

            var token = _authService.GenerateJwtToken(user.Email, user.Role);

            return Result.Ok(new LoginUserViewModel(user.Email, token));
        }
    }
}
