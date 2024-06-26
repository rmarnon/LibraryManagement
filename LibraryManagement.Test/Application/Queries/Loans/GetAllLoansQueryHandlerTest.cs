using FluentAssertions;
using LibraryManagement.Application.Queries.Loans;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Test.Fixtures;
using Moq;

namespace LibraryManagement.Test.Application.Queries.Loans
{
    public class GetAllLoansQueryHandlerTest
    {
        private readonly Mock<ILoanRepository> _LoanRepositoryMock = new();

        [Fact]
        public async Task Should_Return_All_Loans()
        {
            // Arrange
            var query = LoanDataGenerator.GetAllLoansQueryFake();
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
                }
            ]);

            var loans = new List<Loan> { loan };

            _LoanRepositoryMock.Setup(x => x.GetAllAsync(query.Pagination)).ReturnsAsync(loans);

            var handler = new GetAllLoansQueryHandler(_LoanRepositoryMock.Object);

            // Act
            var result = await handler.Handle(query, new());

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().HaveCount(1).And.Contain(x => x.TotalLoanBooks == 2);
        }

        [Fact]
        public async Task Should_Return_Empty_Loans_List()
        {
            // Arrange
            var query = LoanDataGenerator.GetAllLoansQueryFake();

            _LoanRepositoryMock.Setup(x => x.GetAllAsync(query.Pagination)).ReturnsAsync((List<Loan>)null!);

            var handler = new GetAllLoansQueryHandler(_LoanRepositoryMock.Object);

            // Act
            var result = await handler.Handle(query, new());

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().HaveCount(1)
                .And.ContainSingle(x => x.Message.Equals("Empty loan list"));
        }
    }
}
