using FluentResults;
using LibraryManagement.Application.ViewModels;
using MediatR;

namespace LibraryManagement.Application.Queries.Loans
{
    public class GetAllLoansQuery : IRequest<Result<List<LoanViewModel>>>
    {
        public string Query { get; private set; }

        public GetAllLoansQuery(string query) => Query = query;
    }
}
