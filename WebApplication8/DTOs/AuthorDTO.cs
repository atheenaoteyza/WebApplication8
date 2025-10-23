
namespace WebApplication8.DTOs
{
    public class AuthorDTO
    {
        public int AuthorId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<BookDTO> Books { get; set; } = new();
    }
}