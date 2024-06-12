using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Repositories
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<Book> FindByIsbnAsync(string isbn);
    }
}
