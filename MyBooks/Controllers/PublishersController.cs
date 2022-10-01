using Microsoft.AspNetCore.Mvc;
using MyBooks.Data.Services;
using MyBooks.Data.ViewModels;

namespace MyBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly PublishersService _publishersService;

        public PublishersController(PublishersService publishersService)
        {
            _publishersService = publishersService;
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            var newPublisher = _publishersService.AddPublisher(publisher);
            return Created(nameof(AddPublisher), newPublisher);
        }

        [HttpGet("get-publisher-books-with-authors-by-publisher-id/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var result = _publishersService.GetPublisherData(id);

            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            var result = _publishersService.GetPublisherById(id);

            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            var deleted = _publishersService.DelePublisherById(id);

            if (deleted)
                return Ok();
            else
                return NotFound();
        }
    }
}
