using OrderProcessingDomain.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessingApplication.Services
{
    public interface IOrderProcessingService
    {
        Task<CartDto> PlaceOrderAsync(string customerId, List<CartItemDto> items);

    }
}
