using FluentResults;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Commands.Users
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetOneAsync(request.Id);
            if (user is null)
                return Result.Fail("User not found");

            await _userRepository.InactivateAsync(request.Id);
            return Result.Ok();
        }
    }
}
