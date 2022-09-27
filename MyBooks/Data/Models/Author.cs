﻿using System.Collections.Generic;

namespace MyBooks.Data.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        // Navigation properties
        public List<Book_Author_JoinModel> Book_Authors { get; set; }
    }
}
