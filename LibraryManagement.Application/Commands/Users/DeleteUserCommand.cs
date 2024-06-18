using FluentResults;
using MediatR;

namespace LibraryManagement.Application.Commands.Users
{
    public class DeleteUserCommand : IRequest<Result>
    {
        public Guid Id { get; set; }

        public DeleteUserCommand(Guid id) => Id = id;
    }
}
