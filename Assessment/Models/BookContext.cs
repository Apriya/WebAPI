using Microsoft.EntityFrameworkCore;

namespace Assessment.Models
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions options) : base(options) { }
        public DbSet<Books> BooksData {get;set;}
    }
}
