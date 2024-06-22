using FluentResults;
using LibraryManagement.Application.ViewModels;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Queries.Books
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, Result<BookViewModel>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookQueryHandler(IBookRepository bookRepository) => _bookRepository = bookRepository;

        public async Task<Result<BookViewModel>> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetOneAsync(request.Id);
            if (book is null) return Result.Fail("Book not found");

            return Result.Ok(new BookViewModel(book.Title, book.Author, book.Isbn, (ushort)book.PublicationYear));
        }
    }
}
