﻿using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Repositories
{
    public interface ILoanRepository : IGenericRepository<Loan>
    {
        Task<Loan> GetLoanAsync(Guid userId);
        Task<bool> ExistsLoanAsync(Guid userId);
    }
}