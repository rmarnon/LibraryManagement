using FluentResults;
using LibraryManagement.Application.ViewModels;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Queries.Books
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, Result<List<BookViewModel>>>
    {
        private readonly IBookRepository _bookRepository;

        public GetAllBooksQueryHandler(IBookRepository bookRepository) => _bookRepository = bookRepository;

        public async Task<Result<List<BookViewModel>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetAllAsync();
            if (books is null)
                return Result.Fail<List<BookViewModel>>("Empty book list");

            return Result.Ok(books.Select(b => new BookViewModel(b.Title, b.Author, b.Isbn, (ushort)b.PublicationYear)).ToList());
        }
    }
}
