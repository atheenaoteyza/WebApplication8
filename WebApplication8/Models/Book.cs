using System.ComponentModel.DataAnnotations;

namespace WebApplication8.Models
{
    public class Book
    {
        public Book() => Authors = new HashSet<Author>();

        [Key]
        public int ISBN { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public double? Price { get; set; }
        public double? Discount { get; set; }

        public ICollection<Author> Authors { get; set; }

    }
}