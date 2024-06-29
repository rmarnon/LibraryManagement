using FluentAssertions;
using LibraryManagement.Application.Commands.Users;
using LibraryManagement.Application.Validators.Users;

namespace LibraryManagement.Test.Application.Validators.Users
{
    public class LoginUserCommandValidatorTest
    {
        [Theory]
        [MemberData(nameof(ValidationCasesLogin))]
        public void Should_Validate_Login_Users_Command_Validators_Test(LoginUserCommand command, List<string> expectedNotifications, bool isValid)
        {
            // Arrange
            var validator = new LoginUserCommandValidator();

            // Act
            var result = validator.Validate(command);

            // Assert
            result.IsValid.Should().Be(isValid);
            result.Errors.Should().HaveCount(expectedNotifications.Count);
            result.Errors.ForEach(x => expectedNotifications.Should().Contain(x.ErrorMessage));
        }

        public static IEnumerable<object[]> ValidationCasesLogin()
        {
            return
            [
                [
                    new LoginUserCommand { Email = "email@gmail.com", Password = "P@assword0123" },
                    new List<string> { },
                    true
                ],
                [
                    new LoginUserCommand { Email = string.Empty, Password = string.Empty },
                    new List<string> { "Email must not to be null", "Password is required" },
                    false
                ],
                [
                    new LoginUserCommand { Email = "InvalidEmail", Password = "P@assword0123" },
                    new List<string> { "Invalid email adress" },
                    false
                ],
                [
                    new LoginUserCommand { Email = "a@g.c", Password = "InvalidPassword0123" },
                    new List<string> { "Email must be between 6 and 50 characters", "Password must be between 8 and 16 characters long and include at least one uppercase letter, one lowercase letter, one number, and one special character." },
                    false
                ],
            ];
        }
    }
}
