using FluentAssertions;
using LibraryManagement.Application.Commands.Loans;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Test.Fixtures;
using Moq;

namespace LibraryManagement.Test.Application.Commands.Loans
{
    public class UpdateLoanCommandHandlerTest
    {
        private readonly Mock<ILoanRepository> _LoanepositoryMock = new();
        private readonly Mock<IBookRepository> _bookRepositoryMock = new();

        [Fact]
        public async Task Should_Update_Loan_with_Success()
        {
            // Arrange
            var loan = LoanDataGenerator.GetLoanFake();
            var books = new List<Book> { BookDataGenerator.GetBookFake() };
            var command = LoanDataGenerator.UpdateLoanCommandFake();
            command.BookIds = books.Select(x => x.Id).ToList();

            _LoanepositoryMock.Setup(m => m.GetOneAsync(It.IsAny<Guid>()))
                .ReturnsAsync(loan);

            _bookRepositoryMock.Setup(m => m.GetAllAsync())
                .ReturnsAsync(books);

            var handler = new UpdateLoanCommandHandler(_LoanepositoryMock.Object, _bookRepositoryMock.Object);

            // Act
            var result = await handler.Handle(command, new());

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Should_Fail_To_Update_Invalid_Loan()
        {
            // Arrange
            var command = LoanDataGenerator.UpdateLoanCommandFake();

            _LoanepositoryMock.Setup(m => m.GetOneAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Loan)null!);

            var handler = new UpdateLoanCommandHandler(_LoanepositoryMock.Object, _bookRepositoryMock.Object);

            // Act
            var result = await handler.Handle(command, new());

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().HaveCount(1)
                .And.ContainSingle(x => x.Message.Equals("Laon can't be updated"));
        }
    }
}
