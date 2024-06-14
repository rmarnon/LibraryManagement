using FluentValidation;
using LibraryManagement.Application.Commands.Loans;

namespace LibraryManagement.Application.Validators
{
    public class UpdateLoanCommandValidator : AbstractValidator<UpdateLoanCommand>
    {
        public UpdateLoanCommandValidator()
        {
            ValidateBooks();
            ValidateLoanDate();
            ValidateDevolutionDate();
        }

        private void ValidateBooks()
        {
            RuleFor(x => x.BookIds)
                .Must(x => x.Count <= 3)
                .WithMessage("A maximum of 3 books are allowed per loan");
        }

        private void ValidateLoanDate()
        {
            RuleFor(x => x.LoanDate)
                .NotEmpty()
                .WithMessage("loan date is required!");

            RuleFor(x => x.LoanDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Loan date should not be a future date");
        }

        private void ValidateDevolutionDate()
        {
            RuleFor(x => x.DevolutionDate)
                .GreaterThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Devolution date should not be a past date");

            RuleFor(x => x.DevolutionDate)
                .GreaterThan(x => x.LoanDate)
                .WithMessage("Devolution date should be after loan date");
        }
    }
}
