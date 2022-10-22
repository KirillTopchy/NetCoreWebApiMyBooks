using Microsoft.EntityFrameworkCore;
using MyBooks.Data.Models;

namespace MyBooks.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book_Author_JoinModel>()
                .HasOne(book => book.Book)
                .WithMany(bookAuthors => bookAuthors.Book_Authors)
                .HasForeignKey(bookId => bookId.BookId);

            modelBuilder.Entity<Book_Author_JoinModel>()
                .HasOne(book => book.Author)
                .WithMany(bookAuthors => bookAuthors.Book_Authors)
                .HasForeignKey(bookId => bookId.AuthorId);

            modelBuilder.Entity<Log>().HasKey(l => l.Id);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book_Author_JoinModel> Books_Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
