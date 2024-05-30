using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> CheckEmailExsistsAsync(string email);
    }
}
