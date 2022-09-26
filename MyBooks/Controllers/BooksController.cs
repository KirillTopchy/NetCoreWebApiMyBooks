using Microsoft.AspNetCore.Mvc;
using MyBooks.Data.Services;
using MyBooks.Data.ViewModels;

namespace MyBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public BooksService BooksService { get; set; }

        public BooksController(BooksService booksService)
        {
            BooksService = booksService;
        }

        [HttpPost("add-book")]
        public IActionResult AddBook([FromBody]BookVM book)
        {
            BooksService.AddBook(book);
            return Ok();
        }
    }
}
