using FluentResults;
using LibraryManagement.Application.ViewModels;
using MediatR;

namespace LibraryManagement.Application.Queries.Books
{
    public record GetBookQuery(Guid Id) : IRequest<Result<BookViewModel>>
    {
        public Guid Id { get; private set; } = Id;
    }
}
