using FluentAssertions;
using LibraryManagement.Application.Commands.Users;
using LibraryManagement.Application.Validators.Users;

namespace LibraryManagement.Test.Application.Validators.Users
{
    public class UpdateUserCommandValidatorTest
    {
        [Theory]
        [MemberData(nameof(ValidationCasesUpdate))]
        public void Should_Validate_Update_Users_Command_Validators_Test(UpdateUserCommand command, List<string> expectedNotifications, bool isValid)
        {
            // Arrange
            var validator = new UpdateUserCommandValidator();

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
                    new UpdateUserCommand { Email = "email@gmail.com", Name = "Name", Password = "P@assword0123" },
                    new List<string> { },
                    true
                ],
                [
                    new UpdateUserCommand { Email = string.Empty, Name = string.Empty, Password = string.Empty },
                    new List<string> { "Name must not to be null", "Email must not to be null", "Password is required" },
                    false
                ],
                [
                    new UpdateUserCommand { Email = "InvalidEmail", Name = "N", Password = "P@assword0123" },
                    new List<string> { "Name must be between 2 and 50 characters", "Invalid email adress" },
                    false
                ],
                [
                    new UpdateUserCommand { Email = "a@g.c", Name = "Name", Password = "P@InvalidPassword0123" },
                    new List<string> { "Email must be between 6 and 50 characters", "Password must be between 8 and 16 characters long and include at least one uppercase letter, one lowercase letter, one number, and one special character." },
                    false
                ],
            ];
        }
    }
}
