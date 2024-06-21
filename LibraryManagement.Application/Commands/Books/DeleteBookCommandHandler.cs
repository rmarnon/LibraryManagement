using FluentResults;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Commands.Books
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Result>
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookCommandHandler(IBookRepository bookRepository) => _bookRepository = bookRepository;

        public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetOneAsync(request.Id);
            if (book is null)
                return Result.Fail("Book not found");

            await _bookRepository.InactivateAsync(request.Id);
            return Result.Ok(); ;
        }
    }
}
