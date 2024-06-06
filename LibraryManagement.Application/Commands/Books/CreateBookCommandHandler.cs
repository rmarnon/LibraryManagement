using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Commands.Books
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Unit>
    {
        private readonly IBookRepository _bookRepository;

        public CreateBookCommandHandler(IBookRepository bookRepository) => _bookRepository = bookRepository;

        public async Task<Unit> Handle(CreateBookCommand command, CancellationToken cancellationToken)
        {
            var book = new Book(command.Title, command.Author, command.Isbn, command.PublicationYear);
            await _bookRepository.AddAsync(book);

            return Unit.Value;
        }
    }
}
