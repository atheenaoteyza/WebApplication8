using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication8.Models
{
    public class AuthorConfig : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> entity)
        {
            // Fluent API configuring ISBN as PK
            entity.HasKey(b => b.AuthorId);
            entity.Property(b => b.FirstName).IsRequired().HasMaxLength(200);
            // entity.Property(b => b.Price).IsRequired();

            entity.HasData(
                new Author { AuthorId = 00001, FirstName = "John Ronald Reuel", LastName = "Tolkien" },
                new Author { AuthorId = 00002, FirstName = "Augusten", LastName = "Burroughs" }
            );
        }
    }
}