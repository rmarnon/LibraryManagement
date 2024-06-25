using FluentResults;
using LibraryManagement.Application.ViewModels;
using LibraryManagement.Core.Entities;
using MediatR;

namespace LibraryManagement.Application.Queries.Users
{
    public record GetAllUsersQuery : IRequest<Result<List<UserViewModel>>>
    {
        public string Query { get; private set; }
        public PaginationInput Pagination { get; private set; }

        public GetAllUsersQuery(string query, PaginationInput pagination)
        {
            Query = query;
            Pagination = pagination;
        }
    }
}
