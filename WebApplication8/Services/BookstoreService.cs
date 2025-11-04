using Microsoft.EntityFrameworkCore;
using WebApplication8.DTOs;
using WebApplication8.Models;

namespace WebApplication8.Service
{
    public class BookstoreService
    {
        private readonly BookstoreContext _context;

        public BookstoreService(BookstoreContext context)
        {
            _context = context;
        }

        public async Task<List<AuthorDTO>> GetAllAuthorsAsync()
        {
            return await _context.Authors
                .Include(a => a.Books)
                .Select(a => new AuthorDTO
                {
                    AuthorId = a.AuthorId,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Books = a.Books.Select(ab => new BookDTO
                    {
                        ISBN = ab.ISBN,
                        Title = ab.Title,
                        Price = ab.Price,
                        Discount = ab.Discount
                    }).ToList()
                })
                .ToListAsync();
        }



        public async Task AddBookToAuthorAsync(int authorId, int isbn)
        {
            var author = await _context.Authors
                .Include(a => a.Books) // ✅ include books to check duplicates
                .FirstOrDefaultAsync(a => a.AuthorId == authorId);

            var book = await _context.Books.FindAsync(isbn);

            if (book == null || author == null)
                throw new KeyNotFoundException("Author or book not found");

            // ✅ Prevent duplicates at the app level
            if (author.Books.Any(a => a.ISBN == isbn))
                throw new InvalidOperationException("Book already assigned to this author");

            author.Books.Add(book);
            await _context.SaveChangesAsync();
        }


        public async Task AddAuthorAsync(AuthorDTO dto)
        {

            var author = new Author
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();


        }

        // Read Books
        public async Task<List<BookDTO>> GetAllBooksAsync()
        {
            return await _context.Books.Include(b => b.Authors)
            .Select(b => new BookDTO
            {
                ISBN = b.ISBN,
                Title = b.Title,
                Price = b.Price,
                Discount = b.Discount,
                Authors = b.Authors.Select(ba => new AuthorDTO
                {
                    AuthorId = ba.AuthorId,
                    FirstName = ba.FirstName,
                    LastName = ba.LastName,
                }).ToList()
            }).ToListAsync();
        }
        // Create a Book
        public async Task AddNewBookAsync(BookDTO dto)
        {

            var book = new Book
            {
                Title = dto.Title,
                Price = dto.Price,
                Discount = dto.Discount,
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }
        // Update a Book
        public async Task UpdateBookAsync(BookDTO dto)
        {
            var book = await _context.Books.FindAsync(dto.ISBN) ?? throw new Exception("Book not found");

            book.Title = dto.Title;
            book.Price = dto.Price;
            book.Discount = dto.Discount;

            await _context.SaveChangesAsync();
        }

        //Delete a Book
        public async Task DeleteBookAsync(int isbn)
        {
            var book = await _context.Books.FindAsync(isbn) ?? throw new Exception("Book not found");
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}