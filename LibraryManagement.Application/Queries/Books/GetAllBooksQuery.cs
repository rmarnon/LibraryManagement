using FluentResults;
using LibraryManagement.Application.ViewModels;
using LibraryManagement.Core.Entities;
using MediatR;

namespace LibraryManagement.Application.Queries.Books
{
    public record GetAllBooksQuery : IRequest<Result<List<BookViewModel>>>
    {
        public string Query { get; private set; }
        public PaginationInput Pagination { get; private set; }

        public GetAllBooksQuery(string query, PaginationInput pagination)
        {
            Query = query;
            Pagination = pagination;
        }
    }
}
