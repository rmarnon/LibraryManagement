using FluentValidation;

namespace LibraryManagement.Application.Commands.Loans
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
        }

        private void ValidateLoanDate()
        {
            RuleFor(x => x.LoanDate)
                .NotEmpty()
                .WithMessage("loan date is required!");            
        }

        private void ValidateUser()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User is required!");
        }
    }
}
