using Microsoft.EntityFrameworkCore;
using OrderProcessingDomain.Entities;
using OrderProcessingPersistence.Data;
using static OrderProcessingDomain.Entities.Enum;

namespace OrderProcessingPersistence.Repository
{
    public class OrderProcessingRepository : IOrderProcessingRepository
    {
        private readonly OrderContext _orderContext;
        public OrderProcessingRepository(OrderContext orderContext)
        {
            _orderContext = orderContext;   
        }
        
        public async Task PlaceOrderAsync(CartItem cartItem)
        {
            _orderContext.CartItems.Add(cartItem);
            await _orderContext.SaveChangesAsync();
        }

        public async Task<OrderStatus> GetOrderStatusAsync(string cartId)
        {
            var getCart = await _orderContext
                 .Carts
                 .FirstOrDefaultAsync(c => c.Id == cartId);
            if (getCart != null)
            {
                return getCart.OrderStatus;
            }
            return OrderStatus.Unknown;
        }
    }
}
