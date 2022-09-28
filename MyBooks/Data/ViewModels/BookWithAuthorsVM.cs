using System.Collections.Generic;
using System;

namespace MyBooks.Data.ViewModels
{
    public class BookWithAuthorsVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; } //optional property
        public int? Rate { get; set; } //optional property
        public string Genre { get; set; }
        public string CoverUrl { get; set; }
        public string PublisherName { get; set; }
        public List<string> AuthorNames { get; set; }
    }
}
