using OrderProcessingDomain.Entities;
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
        Task<CartItem> PlaceOrderAsync(string bookid, string cartid, int quantity, decimal price);
    }
}
