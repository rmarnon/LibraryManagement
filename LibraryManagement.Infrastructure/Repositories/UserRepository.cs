using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly LibraryDbContext _dbContext;

        public UserRepository(LibraryDbContext dbContext) : base(dbContext) => _dbContext = dbContext;

        public async Task<bool> CheckEmailExsistsAsync(string email)
        {
            return await Query()
                .Where(x => !x.IsDeleted)
                .AnyAsync(x => x.Email == email);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await Query()
                .Include(x => x.Loans).ThenInclude(y => y.BorrowedBooks)
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<User> GetOneAsync(Guid id)
        {
            return await Query()
                .Include(x => x.Loans).ThenInclude(y => y.BorrowedBooks)
                .Where(x => !x.IsDeleted)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
