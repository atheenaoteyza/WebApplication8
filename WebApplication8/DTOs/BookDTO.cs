namespace WebApplication8.DTOs
{
    public class BookDTO
    {
        public int ISBN { get; set; }

        public string Title { get; set; }

        public double? Price { get; set; }
        public double? Discount { get; set; }

        public List<AuthorDTO> Authors { get; set; } = new();

    }
}