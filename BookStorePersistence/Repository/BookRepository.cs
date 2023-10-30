using BookStoreDomain.Entities;
using BookStorePersistence.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStorePersistence.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _bookContext;

        public BookRepository(BookContext bookContext)
        {
            _bookContext = bookContext;
        }
        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _bookContext
                .Books
                .ToListAsync();
        }

        public async Task<Book> GetBookAsync(string BookId)
        {
            return await _bookContext
                .Books
                .Where(x => x.Id == BookId)
                .FirstOrDefaultAsync();
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            await _bookContext
                .Books
                .AddAsync(book);

            return book;
        }
    }
}
