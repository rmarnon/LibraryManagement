using FluentAssertions;
using LibraryManagement.Application.Commands.Books;
using LibraryManagement.Application.Validators.Books;

namespace LibraryManagement.Test.Application.Validators.Books
{
    public class UpdateBookCommandValidatorTest
    {
        [Theory]
        [MemberData(nameof(ValidationCasesUpdate))]
        public void Should_Validate_Update_Books_Command_Validators_Test(UpdateBookCommand command, List<string> expectedNotifications, bool isValid)
        {
            // Arrange
            var validator = new UpdateBookCommandValidator();

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
                    new UpdateBookCommand { Title = "Title", Author = "Author", Isbn = "ISBN000010", PublicationYear = 2001 },
                    new List<string> { },
                    true
                ],
                [
                    new UpdateBookCommand { Title = string.Empty, Author = string.Empty, Isbn = string.Empty, PublicationYear = -2024 },
                    new List<string> { "Title must not to be null", "Author must not to be null", "ISBN must not to be null", "Publication year must not to be negative number" },
                    false
                ],
                [
                    new UpdateBookCommand { Title = "Ti", Author = "A", Isbn = "ISBN000000013", PublicationYear = 2001 },
                    new List<string> { "Title must be between 3 and 100 characters", "Title must be between 2 and 50 characters", "Until 2007, the ISBN code must have 10 digits" },
                    false
                ],
                [
                    new UpdateBookCommand { Title = "Title", Author = "Author", Isbn = "ISBN000010", PublicationYear = 2050 },
                    new List<string> { "Publication year should not be a future date", "After 2007 the isbn code must have 13 digits" },
                    false
                ],
            ];
        }
    }
}
