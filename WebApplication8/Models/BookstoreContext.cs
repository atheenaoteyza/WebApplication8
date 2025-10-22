using Microsoft.EntityFrameworkCore;

namespace WebApplication8.Models
{
    public class BookstoreContext : DbContext
    {
        public BookstoreContext(DbContextOptions<BookstoreContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfig());
            modelBuilder.ApplyConfiguration(new AuthorConfig());

            modelBuilder.Entity<Book>()
            .HasMany(b => b.Authors)
            .WithMany(a => a.Books)
            .UsingEntity(ab => ab.HasData(
                new { AuthorsAuthorId = 0001, BooksISBN = 0001 },
                new { AuthorsAuthorId = 0002, BooksISBN = 0002 }

            ));
        }
    }
}
