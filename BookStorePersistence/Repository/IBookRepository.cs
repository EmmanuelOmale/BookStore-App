using BookStoreDomain.Entities;

namespace BookStorePersistence.Repository
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book> GetBookAsync(string bookId);
        Task<Book> AddBookAsync(Book book);
    }
}
