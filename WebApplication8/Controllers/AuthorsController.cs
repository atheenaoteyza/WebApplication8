using Microsoft.AspNetCore.Mvc;
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
    }
}