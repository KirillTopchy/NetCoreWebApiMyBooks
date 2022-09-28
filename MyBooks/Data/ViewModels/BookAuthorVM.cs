using System.Collections.Generic;

namespace MyBooks.Data.ViewModels
{
    public class BookAuthorVM
    {
        public string BookName { get; set; }
        public List<string> BookAuthors { get; set; }
    }
}
