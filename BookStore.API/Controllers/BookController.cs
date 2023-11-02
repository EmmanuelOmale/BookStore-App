using BookStoreApplication.Services;
using BookStoreDomain.Entities;
using BookStoreDomain.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

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
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }
        [HttpGet]
        [Route("{bookId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookDto>> GetBookByIdAsync(string bookId)
        {
            var book = await _bookService.GetSingleBookAsync(bookId);
            return Ok(book);
        }
        [HttpPost("consume-book-request")]
        public IActionResult ConsumeBookRequest()
        {
            var con = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/"
            };
            var factory = con.CreateConnection();
            using (var channel = factory.CreateModel())
            {
                channel.QueueDeclare(queue: "book_request_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var requestedBookId = Encoding.UTF8.GetString(body);
                    var bookData = await RetrieveBookData(requestedBookId);

                    var responseBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(bookData));
                    channel.BasicPublish(exchange: "", routingKey: ea.BasicProperties.ReplyTo, basicProperties: null, body: responseBytes);
                };

                channel.BasicConsume(queue: "book_request_queue", autoAck: true, consumer: consumer);
            }

            return Ok("Listening for book requests...");
        }
        private async Task<Book> RetrieveBookData(string bookId)
        {
            // Simulate retrieving book data from your data source
            return await _bookService.GetSingleBookAsync(bookId);
        }





    }
}