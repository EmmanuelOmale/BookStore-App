using BookStoreDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStorePersistence.Data
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Book> Books { get; set; }

    }

}