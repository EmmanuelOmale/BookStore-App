using OrderProcessingDomain.Entities;

namespace OrderProcessingPersistence.Repository
{
    public interface IOrderProcessingRepository
    {
        Task PlaceOrderAsync(CartItem order);
    }
}
