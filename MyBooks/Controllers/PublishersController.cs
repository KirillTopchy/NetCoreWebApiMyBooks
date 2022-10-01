using Microsoft.AspNetCore.Mvc;
using MyBooks.Data.Services;
using MyBooks.Data.ViewModels;
using MyBooks.Exceptions;
using System;

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
            try
            {
                var newPublisher = _publishersService.AddPublisher(publisher);
                return Created(nameof(AddPublisher), newPublisher);
            }
            catch(PublisherNameException ex)
            {
                return BadRequest($"{ex.Message}, Publisher name: {ex.PublisherName}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
            // Global error to test global error handling. if want to test endpoint comment next row.
            throw new Exception("This is an exception that well be handled by middleware");

            var result = _publishersService.GetPublisherById(id);

            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            try
            {
                _publishersService.DelePublisherById(id);
                return Ok();
            }
         
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
