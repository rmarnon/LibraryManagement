﻿namespace LibraryManagement.Core.Entities
{
    public class Loan : BaseEntity
    {
        public DateTime LoanDate { get; private set; }
        public DateTime? DevolutionDate { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public virtual List<LoanBook> BorrowedBooks { get; private set; } = [];

        public Loan(DateTime loanDate, Guid userId)
        {
            UserId = userId;
            LoanDate = loanDate;
            DevolutionDate = DateTime.Today.AddDays(7);
        }
    }
}
