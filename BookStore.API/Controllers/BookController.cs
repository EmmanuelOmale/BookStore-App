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
        [HttpPost("consume-book-request")]
        public IActionResult ConsumeBookRequest()
        {
            using (var conn = new ConnectionFatory)
            {
                _rabbitConnection.CreateModel()
                // Declare the queue to consume messages from
                channel.QueueDeclare(queue: "book_request_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

                // Set up a consumer to listen to the queue
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    // Get the book ID from the message
                    var body = ea.Body;
                    var requestedBookId = Encoding.UTF8.GetString(body);

                    // Simulate retrieving the book data by book ID
                    var bookData = RetrieveBookData(requestedBookId);

                    // Send the book data as a response
                    byte[] responseBytes = Encoding.UTF8.GetBytes(bookData);
                    channel.BasicPublish(exchange: "", routingKey: ea.BasicProperties.ReplyTo, basicProperties: null, body: responseBytes);
                };

                // Start consuming messages from the queue
                channel.BasicConsume(queue: "book_request_queue", autoAck: true, consumer: consumer);
            }

            return Ok("Listening for book requests...");
        }
    }
}