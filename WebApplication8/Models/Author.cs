using System.ComponentModel.DataAnnotations;

namespace WebApplication8.Models
{
    public class Author
    {
        [Key] //Primary Key     
        public int AuthorId { get; set; }

        [Required]
        [MaxLength(200)] // not nullable
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}