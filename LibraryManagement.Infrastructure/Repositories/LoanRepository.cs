using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class LoanRepository : BaseRepository<Loan>, ILoanRepository
    {
        private readonly LibraryDbContext _context;
        public LoanRepository(LibraryDbContext libraryDbContext) : base(libraryDbContext) => _context = libraryDbContext;

        public async Task<Loan> GetLoanByUserIdAsync(Guid userId)
        {
            return await Query()
                .Include(x => x.BorrowedBooks)
                .SingleOrDefaultAsync(x => x.UserId == userId && !x.IsDeleted);
        }

        public async Task<bool> ExistsLoanAsync(Guid userId)
        {
            return await Query().AnyAsync(x => x.UserId == userId && !x.IsDeleted);
        }

        public async Task<Loan> GetOneAsync(Guid id)
        {
            return await Query()
                .Include(x => x.User)
                .Include(x => x.BorrowedBooks).ThenInclude(y => y.Book)
                .SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<List<Loan>> GetAllAsync()
        {
            return await Query()
                .Include(x => x.User)
                .Include(x => x.BorrowedBooks).ThenInclude(y => y.Book)
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }
    }
}
