using System;
using System.Collections.Generic;

namespace MyBooks.Data.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; } //optional property
        public int? Rate { get; set; } //optional property
        public string Genre { get; set; }
        public string CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }

        // Navigation properties
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public List<Book_Author_JoinModel> Book_Authors { get; set; }
        public List<Book_Publisher_JoinModel> Book_Publisher { get; set; }
    }
}
