using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderProcessingApplication.Services;
using OrderProcessingDomain.Entities;
using OrderProcessingDomain.Entities.Dtos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace OrderProcessing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderProcessingService _orderProcessingService;

        public OrderController(IOrderProcessingService orderProcessingService)
        {
            _orderProcessingService = orderProcessingService;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PlaceOrder([FromBody] CartItem order)
        {
            if (order == null)
            {
                return BadRequest("Invalid request.");
            }
            var bookData = await ConsumeBookResponse(order.BookId);
            var caritem = new CartItem()
            {
                BookId = order.BookId,
                Quantity = order.Quantity,
                UnitPrice = 20,
                Id = Guid.NewGuid().ToString(),
            };
            _orderProcessingService.PlaceOrderAsync(order.BookId, order.CartId, 20, 200);
            return Ok(caritem);
        }

        private async Task<string> ConsumeBookResponse(string bookId)
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
                channel.QueueDeclare(queue: "book_response_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
                var consumer = new EventingBasicConsumer(channel);
                var bookData = string.Empty;

                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    bookData = Encoding.UTF8.GetString(body);
                    var responseBookId = ea.BasicProperties.CorrelationId;

                    if (responseBookId == bookId)
                    {
                        var newOrder = await _orderProcessingService.PlaceOrderAsync(bookData, "123", 2, 200);
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    }
                };

                channel.BasicConsume(queue: "book_response_queue", autoAck: false, consumer: consumer);

                var correlationId = bookId;
                var body = Encoding.UTF8.GetBytes(bookId);
                channel.BasicPublish(
                    exchange: "",
                    routingKey: "book_request_queue",
                    body: body
                );

                var timeout = TimeSpan.FromSeconds(10); // Set a reasonable timeout
                var stopwatch = Stopwatch.StartNew();
                while (string.IsNullOrEmpty(bookData) && stopwatch.Elapsed < timeout)
                {
                    await Task.Delay(100); // Adjust the delay based on your expected response time
                }

                return bookData;
            }
        }
        }
}