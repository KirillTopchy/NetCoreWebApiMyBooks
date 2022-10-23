using Microsoft.EntityFrameworkCore;
using MyBooks.Data;
using MyBooks.Data.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System;
using MyBooks.Data.Services;
using System.Linq;

namespace MyBooksTests
{
    public class PublishersServiceTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "BookDbTest")
            .Options;

        private AppDbContext _context;
        private PublishersService _publishersService;

        [OneTimeSetUp]
        public void Setup()
        {
            _context = new AppDbContext(dbContextOptions);
            _context.Database.EnsureCreated();

            SeedDatabase();

            _publishersService = new PublishersService(_context);
        }

        [Test, Order(1)]
        public void GetAllPublishers_WithNoSortBy_WithNoSearcString_WithNoPageNumber_Test()
        {
            var result = _publishersService.GetAllPublishers(string.Empty, string.Empty, null);

            Assert.That(result, Has.Count.EqualTo(5));
        }

        [Test, Order(2)]
        public void GetAllPublishers_WithNoSortBy_WithNoSearcString_WithPageNumber_Test()
        {
            var result = _publishersService.GetAllPublishers(string.Empty, string.Empty, 2);

            Assert.That(result, Has.Count.EqualTo(1));
        }

        [Test, Order(3)]
        public void GetAllPublishers_WithNoSortBy_WithSearcString_WithNoPageNumber_Test()
        {
            var result = _publishersService.GetAllPublishers(string.Empty, "3", null);

            Assert.That(result, Has.Count.EqualTo(1));
            Assert.Multiple(() =>
            {
                Assert.That(result.First().Name, Is.EqualTo("Publisher 3"));
                Assert.That(result.First().Id, Is.EqualTo(3));
            });
        }

        [Test, Order(4)]
        public void GetAllPublishers_WithSortBy_WithNoSearcString_WithNoPageNumber_Test()
        {
            var result = _publishersService.GetAllPublishers("name_desc", string.Empty, null);

            Assert.That(result, Has.Count.EqualTo(5));
            Assert.Multiple(() =>
            {
                Assert.That(result.First().Name, Is.EqualTo("Publisher 6"));
                Assert.That(result.First().Id, Is.EqualTo(6));
            });
        }

        [Test, Order(5)]
        public void GetPublisherById_WithExistingId_Test()
        {
            var result = _publishersService.GetPublisherById(2);

            Assert.Multiple(() =>
            {
                Assert.That(result.Name, Is.EqualTo("Publisher 2"));
                Assert.That(result.Id, Is.EqualTo(2));
            });
        }

        [Test, Order(6)]
        public void GetPublisherById_WithNotExistingId_Test()
        {
            var result = _publishersService.GetPublisherById(77);

            Assert.That(result, Is.Null);
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

            List<Author> authors = new()
            {
                new Author()
                {
                    Id = 1,
                    FullName = "Author 1"
                },
                new Author()
                {
                    Id = 2,
                    FullName = "Author 2"
                }
            };
            _context.Authors.AddRange(authors);

            List<Book> books = new()
            {
                new Book()
                {
                    Id = 1,
                    Title = "Book 1 Title",
                    Description = "Book 1 Description",
                    IsRead = false,
                    Genre = "Genre",
                    CoverUrl = "https://...",
                    DateAdded = DateTime.Now.AddDays(-10),
                    PublisherId = 1
                },
                new Book()
                {
                    Id = 2,
                    Title = "Book 2 Title",
                    Description = "Book 2 Description",
                    IsRead = false,
                    Genre = "Genre",
                    CoverUrl = "https://...",
                    DateAdded = DateTime.Now.AddDays(-10),
                    PublisherId = 1
                }
            };
            _context.Books.AddRange(books);

            List<Book_Author_JoinModel> books_authors = new()
            {
                new Book_Author_JoinModel()
                {
                    Id = 1,
                    BookId = 1,
                    AuthorId = 1
                },
                new Book_Author_JoinModel()
                {
                    Id = 2,
                    BookId = 1,
                    AuthorId = 2
                },
                new Book_Author_JoinModel()
                {
                    Id = 3,
                    BookId = 2,
                    AuthorId = 2
                },
            };
            _context.Books_Authors.AddRange(books_authors);

            _context.SaveChanges();
        }
    }
}