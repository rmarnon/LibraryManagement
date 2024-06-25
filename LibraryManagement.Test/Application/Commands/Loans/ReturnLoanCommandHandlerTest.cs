using FluentAssertions;
using LibraryManagement.Application.Commands.Loans;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Test.Fixtures;
using Moq;

namespace LibraryManagement.Test.Application.Commands.Loans
{
    public class ReturnLoanCommandHandlerTest
    {
        private readonly Mock<ILoanRepository> _LoanepositoryMock = new();
        private readonly Mock<IBookRepository> _bookRepositoryMock = new();

        [Fact]
        public async Task Should_Return_Loan_with_Success()
        {
            // Arrange
            var id = Guid.NewGuid();
            var loan = LoanDataGenerator.GetLoanFake();
            loan.BorrowedBooks.Add(new LoanBook(id, id));

            var command = LoanDataGenerator.ReturnLoanCommandFake();
            command.BookIds.Add(id);

            _LoanepositoryMock.Setup(m => m.GetLoanByUserIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(loan);

            var handler = new ReturnLoanCommandHandler(_LoanepositoryMock.Object);

            // Act
            var result = await handler.Handle(command, new());

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Should_Fail_To_Return_Loan_Whitout_Loan_Books()
        {
            // Arrange
            var command = LoanDataGenerator.ReturnLoanCommandFake();

            _LoanepositoryMock.Setup(m => m.GetOneAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Loan)null!);

            var handler = new ReturnLoanCommandHandler(_LoanepositoryMock.Object);

            // Act
            var result = await handler.Handle(command, new());

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().HaveCount(1)
                .And.ContainSingle(x => x.Message.Equals("Returned books differ from borrowed books"));
        }
    }
}
