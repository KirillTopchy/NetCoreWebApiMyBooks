namespace MyBooks.Data.Models
{
    public class Book_Publisher_JoinModel
    {
        public int Id { get; set; }

        // Navigatio properties
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
    }
}
