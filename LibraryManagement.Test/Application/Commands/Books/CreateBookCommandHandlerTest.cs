using FluentAssertions;
using LibraryManagement.Application.Commands.Books;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Test.Fixtures;
using Moq;

namespace LibraryManagement.Test.Application.Commands.Books
{
    public class CreateBookCommandHandlerTest
    {
        private readonly Mock<IBookRepository> _BookepositoryMock = new();

        [Fact]
        public async Task Should_Create_Book_with_Success()
        {
            // Arrange
            var command = DataGenerator.CreateBookCommandFake();

            var handler = new CreateBookCommandHandler(_BookepositoryMock.Object);

            // Act
            var result = await handler.Handle(command, new());

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Author.Should().Be(command.Author);
            result.Value.Isbn.Should().Be(command.Isbn);
            result.Value.Title.Should().Be(command.Title);
            result.Value.PublicationYear.Should().Be((ushort)command.PublicationYear);
        }
    }
}
