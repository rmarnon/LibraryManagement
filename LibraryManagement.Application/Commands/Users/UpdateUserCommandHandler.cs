using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Commands.Users
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetOneAsync(request.Id);

            if (user != null)
            {
                user.Update(request.Name, request.Email, request.IsDeleted);
                await _userRepository.UpdateAsync(user);
            }

            return Unit.Value;
        }
    }
}
