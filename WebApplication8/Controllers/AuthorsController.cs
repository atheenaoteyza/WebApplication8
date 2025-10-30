using Microsoft.AspNetCore.Mvc;
using WebApplication8.DTOs;
using WebApplication8.Service;

namespace WebApplication8.Controllers
{
    [Route("authors")]
    public class AuthorsController : Controller
    {
        private readonly BookstoreService _bookstoreService;

        public AuthorsController(BookstoreService bookstoreService)
        {
            _bookstoreService = bookstoreService;
        }

        [HttpGet("test-authors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _bookstoreService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        [HttpPost("{authorId}/add-book/{isbn}")]
        public async Task<IActionResult> AddBookToAuthor(int authorId, int isbn)
        {
            try
            {
                await _bookstoreService.AddBookToAuthorAsync(authorId, isbn);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        // ✍️ Add new author
        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorDTO dto)
        {
            try
            {
                await _bookstoreService.AddAuthorAsync(dto); // 👈 await the async method
                return Ok("Author added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/books")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookstoreService.GetAllBooksAsync();
            return Ok(books);
        }


        [HttpPost("/add-book")]
        public async Task<IActionResult> AddNewBook([FromBody] BookDTO dto)
        {
            try
            {
                await _bookstoreService.AddNewBookAsync(dto);
                return Ok("Book added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/update-book")]
        public async Task<IActionResult> UpdateBook([FromBody] BookDTO dto)
        {
            try
            {
                await _bookstoreService.UpdateBookAsync(dto);
                return Ok("Book updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/delete-book/{isbn}")]
        public async Task<IActionResult> DeleteBookById(int isbn)
        {
            try
            {
                await _bookstoreService.DeleteBookAsync(isbn);
                return Ok("Book successfully removed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}