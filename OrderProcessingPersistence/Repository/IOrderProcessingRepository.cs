using OrderProcessingDomain.Entities;
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
        Task PlaceOrderAsync(Cart order);
        Task GetOrderStatusAsync(string orderId);
    }
}
