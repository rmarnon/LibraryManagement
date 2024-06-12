using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Repositories
{
    public interface ILoanRepository : IGenericRepository<Loan>
    {
        Task<Loan> GetLoanByUserIdAsync(Guid userId);
        Task<bool> ExistsLoanAsync(Guid userId);
        Task<Loan> GetLoanByIdAsync(Guid id);
    }
}
