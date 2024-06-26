using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace LibraryManagement.Infrastructure.Repositories
{
    [ExcludeFromCodeCoverage]
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        private readonly LibraryDbContext _dbContext;

        public BookRepository(LibraryDbContext libraryDbContext) : base(libraryDbContext) => _dbContext = libraryDbContext;

        public async Task<Book> FindByIsbnAsync(string isbn)
        {
            return await Query().FirstOrDefaultAsync(x => x.Isbn == isbn);
        }
    }
}
