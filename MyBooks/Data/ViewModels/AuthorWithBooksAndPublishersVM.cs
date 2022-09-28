using MyBooks.Data.Models;
using System.Collections.Generic;

namespace MyBooks.Data.ViewModels
{
    public class AuthorWithBooksAndPublishersVm
    {
        public string FullName { get; set; }
        public List<string> BookTitles { get; set; }
        public List<Publisher> BookPublishers { get; set; }
    }
}
