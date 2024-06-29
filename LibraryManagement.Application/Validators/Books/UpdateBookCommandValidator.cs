using FluentValidation;
using LibraryManagement.Application.Commands.Books;

namespace LibraryManagement.Application.Validators.Books
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            ValidateIsbn();
            ValidateTitle();
            ValidateAuthor();
            ValidatePublication();
        }

        private void ValidateTitle()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title must not to be null");

            RuleFor(x => x.Title)
                .Length(3, 100)
                .When(x => !string.IsNullOrWhiteSpace(x.Title))
                .WithMessage("Title must be between 3 and 100 characters");
        }

        private void ValidateAuthor()
        {
            RuleFor(x => x.Author)
                .NotEmpty()
                .WithMessage("Author must not to be null");

            RuleFor(x => x.Author)
                .Length(2, 50)
                .When(x => !string.IsNullOrWhiteSpace(x.Author))
                .WithMessage("Title must be between 2 and 50 characters");
        }

        private void ValidateIsbn()
        {
            RuleFor(x => x.Isbn)
                .NotEmpty()
                .WithMessage("ISBN must not to be null");

            RuleFor(x => x.Isbn)
                .Length(13)
                .When(y => y.PublicationYear >= 2007)
                .WithMessage("After 2007 the isbn code must have 13 digits");

            RuleFor(x => x.Isbn)
                .Length(10)
                .When(y => y.PublicationYear < 2007)
                .WithMessage("Until 2007, the ISBN code must have 10 digits");
        }

        private void ValidatePublication()
        {
            RuleFor(x => x.PublicationYear)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Publication year must not to be negative number");

            RuleFor(x => x.PublicationYear)
                .Must(y => DateTime.Today.Year >= y)
                .When(z => z.PublicationYear > 0)
                .WithMessage("Publication year should not be a future date");
        }
    }
}
