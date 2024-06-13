using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> CheckEmailExsistsAsync(string email);
        Task<List<User>> GetAllAsync();
        Task<User> GetOneAsync(Guid id);
        Task<User> GetUserByEmailAndPasswordHashAsync(string email, string passwordHash);
    }
}
