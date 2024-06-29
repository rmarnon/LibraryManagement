using LibraryManagement.Application.Commands.Books;
using LibraryManagement.Application.Queries.Books;
using LibraryManagement.Application.Queries.Users;
using LibraryManagement.Application.ViewModels;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Test.Fixtures
{
    internal static class BookDataGenerator
    {
        internal static BookViewModel GetBookViewModel()
        {
            return new("Title", "Author", "Isbn", 1982);
        }

        internal static Book GetBookFake()
        {
            return new("Title", "Author", "Isbn", 1994);
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

        internal static UpdateBookCommand UpdateBookCommandFake()
        {
            return new()
            {
                Author = "Author",
                Isbn = "Isbn",
                Title = "title",
                PublicationYear = 1979
            };
        }

        internal static DeleteBookCommand DeleteBookCommandFake()
        {
            return new(Guid.NewGuid());
        }

        internal static GetAllBooksQuery GetAllBooksQueryFake()
        {
            return new("query", new());
        }

        internal static GetBookQuery GetBookQueryFake()
        {
            return new(Guid.NewGuid());
        }
    }
}
