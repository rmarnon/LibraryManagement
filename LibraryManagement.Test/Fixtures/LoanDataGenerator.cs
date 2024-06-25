using LibraryManagement.Application.Commands.Loans;
using LibraryManagement.Application.ViewModels;

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
    }
}
