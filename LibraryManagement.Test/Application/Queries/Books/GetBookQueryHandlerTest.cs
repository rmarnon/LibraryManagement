using FluentAssertions;
using LibraryManagement.Application.Queries.Books;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Test.Fixtures;
using Moq;

namespace LibraryManagement.Test.Application.Queries.Books
{
    public class GetBookQueryHandlerTest
    {
        private readonly Mock<IBookRepository> _BookRepositoryMock = new();

        [Fact]
        public async Task Should_Return_Book_By_Id()
        {
            // Arrange
            var query = BookDataGenerator.GetBookQueryFake();
            var book = BookDataGenerator.GetBookFake();

            _BookRepositoryMock.Setup(x => x.GetOneAsync(It.IsAny<Guid>())).ReturnsAsync(book);

            var handler = new GetBookQueryHandler(_BookRepositoryMock.Object);

            // Act
            var result = await handler.Handle(query, new());

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Title.Should().Be(book.Title);
            result.Value.Author.Should().Be(book.Author);
            result.Value.Isbn.Should().Be(book.Isbn);
            result.Value.PublicationYear.Should().Be((ushort)book.PublicationYear);
        }

        [Fact]
        public async Task Should_Cant_Find_Book()
        {
            // Arrange
            var query = BookDataGenerator.GetBookQueryFake();

            _BookRepositoryMock.Setup(x => x.GetOneAsync(It.IsAny<Guid>())).ReturnsAsync((Book)null!);

            var handler = new GetBookQueryHandler(_BookRepositoryMock.Object);

            // Act
            var result = await handler.Handle(query, new());

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().HaveCount(1)
                .And.ContainSingle(x => x.Message.Equals("Book not found"));
        }
    }
}
