using Microsoft.EntityFrameworkCore;
using WebApplication8.DTOs;
using WebApplication8.Models;

namespace WebApplication8.Services
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
            var author = await _context.Authors.FindAsync(authorId);
            var book = await _context.Books.FindAsync(isbn);

            if (author == null || book == null)
                throw new Exception("Author or book not found");

            //Prevent duplicates
            if (author.Books.Any(ab => ab.ISBN == isbn))
                throw new Exception("Book already assigned to this authtor");

            author.Books.Add(book);
            await _context.SaveChangesAsync();
        }

    }
}