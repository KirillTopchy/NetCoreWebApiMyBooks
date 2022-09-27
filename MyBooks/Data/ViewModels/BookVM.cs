using System;
using System.Collections.Generic;

namespace MyBooks.Data.ViewModels
{
    public class BookVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; } //optional property
        public int? Rate { get; set; } //optional property
        public string Genre { get; set; }
        public string CoverUrl { get; set; }
        public int PublisherId { get; set; }
        public List<int> AuthorsIds { get; set; }
    }
}
