using FluentAssertions;
using LibraryManagement.Application.Commands.Loans;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Test.Fixtures;
using Moq;

namespace LibraryManagement.Test.Application.Commands.Loans
{
    public class CreateLoanCommandHandlerTest
    {
        private readonly Mock<ILoanRepository> _loanRepositoryMock = new();
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<IBookRepository> _bookRepositoryMock = new();

        [Fact]
        public async Task Should_Create_Loan_with_Success()
        {
            // Arrange
            var command = LoanDataGenerator.CreateLoanCommandFake();
            var books = new List<Book> { BookDataGenerator.GetBookFake() };
            command.BookIds = books.Select(x => x.Id).ToList();

            _loanRepositoryMock.Setup(x => x.ExistsLoanByUserIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(false);

            _userRepositoryMock.Setup(m => m.ExistsAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            _bookRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(books);

            var handler = new CreateLoanCommandHandler(
                _loanRepositoryMock.Object,
                _userRepositoryMock.Object,
                _bookRepositoryMock.Object);

            // Act
            var result = await handler.Handle(command, new());

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Should_Fail_To_Create_Loan_with_Loan_Exist()
        {
            // Arrange
            var command = LoanDataGenerator.CreateLoanCommandFake();

            _loanRepositoryMock.Setup(x => x.ExistsLoanByUserIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            _userRepositoryMock.Setup(m => m.ExistsAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            var handler = new CreateLoanCommandHandler(
                _loanRepositoryMock.Object,
                _userRepositoryMock.Object,
                _bookRepositoryMock.Object);

            // Act
            var result = await handler.Handle(command, new());

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().HaveCount(1)
                .And.ContainSingle(x => x.Message.Equals("User or loan can't be found"));
        }
    }
}
