using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.Persistence.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable(nameof(Book));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Author)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.PublicationYear)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.Isbn)
                .HasMaxLength(13)
                .IsRequired();
        }
    }
}
