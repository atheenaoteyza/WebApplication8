namespace WebApplication8.Models
{
    public class Author
    {
        // 👇 This constructor runs every time you create a new Author object
        public Author() => Books = new HashSet<Book>(); // Initializes Books property

        // 👇 This is the primary key
        public int AuthorId { get; set; }

        // 👇 Basic scalar properties
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // 👇 Declares a navigation property for the many-to-many relationship
        public ICollection<Book> Books { get; set; } // Declares Books as a Collection of Book
    }



    // Creates a collection of Books for each new Author

}



