using FluentValidation;
using LibraryManagement.Application.Commands.Loans;

namespace LibraryManagement.Application.Validators.Loans
{
    public class UpdateLoanCommandValidator : AbstractValidator<UpdateLoanCommand>
    {
        public UpdateLoanCommandValidator()
        {
            ValidateBooks();
            ValidateLoanId();
            ValidateLoanDate();
            ValidateDevolutionDate();
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
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Loan date should not be a future date");
        }

        private void ValidateDevolutionDate()
        {
            RuleFor(x => x.DevolutionDate)
                .GreaterThan(x => x.LoanDate)
                .WithMessage("Devolution date should be after loan date");
        }

        private void ValidateLoanId()
        {
            RuleFor(x => x.Id)
                .Must(x => x != Guid.Empty)
                .WithMessage("Loan is required!");
        }
    }
}
