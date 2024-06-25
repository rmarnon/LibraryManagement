using FluentAssertions;
using FluentResults;
using LibraryManagement.API.Controllers;
using LibraryManagement.Application.Commands.Books;
using LibraryManagement.Application.Queries.Books;
using LibraryManagement.Application.ViewModels;
using LibraryManagement.Test.Fixtures;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LibraryManagement.Test.API.Controllers
{
    public class BookControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock = new();

        [Fact]
        public async Task Should_Create_Book_With_Success()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateBookCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok());

            var controller = new BookController(_mediatorMock.Object);

            // Action
            var result = await controller.CreateBook(new());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Fact]
        public async Task Should_Fail_To_Create_Invalid_Book()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateBookCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Fail("Fail"));

            var controller = new BookController(_mediatorMock.Object);

            // Action
            var result = await controller.CreateBook(new());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Should_Updte_Book_With_Success()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateBookCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok());

            var controller = new BookController(_mediatorMock.Object);

            // Action
            var result = await controller.UpdateBook(new());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Should_Fail_To_Updte_Invalid_Book()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateBookCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Fail("Fail"));

            var controller = new BookController(_mediatorMock.Object);

            // Action
            var result = await controller.UpdateBook(new());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Should_Return_Book_By_Id()
        {
            // Arrange
            var Book = DataGenerator.GetBookViewModel();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBookQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok(Book));

            var controller = new BookController(_mediatorMock.Object);

            // Action
            var result = await controller.GetBookById(Guid.NewGuid());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Should_Return_All_Books()
        {
            // Arrange
            var Books = new List<BookViewModel> { DataGenerator.GetBookViewModel() };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllBooksQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok(Books));

            var controller = new BookController(_mediatorMock.Object);

            // Action
            var result = await controller.GetAllBooks(string.Empty, new());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Should_Delete_Book()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteBookCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok());

            var controller = new BookController(_mediatorMock.Object);

            // Action
            var result = await controller.DeleteBook(Guid.NewGuid());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
