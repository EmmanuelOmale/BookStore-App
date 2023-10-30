using BookStoreDomain.Entities;
using BookStoreDomain.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApplication.Services
{
    public interface IBookService
    {
        //Task<AddBookDto> AddBookAsync(AddBookDto addBookDto);
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetSingleBookAsync(string bookId);
    }
}
