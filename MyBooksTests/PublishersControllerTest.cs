using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MyBooks.Controllers;
using MyBooks.Data;
using MyBooks.Data.Models;
using MyBooks.Data.Services;
using MyBooks.Data.ViewModels;
using MyBooks.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBooksTests
{
    public class PublishersControllerTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "BookDbControllerTest")
            .Options;

        private AppDbContext _context;
        private PublishersService _publishersService;
        private PublishersController _publishersController;

        [OneTimeSetUp]
        public void Setup()
        {
            _context = new AppDbContext(dbContextOptions);
            _context.Database.EnsureCreated();

            SeedDatabase();

            _publishersService = new PublishersService(_context);
            _publishersController = new PublishersController(_publishersService, new NullLogger<PublishersController>());
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [Test, Order(1)]
        public void HTTPGET_GetAllPublishers_WithSortBySearchPageNr_ReturnOk_Test()
        {
            var actionResult = _publishersController.GetAllPublishers("name_desc", "Publisher", 1);
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
            var resultDataFirstPage = (actionResult as OkObjectResult).Value as List<Publisher>;
            Assert.Multiple(() =>
            {
                Assert.That(resultDataFirstPage.First().Name, Is.EqualTo("Publisher 6"));
                Assert.That(resultDataFirstPage.First().Id, Is.EqualTo(6));
                Assert.That(resultDataFirstPage, Has.Count.EqualTo(5));
            });

            var actionResultSecond = _publishersController.GetAllPublishers("name_desc", "Publisher", 2);
            Assert.That(actionResultSecond, Is.TypeOf<OkObjectResult>());
            var resultDataSecondPage = (actionResultSecond as OkObjectResult).Value as List<Publisher>;
            Assert.Multiple(() =>
            {
                Assert.That(resultDataSecondPage.First().Name, Is.EqualTo("Publisher 1"));
                Assert.That(resultDataSecondPage.First().Id, Is.EqualTo(1));
                Assert.That(resultDataSecondPage, Has.Count.EqualTo(1));
            });
        }

        [Test, Order(2)]
        public void HTTPGET_GetPublisherById_ReturnOk_Test()
        {
            var actionResult = _publishersController.GetPublisherById(1);
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
            var publisherData = (actionResult as OkObjectResult).Value as Publisher;
            Assert.Multiple(() =>
            {
                Assert.That(publisherData, Is.Not.Null);
                Assert.That(publisherData.Name, Is.EqualTo("publisher 1").IgnoreCase);
                Assert.That(publisherData.Id, Is.EqualTo(1));
            });
        }

        [Test, Order(3)]
        public void HTTPGET_GetPublisherById_ReturnNotFound_Test()
        {
            var actionResult = _publishersController.GetPublisherById(77);
            Assert.That(actionResult, Is.TypeOf<NotFoundResult>());
        }

        [Test, Order(4)]
        public void HTTPGET_AddPublisher_ThrowsNameExcaption_And_ReturnBadRequest_Test()
        {
            var publisherVM = new PublisherVM()
            {
                Name = "7 Publisher"
            };

            var actionResult = _publishersController.AddPublisher(publisherVM);
            var resultData = (actionResult as BadRequestObjectResult).Value.ToString();

            Assert.Multiple(() =>
            {
                Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());
                Assert.That(_context.Publishers.ToList(), Has.Count.EqualTo(6));
                Assert.That(resultData, Is.EqualTo("Name starts with number, Publisher name: 7 Publisher"));
            });
        }

        [Test, Order(5)]
        public void HTTPGET_AddPublisher_ReturnCreated_Test()
        {
            var publisherVM = new PublisherVM()
            {
                Name = "Publisher 7"
            };

            var actionResult = _publishersController.AddPublisher(publisherVM);
            Assert.Multiple(() =>
            {
                Assert.That(actionResult, Is.TypeOf<CreatedResult>());
                Assert.That(_context.Publishers.ToList(), Has.Count.EqualTo(7));
            });
        }

        private void SeedDatabase()
        {
            List<Publisher> publishers = new()
            {
                    new Publisher() {
                        Id = 1,
                        Name = "Publisher 1"
                    },
                    new Publisher() {
                        Id = 2,
                        Name = "Publisher 2"
                    },
                    new Publisher() {
                        Id = 3,
                        Name = "Publisher 3"
                    },
                    new Publisher() {
                        Id = 4,
                        Name = "Publisher 4"
                    },
                    new Publisher() {
                        Id = 5,
                        Name = "Publisher 5"
                    },
                    new Publisher() {
                        Id = 6,
                        Name = "Publisher 6"
                    },
            };
            _context.Publishers.AddRange(publishers);

            _context.SaveChanges();
        }
    }
}
