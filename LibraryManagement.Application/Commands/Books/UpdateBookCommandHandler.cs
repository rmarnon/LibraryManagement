using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Commands.Books
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Unit>
    {
        private readonly IBookRepository _bookRepository;

        public UpdateBookCommandHandler(IBookRepository bookRepository) => _bookRepository = bookRepository;

        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetOneAsync(request.Id);

            if (book != null)
            {
                book.Update(request.Title, request.Author, request.Isbn, request.PublicationYear);
                await _bookRepository.UpdateAsync(book);
            }

            return Unit.Value;
        }
    }
}
