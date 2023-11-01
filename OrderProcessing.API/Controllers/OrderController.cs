﻿using Microsoft.AspNetCore.Mvc;
using OrderProcessingApplication.Services;
using OrderProcessingDomain.Entities.Dtos;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Diagnostics;
using System.Net;
using System.Text;
using BookStoreDomain.Entities;

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
        [ProducesResponseType(200)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> PlaceOrder([FromBody] CartDto order)
        {
            if (order == null)
            {
                return BadRequest("Invalid request.");
            }
            var customerId = "12345"; // Should be replaced with authenticated customer's Id
            var items = order.OrderItems;
            var newOrder = await _orderProcessingService.PlaceOrderAsync(customerId, items);

            return Ok(newOrder);
        }
        private async Task<Book> ConsumeBookResponse(string bookId)
        {
            var con = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/"
            };
            var fatory = con.CreateConnection();
            using (var channel = fatory.CreateModel())
            {
                // Declare a queue to consume the book response
                channel.QueueDeclare(queue: "book_response_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                var bookData = string.Empty;

                consumer.Received += async (model, ea) =>
                {
                    // Get the book data from the response
                    var body = ea.Body;
                    bookData = Encoding.UTF8.GetString(body);

                    // Check if this is the response for the requested book ID
                    var responseBookId = ea.BasicProperties.CorrelationId;
                    if (responseBookId == bookId)
                    {
                        // You have received the response for the requested book ID
                        // You can now proceed with processing the order
                        var customerId = "12345"; // Replace with authenticated customer's Id
                        var orderItems = new List<CartItem>(); // Replace with your actual order items
                        var newOrder = await _orderProcessingService.PlaceOrderAsync(customerId, orderItems, bookData);

                        // Continue processing with the new order

                        // Acknowledge the message to remove it from the queue
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    }
                };

                // Start consuming the response from the queue
                channel.BasicConsume(queue: "book_response_queue", autoAck: false, consumer: consumer);

                // Send a request for the book with the specified bookId
                var correlationId = Guid.NewGuid().ToString();
                var body = Encoding.UTF8.GetBytes(bookId);
                channel.BasicPublish(
                    exchange: "",
                    routingKey: "book_request_queue",
                    basicProperties: new IBasicProperties
                    {
                        CorrelationId = correlationId,
                        ReplyTo = "book_response_queue"
                    },
                    body: body
                );

                // Wait for a response
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
