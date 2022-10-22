using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using MyBooks.ActionResults;
using MyBooks.Data.Models;
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
        private readonly ILogger<PublishersController> _logger;

        public PublishersController(PublishersService publishersService,
                                    ILogger<PublishersController> logger)
        {
            _publishersService = publishersService;
            _logger = logger;
        }

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string sortBy, string searchString, int pageNumber)
        {
            try
            {
                _logger.LogInformation("This is a log in GetAllPublishers()");
                var result = _publishersService.GetAllPublishers(sortBy, searchString, pageNumber);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Sorry, we could not load the publishers");
            }
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            try
            {
                var newPublisher = _publishersService.AddPublisher(publisher);
                return Created(nameof(AddPublisher), newPublisher);
            }
            catch (PublisherNameException ex)
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
        public /*CustomActionResult*/ IActionResult GetPublisherById(int id)
        {
            // Global error to test global error handling. If want to test remove comment from next row.
            //throw new Exception("This is an exception that well be handled by middleware");

            var result = _publishersService.GetPublisherById(id);

            if (result != null)
            {
                //var responseObject = new CustomActionResultVM()
                //{
                //    Publisher = result
                //};

                //return new CustomActionResult(responseObject);
                return Ok(result);
            }
            else
            {
                //var responseObject = new CustomActionResultVM()
                //{
                //    Exception = new Exception("This is comming from publishers controller")
                //};

                //return new CustomActionResult(responseObject);
                return NotFound();
            }
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
