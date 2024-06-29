using LibraryManagement.Application.Commands.Loans;
using LibraryManagement.Application.Queries.Loans;
using LibraryManagement.Application.ViewModels;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Test.Fixtures
{
    internal static class LoanDataGenerator
    {
        public static DateTime Data = DateTime.UtcNow;

        internal static LoanViewModel GetLoanViewModel()
        {
            return new(Data, Data, UserDataGenerator.GetUserViewModel());
        }

        internal static CreateLoanCommand CreateLoanCommandFake()
        {
            return new()
            {
                LoanDate = Data,
                UserId = Guid.NewGuid(),
                BookIds = [Guid.NewGuid()]
            };
        }

        internal static UpdateLoanCommand UpdateLoanCommandFake()
        {
            return new()
            {
                LoanDate = Data,
                DevolutionDate = Data,
                BookIds = [Guid.NewGuid()]
            };
        }

        internal static ReturnLoanCommand ReturnLoanCommandFake()
        {
            return new()
            {
                DevolutionDate = Data,
                UserId = Guid.NewGuid(),
                BookIds = [Guid.NewGuid()]
            };
        }

        internal static Loan GetLoanFake()
        {
            return new Loan { User = UserDataGenerator.GetUserFake() };
        }

        internal static GetAllLoansQuery GetAllLoansQueryFake()
        {
            return new("query", new());
        }

        internal static GetLoanQuery GetLoanQueryFake()
        {
            return new(Guid.NewGuid());
        }
    }
}
