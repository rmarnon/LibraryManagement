using FluentValidation;
using LibraryManagement.Application.Commands.Loans;

namespace LibraryManagement.Application.Validators.Loans
{
    public class ReturnLoanCommandValidator : AbstractValidator<ReturnLoanCommand>
    {
        public ReturnLoanCommandValidator()
        {
            ValidateUser();
            ValidateBooks();
            ValidateDevolutionDate();
        }

        private void ValidateUser()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User is required!");
        }

        private void ValidateBooks()
        {
            RuleFor(x => x.BookIds)
                .NotEmpty()
                .WithMessage("There must be at least one book to give back");
        }

        private void ValidateDevolutionDate()
        {
            RuleFor(x => x.DevolutionDate)
                .NotEmpty()
                .WithMessage("Devolution date is required!");

            RuleFor(x => x.DevolutionDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Devolution date should not be a future date");
        }
    }
}
