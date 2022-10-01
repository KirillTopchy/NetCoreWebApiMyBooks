using Microsoft.AspNetCore.Mvc;
using MyBooks.Data.Services;
using MyBooks.Data.ViewModels;
using System.Linq;

namespace MyBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksService _booksService;

        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpPost("add-book-with-authors")]
        public IActionResult AddBook([FromBody] BookVM book)
        {
            var newBook = _booksService.AddBookWithAuthors(book);
            return Created(nameof(AddBook), newBook);
        }

        [HttpGet("get-all-books")]
        public IActionResult GetAllBooks()
        {
            var books = _booksService.GetAllBooks();

            if (books.Any())
                return Ok(books);
            else
                return NotFound();
        }

        [HttpGet("get-book-by-id/{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _booksService.GetBookById(id);

            if (book != null)
                return Ok(book);
            else
                return NotFound();
        }

        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBookById(int id, [FromBody] BookVM book)
        {
            var updatedBook = _booksService.UpdateBookById(id, book);
            if (updatedBook != null)
                return Ok(updatedBook);
            else
                return NotFound();
        }

        [HttpDelete("delete-book-by-id/{id}")]
        public IActionResult DeleteBookById(int id)
        {
            var deleted = _booksService.DeleteBookById(id);

            if (deleted)
                return Ok();
            else
                return NotFound();
        }
    }
}
