using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MyBooks.Controllers;
using MyBooks.Data;
using MyBooks.Data.Models;
using MyBooks.Data.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;

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
