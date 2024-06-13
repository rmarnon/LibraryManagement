using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Commands.Users
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public UpdateUserCommandHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetOneAsync(request.Id);

            if (user != null)
            {
                var password = _authService.GenerateSha256Hash(request.Password);
                user.Update(request.Name, request.Email, password, request.Role);
                await _userRepository.UpdateAsync(user);
            }

            return Unit.Value;
        }
    }
}
