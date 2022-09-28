using MyBooks.Data.Models;
using System.Collections.Generic;

namespace MyBooks.Data.ViewModels
{
    public class AuthorWithBooksVM
    {
        public string FullName { get; set; }
        public List<string> BookTitles { get; set; }
        public List<Publisher> BookPublishers { get; set; }
    }
}
