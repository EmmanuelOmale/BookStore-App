using BookStoreApplication.Services;
using BookStoreDomain.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [Route("GetAllBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooksAsync()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }


        [HttpGet]
        [Route("{id:string}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookDto>> GetBookByIdAsync(string bookId)
        {
            var book = await _bookService
                .GetSingleBookAsync(bookId);

            return Ok(book);
        }
    }
}