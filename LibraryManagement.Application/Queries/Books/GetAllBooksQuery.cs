using LibraryManagement.Application.ViewModels;
using MediatR;

namespace LibraryManagement.Application.Queries.Books
{
    public record GetAllBooksQuery : IRequest<List<BookViewModel>>
    {
        public string Query { get; private set; }

        public GetAllBooksQuery(string query) => Query = query;
    }
}
