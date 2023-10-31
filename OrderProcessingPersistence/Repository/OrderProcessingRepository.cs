using OrderProcessingDomain.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessingPersistence.Repository
{
    public class OrderProcessingRepository : IOrderProcessingRepository
    {
        public Task<CartDto> GetOrderStatusAsync(string orderId)
        {
            throw new NotImplementedException();
        }

        public Task<CartDto> PlaceOrderAsync(string customerId, List<CartItemDto> items)
        {
            throw new NotImplementedException();
        }
    }
}
