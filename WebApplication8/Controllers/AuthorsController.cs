using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly BookstoreContext _context;

        public AuthorsController(BookstoreContext context)
        {
            _context = context;
        }

        // 👇 Test endpoint to view authors + books
        [HttpGet("/test-authors")]
        public IActionResult TestAuthors()
        {
            var authors = _context.Authors
                .Include(a => a.Books)
                .ToList();

            foreach (var author in authors)
            {
                Console.WriteLine($"{author.FirstName} {author.LastName}");
                foreach (var book in author.Books)
                {
                    Console.WriteLine($"  - {book.Title}");
                }
            }

            // You can also return JSON if you want to see results in the browser:
            return Json(authors);
        }
    }
}
