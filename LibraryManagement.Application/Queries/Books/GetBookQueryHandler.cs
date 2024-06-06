using LibraryManagement.Application.ViewModels;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Queries.Books
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, BookViewModel>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookQueryHandler(IBookRepository bookRepository) => _bookRepository = bookRepository;

        public async Task<BookViewModel> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetOneAsync(request.Id);
            if (book is null) return null;
            return new(book.Title, book.Author, book.Isbn, book.PublicationYear);
        }
    }
}
