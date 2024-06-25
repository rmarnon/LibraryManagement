using FluentResults;
using LibraryManagement.Application.ViewModels;
using LibraryManagement.Core.Entities;
using MediatR;

namespace LibraryManagement.Application.Queries.Loans
{
    public class GetAllLoansQuery : IRequest<Result<List<LoanViewModel>>>
    {
        public string Query { get; private set; }
        public PaginationInput Pagination { get; private set; }

        public GetAllLoansQuery(string query, PaginationInput pagination)
        {
            Query = query;
            Pagination = pagination;
        }
    }
}
