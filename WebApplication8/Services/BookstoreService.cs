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
    }


}