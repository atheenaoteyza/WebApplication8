using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication8.Models
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> entity)
        {
            //Fluent API configuring ISBN as PK
            entity.HasKey(b => b.ISBN);
            entity.Property(b => b.Title).IsRequired().HasMaxLength(200);

            entity.HasData(
                new Book { ISBN = 00001, Title = "The Hobbit" },
                new Book { ISBN = 00002, Title = "The Running Scissors" }
                );

        }
    }
}