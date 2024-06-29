using FluentAssertions;
using LibraryManagement.Application.Queries.Loans;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Test.Fixtures;
using Moq;

namespace LibraryManagement.Test.Application.Queries.Loans
{
    public class GetLoanQueryHandlerTest
    {
        private readonly Mock<ILoanRepository> _LoanRepositoryMock = new();

        [Fact]
        public async Task Should_Return_Loan_By_Id()
        {
            // Arrange
            var query = LoanDataGenerator.GetLoanQueryFake();
            var loan = LoanDataGenerator.GetLoanFake();

            loan.BorrowedBooks.AddRange(
            [
                new() {
                    Book = BookDataGenerator.GetBookFake(),
                    Loan = LoanDataGenerator.GetLoanFake()
                },
                new() {
                    Book = BookDataGenerator.GetBookFake(),
                    Loan = LoanDataGenerator.GetLoanFake()
                },
                new() {
                    Book = BookDataGenerator.GetBookFake(),
                    Loan = LoanDataGenerator.GetLoanFake()
                }
            ]);

            _LoanRepositoryMock.Setup(x => x.GetOneAsync(It.IsAny<Guid>())).ReturnsAsync(loan);

            var handler = new GetLoanQueryHandler(_LoanRepositoryMock.Object);

            // Act
            var result = await handler.Handle(query, new());

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.TotalLoanBooks.Should().Be(3);
        }

        [Fact]
        public async Task Should_Cant_Find_Loan()
        {
            // Arrange
            var query = LoanDataGenerator.GetLoanQueryFake();

            _LoanRepositoryMock.Setup(x => x.GetOneAsync(It.IsAny<Guid>())).ReturnsAsync((Loan)null!);

            var handler = new GetLoanQueryHandler(_LoanRepositoryMock.Object);

            // Act
            var result = await handler.Handle(query, new());

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().HaveCount(1)
                .And.ContainSingle(x => x.Message.Equals("Loan not found"));
        }
    }
}
