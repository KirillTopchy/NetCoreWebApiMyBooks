using MyBooks.Data.Models;
using MyBooks.Data.ViewModels;
using System.Linq;

namespace MyBooks.Data.Services
{
    public class AuthorsService
    {
        private readonly AppDbContext _context;
        public AuthorsService(AppDbContext context)
        {
            _context = context;
        }

        public void AddAuthor(AuthorVM authorVM)
        {
            var author = new Author()
            {
                FullName = authorVM.FullName
            };

            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        public AuthorWithBooksAndPublishersVm GetAuthorWithBooksAndPublishers(int authorId)
        {
            var author = _context.Authors.Where(x => x.Id == authorId)
                .Select(author => new AuthorWithBooksAndPublishersVm()
                    {
                        FullName = author.FullName,
                        BookTitles = author.Book_Authors
                            .Select(bookAuthorJoinModel => bookAuthorJoinModel.Book.Title)
                            .ToList(),
                        BookPublishers = author.Book_Authors
                            .Select(bookAuthorJoinModel => bookAuthorJoinModel.Book.Publisher)
                            .ToList()
                    })
                .FirstOrDefault();

            return author;
        }
    }
}
