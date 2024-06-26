using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace LibraryManagement.Infrastructure.Persistence.Configurations
{
    [ExcludeFromCodeCoverage]
    public class LoanBookConfiguration : IEntityTypeConfiguration<LoanBook>
    {
        public void Configure(EntityTypeBuilder<LoanBook> builder)
        {
            builder.ToTable(nameof(LoanBook));

            builder.HasKey(p => new
            {
                p.LoanId,
                p.BookId
            });
        }
    }
}
