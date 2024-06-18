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
            await _userRepository.InactivateAsync(request.Id);
            return Result.Ok();
        }
    }
}
