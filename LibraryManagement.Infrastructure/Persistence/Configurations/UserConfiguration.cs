using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace LibraryManagement.Infrastructure.Persistence.Configurations
{
    [ExcludeFromCodeCoverage]
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));

            builder.HasKey(x => x.Id);            

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(x => x.Email)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Password)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(x => x.Role)
                .HasConversion<int>()
                .IsRequired();
        }
    }
}
