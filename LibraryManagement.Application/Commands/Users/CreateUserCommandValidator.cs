using FluentValidation;

namespace LibraryManagement.Application.Commands.Users
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
                .WithMessage("Name must be between 2 and 50 characters");
        }

        private void ValidateEmail()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email must not to be null");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Invalid email adress");

            RuleFor(x => x.Email)
                .Length(3, 50)
                .WithMessage("Email must be between 3 and 50 characters");
        }
    }
}
