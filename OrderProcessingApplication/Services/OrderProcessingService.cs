using AutoMapper;
using OrderProcessingDomain.Entities;
using OrderProcessingDomain.Entities.Dtos;
using OrderProcessingPersistence.Data;
using OrderProcessingPersistence.Repository;
using static OrderProcessingDomain.Entities.Enum;

namespace OrderProcessingApplication.Services
{
    public class OrderProcessingService : IOrderProcessingService
    {
        
        private readonly IOrderProcessingRepository _order;
        private readonly IMapper _mapper;

        public OrderProcessingService(IOrderProcessingRepository order, IMapper mapper)
        {
            _order = order;
            _mapper = mapper;
        }
        public async Task<CartItem> PlaceOrderAsync(string bookid, string cartid, int quantity)
        {
            
            var cart = new CartItem()
            {
                BookId = bookid,
                Quantity = quantity,
            };
            await _order.PlaceOrderAsync(cart);
            return cart;
        }

        public async Task<OrderStatus> GetOrderStatusAsync(string cartId)
        {
            return await _order
                .GetOrderStatusAsync(cartId);
        }
    }
}