using FluentValidation;
using LibraryManagement.Application.Commands.Users;

namespace LibraryManagement.Application.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            ValidateName();
            ValidateEmail();
        }

        private void ValidateName()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name must not to be null");

            RuleFor(x => x.Name)
                .Length(2, 50)
                .When(x => !string.IsNullOrWhiteSpace(x.Name))
                .WithMessage("Name must be between 2 and 50 characters");
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
    }
}
