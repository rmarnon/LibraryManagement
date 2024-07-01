using FluentAssertions;
using LibraryManagement.Application.Commands.Loans;
using LibraryManagement.Application.Validators.Loans;

namespace LibraryManagement.Test.Application.Validators.Loans
{
    public class ReturnLoanCommandValidatorTest
    {
        [Theory]
        [MemberData(nameof(ValidationCasesReturn))]
        public void Should_Validate_Return_Loans_Command_Validators_Test(ReturnLoanCommand command, List<string> expectedNotifications, bool isValid)
        {
            // Arrange
            var validator = new ReturnLoanCommandValidator();

            // Act
            var result = validator.Validate(command);

            // Assert
            result.IsValid.Should().Be(isValid);
            result.Errors.Should().HaveCount(expectedNotifications.Count);
            result.Errors.ForEach(x => expectedNotifications.Should().Contain(x.ErrorMessage));
        }

        public static IEnumerable<object[]> ValidationCasesReturn()
        {
            return
            [
                [
                    new ReturnLoanCommand { UserId = Guid.NewGuid(), DevolutionDate = DateTime.Today, BookIds = [Guid.NewGuid()] },
                    new List<string> { },
                    true
                ],
                [
                    new ReturnLoanCommand { UserId = Guid.Empty, DevolutionDate = DateTime.UtcNow.AddDays(1), BookIds = [] },
                    new List<string> { "User is required!", "Devolution date should not be a future date", "There must be at least one book to give back" },
                    false
                ]
            ];
        }
    }
}
