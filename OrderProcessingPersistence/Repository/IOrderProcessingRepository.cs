using OrderProcessingDomain.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessingPersistence.Repository
{
    public interface IOrderProcessingRepository
    {
        Task<CartDto> PlaceOrderAsync(string customerId, List<CartItemDto> items);
        Task<CartDto> GetOrderStatusAsync(string orderId);
    }
}
