using Microsoft.AspNetCore.Mvc;
using MyBooks.Data.Services;
using MyBooks.Data.ViewModels;

namespace MyBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorsService _authorsService;
        public AuthorsController(AuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpPost("add-author")]
        public IActionResult AddAuthor([FromBody] AuthorVM author)
        {
            var newAuthor = _authorsService.AddAuthor(author);
            return Created(nameof(AddAuthor), newAuthor);
        }

        [HttpGet("get-author-with-books-and-publishers-by-id/{id}")]
        public IActionResult GetAuthorWithBooksAndPublishers(int id)
        {
            var result = _authorsService.GetAuthorWithBooksAndPublishers(id);

            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }
    }
}
