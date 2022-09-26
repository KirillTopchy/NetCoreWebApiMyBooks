using MyBooks.Data.Models;
using MyBooks.Data.ViewModels;
using System;

namespace MyBooks.Data.Services
{
    public class BooksService
    {
        private readonly AppDbContext _context;
        public BooksService(AppDbContext context)
        {
            _context = context;
        }

        public void AddBook(BookVM bookVM)
        {
            var book = new Book()
            {
                Title = bookVM.Title,
                Description = bookVM.Description,
                IsRead = bookVM.IsRead,
                DateRead = bookVM.IsRead ? bookVM.DateRead.Value : null,
                Rate = bookVM.IsRead ? bookVM.Rate.Value : null,
                Genre = bookVM.Genre,
                Author = bookVM.Author,
                CoverUrl = bookVM.CoverUrl,
                DateAdded = DateTime.Now
            };

            _context.Books.Add(book);
            _context.SaveChanges();
        }
    }
}
