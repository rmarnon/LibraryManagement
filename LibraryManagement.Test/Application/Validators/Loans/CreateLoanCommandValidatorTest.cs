using FluentAssertions;
using LibraryManagement.Application.Commands.Loans;
using LibraryManagement.Application.Validators.Loans;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Test.Application.Validators.Loans
{
    public class CreateLoanCommandValidatorTest
    {
        [Theory]
        [MemberData(nameof(ValidationCasesCreate))]
        public void Should_Validate_Create_Loans_Command_Validators_Test(CreateLoanCommand command, List<string> expectedNotifications, bool isValid)
        {
            // Arrange
            var validator = new CreateLoanCommandValidator();

            // Act
            var result = validator.Validate(command);

            // Assert
            result.IsValid.Should().Be(isValid);
            result.Errors.Should().HaveCount(expectedNotifications.Count);
            result.Errors.ForEach(x => expectedNotifications.Should().Contain(x.ErrorMessage));
        }

        public static IEnumerable<object[]> ValidationCasesCreate()
        {
            return
            [
                [
                    new CreateLoanCommand { UserId = Guid.NewGuid(), LoanDate = DateTime.Today, BookIds = [Guid.NewGuid()] },
                    new List<string> { },
                    true
                ],
                [
                    new CreateLoanCommand { UserId = Guid.Empty, LoanDate = DateTime.Today.AddDays(1), BookIds = [] },
                    new List<string> { "User is required!", "There must be at least one book to loan", "Loan date should not be a future date"},
                    false
                ],
                [
                    new CreateLoanCommand { UserId = Guid.NewGuid(), LoanDate = DateTime.Today, BookIds = [Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()] },
                    new List<string> { "A maximum of 3 books are allowed per loan" },
                    false
                ]
            ];
        }
    }
}
