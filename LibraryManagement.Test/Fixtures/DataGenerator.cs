using LibraryManagement.Application.ViewModels;

namespace LibraryManagement.Test.Fixtures
{
    internal static class DataGenerator
    {
        public static DateTime Data = DateTime.UtcNow;

        internal static UserViewModel GetUserViewModel()
        {
            return new("Name", "Email");
        }

        internal static LoginUserViewModel GetLoginUserViewModel()
        {
            return new("Email", "Token");
        }

        internal static BookViewModel GetBookViewModel()
        {
            return new("Title", "Author", "Isbn", 1982);
        }

        internal static LoanViewModel GetLoanViewModel()
        {
            return new(Data, Data, GetUserViewModel());
        }
    }
}
