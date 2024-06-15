using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Repositories
{
    public interface ILoanRepository : IGenericRepository<Loan>
    {
        Task<Loan> GetLoanByUserIdAsync(Guid userId);
        Task<bool> ExistsLoanByUserIdAsync(Guid userId);
        Task<Loan> GetOneAsync(Guid id);
        Task<List<Loan>> GetAllAsync();
    }
}
