using MyBooks.Data.Models;
using MyBooks.Data.ViewModels;
using System.Linq;

namespace MyBooks.Data.Services
{
    public class PublishersService
    {
        private readonly AppDbContext _context;
        public PublishersService(AppDbContext context)
        {
            _context = context;
        }

        public void AddPublisher(PublisherVM publisherVM)
        {
            var publisher = new Publisher()
            {
                Name = publisherVM.Name
            };

            _context.Publishers.Add(publisher);
            _context.SaveChanges();
        }

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
    }
}
