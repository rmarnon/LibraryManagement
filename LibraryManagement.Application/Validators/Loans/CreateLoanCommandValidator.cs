using FluentValidation;
using LibraryManagement.Application.Commands.Loans;

namespace LibraryManagement.Application.Validators.Loans
{
    public class CreateLoanCommandValidator : AbstractValidator<CreateLoanCommand>
    {
        public CreateLoanCommandValidator()
        {
            ValidateUser();
            ValidateBooks();
            ValidateLoanDate();
        }

        private void ValidateBooks()
        {
            RuleFor(x => x.BookIds)
                .NotEmpty()
                .WithMessage("There must be at least one book to loan");

            RuleFor(x => x.BookIds)
                .Must(x => x.Count <= 3)
                .WithMessage("A maximum of 3 books are allowed per loan");
        }

        private void ValidateLoanDate()
        {
            RuleFor(x => x.LoanDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Loan date should not be a future date");
        }

        private void ValidateUser()
        {
            RuleFor(x => x.UserId)
                .Must(x => x != Guid.Empty)
                .WithMessage("User is required!");
        }
    }
}
