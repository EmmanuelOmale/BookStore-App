using Microsoft.AspNetCore.Mvc;
using OrderProcessingApplication.Services;
using OrderProcessingDomain.Entities.Dtos;
using System.Net;

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

            //return Created($"/api/orders/{Order.Id}", newOrder);
            return Ok(newOrder);
        }
    }
}
