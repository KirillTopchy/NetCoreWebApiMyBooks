using MyBooks.Data.Models;
using MyBooks.Data.Paging;
using MyBooks.Data.ViewModels;
using MyBooks.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyBooks.Data.Services
{
    public class PublishersService
    {
        private readonly AppDbContext _context;
        public PublishersService(AppDbContext context)
        {
            _context = context;
        }

        public Publisher AddPublisher(PublisherVM publisherVM)
        {
            if (StringStartsWithNumber(publisherVM.Name))
                throw new PublisherNameException("Name starts with number", publisherVM.Name);

            var publisher = new Publisher()
            {
                Name = publisherVM.Name
            };

            _context.Publishers.Add(publisher);
            _context.SaveChanges();

            return publisher;
        }

        public Publisher GetPublisherById(int id) => _context.Publishers.FirstOrDefault(x => x.Id == id);

        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId)
        {
            var publisherData = _context.Publishers
                .Where(publisher => publisher.Id == publisherId)
                .Select(publisher => new PublisherWithBooksAndAuthorsVM()
                    {
                        Name = publisher.Name,
                        BookAuthors = publisher.Books
                            .Select(book => new BookAuthorVM()
                            {
                                BookName = book.Title,
                                BookAuthors = book.Book_Authors
                                    .Select(bookAuthorJoinModel => bookAuthorJoinModel.Author.FullName)
                                    .ToList()
                            })
                            .ToList()
                    })
                .FirstOrDefault();

            return publisherData;
        }

        public void DelePublisherById(int id)
        {
            var publisher = _context.Publishers.FirstOrDefault(p => p.Id == id);

            if(publisher != null)
            {
                _context.Publishers.Remove(publisher);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"The publisher with id {id} does not exist");
            }
        }

        private bool StringStartsWithNumber(string name) => Regex.IsMatch(name, @"^\d");

        public List<Publisher> GetAllPublishers(string sortBy, string searchString, int? pageNumber)
        {
            var allPublishers = _context.Publishers
                .OrderBy(p => p.Name)
                .ToList();

            // Sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_desc":
                        allPublishers = allPublishers.OrderByDescending(p => p.Name).ToList();
                        break;
                    default:
                        break;
                }
            }

            // Filtering
            if (!string.IsNullOrEmpty(searchString))
            {
                allPublishers = allPublishers.Where(p => p.Name.Contains(searchString,
                    StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            // Paging
            int pageSize = 5;
            allPublishers = PaginatedList<Publisher>.Create(allPublishers.AsQueryable(), pageNumber ?? 1, pageSize);

            return allPublishers;
        } 
    }
}
