using FluentAssertions;
using LibraryManagement.Application.Commands.Books;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Test.Fixtures;
using Moq;

namespace LibraryManagement.Test.Application.Commands.Books
{
    public class DeleteBookCommandHandlerTest
    {
        private readonly Mock<IBookRepository> _BookepositoryMock = new();

        [Fact]
        public async Task Should_Delete_Book_with_Success()
        {
            // Arrange
            var book = BookDataGenerator.GetBookFake();

            var command = BookDataGenerator.DeleteBookCommandFake();

            _BookepositoryMock.Setup(m => m.GetOneAsync(It.IsAny<Guid>()))
                .ReturnsAsync(book);

            var handler = new DeleteBookCommandHandler(_BookepositoryMock.Object);

            // Act
            var result = await handler.Handle(command, new());

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Should_Fail_To_Delete_Invalid_Book()
        {
            // Arrange
            var command = BookDataGenerator.DeleteBookCommandFake();

            _BookepositoryMock.Setup(m => m.GetOneAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Book)null!);

            var handler = new DeleteBookCommandHandler(_BookepositoryMock.Object);

            // Act
            var result = await handler.Handle(command, new());

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().HaveCount(1)
                .And.ContainSingle(x => x.Message.Equals("Book not found"));
        }
    }
}
