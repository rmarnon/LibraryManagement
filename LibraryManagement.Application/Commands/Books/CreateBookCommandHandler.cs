using FluentResults;
using LibraryManagement.Application.ViewModels;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Commands.Books
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Result<BookViewModel>>
    {
        private readonly IBookRepository _bookRepository;

        public CreateBookCommandHandler(IBookRepository bookRepository) => _bookRepository = bookRepository;

        public async Task<Result<BookViewModel>> Handle(CreateBookCommand command, CancellationToken cancellationToken)
        {
            var book = new Book(command.Title, command.Author, command.Isbn, command.PublicationYear);
            await _bookRepository.AddAsync(book);

            return Result.Ok(new BookViewModel(book.Title, book.Author, book.Isbn, (ushort)book.PublicationYear));
        }
    }
}
