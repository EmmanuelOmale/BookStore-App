using OrderProcessingDomain.Entities;
using OrderProcessingDomain.Entities.Dtos;
using OrderProcessingPersistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessingPersistence.Repository
{
    public class OrderProcessingRepository : IOrderProcessingRepository
    {
        private readonly OrderContext _orderContext;
        public OrderProcessingRepository(OrderContext orderContext)
        {
            _orderContext = orderContext;   
        }
        
        public async Task PlaceOrderAsync(CartItem order)
        {
            _orderContext.CartItems.Add(order);
            await _orderContext.SaveChangesAsync();
        }
    }
}
