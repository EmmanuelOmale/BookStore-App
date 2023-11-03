using OrderProcessingDomain.Entities;
using static OrderProcessingDomain.Entities.Enum;

namespace OrderProcessingPersistence.Repository
{
    public interface IOrderProcessingRepository
    {
        Task PlaceOrderAsync(CartItem order);
        Task<OrderStatus> GetOrderStatusAsync(string orderId);
    }
}
