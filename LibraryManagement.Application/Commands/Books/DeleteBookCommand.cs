using FluentResults;
using MediatR;

namespace LibraryManagement.Application.Commands.Books
{
    public class DeleteBookCommand : IRequest<Result>
    {
        public Guid Id { get; set; }

        public DeleteBookCommand(Guid id) => Id = id;
    }
}
