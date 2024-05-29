using MediatR;

namespace LibraryManagement.Application.Commands.Users
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }

        public DeleteUserCommand(Guid id) => Id = id;
    }
}
