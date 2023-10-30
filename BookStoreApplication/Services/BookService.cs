using AutoMapper;
using BookStoreDomain.Entities;
using BookStoreDomain.Entities.Dtos;
using BookStorePersistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApplication.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

/*        public async Task<AddBookDto> AddBookAsync(AddBookDto addBookDto)
        {
            if(addBookDto == null) { throw new ArgumentException("Please add a new book"); }
            var book = _mapper.Map<Book>(addBookDto);
            if (book == null)
            {
                throw new ApplicationException("There is an issue with mappiing new product");
            }
            var newBook = await _bookRepository
                .AddBookAsync(book);

        }
*/
        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository
                .GetBooksAsync();
        }

        public async Task<Book> GetSingleBookAsync(string bookId)
        {
            if (bookId == null)
            {
                throw new ArgumentException("Book Id field cannot be empty");
            }
            return await _bookRepository
                .GetBookAsync(bookId);
                
        }
    }
}
