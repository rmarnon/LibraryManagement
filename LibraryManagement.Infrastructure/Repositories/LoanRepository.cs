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

        public async Task<Loan> GetLoanAsync(Guid userId)
        {
            return await Query()
                .Include(x => x.BorrowedBooks)
                .SingleOrDefaultAsync(x => x.UserId == userId
                && !x.IsDeleted);
        }

        public async Task<bool> ExistsLoanAsync(Guid userId)
        {
            return await Query().AnyAsync(x => x.UserId == userId);
        }
    }
}
