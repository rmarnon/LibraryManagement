using FluentAssertions;
using LibraryManagement.Application.Queries.Books;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Test.Fixtures;
using Moq;

namespace LibraryManagement.Test.Application.Queries.Books
{
    public class GetAllBooksQueryHandlerTest
    {
        private readonly Mock<IBookRepository> _BookRepositoryMock = new();

        [Fact]
        public async Task Should_Return_All_Books()
        {
            // Arrange
            var query = BookDataGenerator.GetAllBooksQueryFake();
            var Book = BookDataGenerator.GetBookFake();
            var Books = new List<Book> { Book, Book };

            _BookRepositoryMock.Setup(x => x.GetAllAsync(query.Pagination)).ReturnsAsync(Books);

            var handler = new GetAllBooksQueryHandler(_BookRepositoryMock.Object);

            // Act
            var result = await handler.Handle(query, new());

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().HaveCount(2);
        }

        [Fact]
        public async Task Should_Return_Empty_Books_List()
        {
            // Arrange
            var query = BookDataGenerator.GetAllBooksQueryFake();

            _BookRepositoryMock.Setup(x => x.GetAllAsync(query.Pagination)).ReturnsAsync((List<Book>)null!);

            var handler = new GetAllBooksQueryHandler(_BookRepositoryMock.Object);

            // Act
            var result = await handler.Handle(query, new());

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().HaveCount(1)
                .And.ContainSingle(x => x.Message.Equals("Empty book list"));
        }
    }
}
