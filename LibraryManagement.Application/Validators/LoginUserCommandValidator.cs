using FluentValidation;
using LibraryManagement.Application.Commands.Users;

namespace LibraryManagement.Application.Validators
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            ValidateEmail();
            ValidatePassword();
        }

        private void ValidateEmail()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email must not to be null");

            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrWhiteSpace(x.Email))
                .WithMessage("Invalid email adress");

            RuleFor(x => x.Email)
                .Length(3, 50)
                .When(x => !string.IsNullOrWhiteSpace(x.Email))
                .WithMessage("Email must be between 3 and 50 characters");
        }

        private void ValidatePassword()
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required");

            RuleFor(x => x.Password)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,16}$")
                .When(x => !string.IsNullOrWhiteSpace(x.Password))
                .WithMessage("Password must be between 8 and 16 characters long and include at least one uppercase letter, one lowercase letter, one number, and one special character.");
        }
    }
}
