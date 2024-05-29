using System.Linq.Expressions;

namespace LibraryManagement.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T model);
        Task<T> UpdateAsync(T model);
        Task<List<T>> GetAllAsync();
        Task<T> GetOneAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task InactivateAsync(Guid id);
    }
}
