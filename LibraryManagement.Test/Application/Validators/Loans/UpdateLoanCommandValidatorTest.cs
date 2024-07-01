using FluentAssertions;
using LibraryManagement.Application.Commands.Loans;
using LibraryManagement.Application.Validators.Loans;

namespace LibraryManagement.Test.Application.Validators.Loans
{
    public class UpdateLoanCommandValidatorTest
    {
        [Theory]
        [MemberData(nameof(ValidationCasesUpdate))]
        public void Should_Validate_Update_Loans_Command_Validators_Test(UpdateLoanCommand command, List<string> expectedNotifications, bool isValid)
        {
            // Arrange
            var validator = new UpdateLoanCommandValidator();

            // Act
            var result = validator.Validate(command);

            // Assert
            result.IsValid.Should().Be(isValid);
            result.Errors.Should().HaveCount(expectedNotifications.Count);
            result.Errors.ForEach(x => expectedNotifications.Should().Contain(x.ErrorMessage));
        }

        public static IEnumerable<object[]> ValidationCasesUpdate()
        {
            return
            [
                [
                    new UpdateLoanCommand { Id = Guid.NewGuid(), LoanDate = DateTime.UtcNow, DevolutionDate = DateTime.Today.AddDays(7), BookIds = [Guid.NewGuid()] },
                    new List<string> { },
                    true
                ],
                [
                    new UpdateLoanCommand { Id = Guid.Empty, LoanDate = DateTime.UtcNow.AddDays(-1), DevolutionDate = DateTime.UtcNow.AddDays(-5), BookIds = [] },
                    new List<string> { "Loan is required!", "Devolution date should be after loan date", "There must be at least one book to loan", "Loan date should not be a future date" },
                    false
                ],
                [
                    new UpdateLoanCommand { Id = Guid.NewGuid(), LoanDate = DateTime.Today, DevolutionDate = DateTime.Today.AddMinutes(1), BookIds = [Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()] },
                    new List<string> { "A maximum of 3 books are allowed per loan" },
                    false
                ]
            ];
        }
    }
}
