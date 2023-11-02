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
        
        private readonly OrderContext _order;
        private readonly IMapper _mapper;

        public OrderProcessingService(OrderContext order, IMapper mapper)
        {

            _order = order;
            _mapper = mapper;
        }
        public async Task<CartItem> PlaceOrderAsync(string bookid, string cartid, int quantity, decimal price)
        {
            
            var cart = new CartItem()
            {
                BookId = bookid,
                Quantity = quantity,
                UnitPrice = price,
            };
            _order.Add(cart);
            await _order.SaveChangesAsync();

            
            return cart;
        }
    }
}