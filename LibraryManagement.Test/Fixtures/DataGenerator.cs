using LibraryManagement.Application.Commands.Books;
using LibraryManagement.Application.Commands.Loans;
using LibraryManagement.Application.Commands.Users;
using LibraryManagement.Application.ViewModels;
using LibraryManagement.Core.Entities;

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

        internal static Book GetBookFake()
        {
            return new("Title", "Author", "Isbn", 1994);
        }

        internal static CreateUserCommand CreateUserCommandFake()
        {
            return new()
            {
                Name = "Name",
                Email = "Email",
                Password = "P@ssword0123",
                Role = Core.Enums.Role.User
            };
        }

        internal static CreateBookCommand CreateBookCommandFake()
        {
            return new()
            {
                Author = "Author",
                Isbn = "Isbn",
                Title = "title",
                PublicationYear = 1979
            };
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
