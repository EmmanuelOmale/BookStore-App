﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderProcessingApplication.Services;
using OrderProcessingDomain.Entities;
using OrderProcessingDomain.Entities.Dtos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Text;
namespace OrderProcessing.API.Controllers
{
    [Route("api/OrderAPI")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderProcessingService _orderProcessingService;
        private readonly IMapper _mapper;

        public OrderController(IOrderProcessingService orderProcessingService, IMapper mapper)
        {
            _orderProcessingService = orderProcessingService;
            _mapper = mapper;   

        }

        [HttpPost("{addItemToCart}", Name = "Add book to cart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddBookToCart([FromBody] CartItemDto cartItem)
        {
            if (cartItem == null)
            {
                return BadRequest("Invalid request.");
            }
            var bookData = await ConsumeBookResponse(cartItem.BookId);
            var newItem = new CartItem()
            {
                BookId = cartItem.BookId,
                Quantity = cartItem.Quantity,
                UnitPrice = 20,
                Id = Guid.NewGuid().ToString(),
            };
            await _orderProcessingService
                .PlaceOrderAsync(cartItem.BookId, cartItem.CartId, 20);
            return Ok(newItem);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOrderStatus(string cartId)
        {
            if (cartId == null)
            {
                return BadRequest("Invalid cartId");
            }
            try
            {
                var orderStatus = _orderProcessingService
                    .GetOrderStatusAsync(cartId);
                return Ok(orderStatus);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to fetch order status: {ex.Message}");
            }
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
                        var newOrder = await _orderProcessingService.PlaceOrderAsync(bookData, "123", 2);
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