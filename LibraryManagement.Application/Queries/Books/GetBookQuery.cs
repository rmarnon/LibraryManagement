using LibraryManagement.Application.ViewModels;
using MediatR;

namespace LibraryManagement.Application.Queries.Books
{
    public record GetBookQuery(Guid Id) : IRequest<BookViewModel>
    {
        public Guid Id { get; private set; } = Id;
    }
}
