using AutoMapper;
using OrderProcessingDomain.Entities;
using OrderProcessingDomain.Entities.Dtos;
using OrderProcessingPersistence.Repository;
using static OrderProcessingDomain.Entities.Enum;

namespace OrderProcessingApplication.Services
{
    public class OrderProcessingService : IOrderProcessingService
    {
        private readonly IOrderProcessingRepository _orderProcessingRepository;
        private readonly IMapper _mapper;

        public OrderProcessingService(IOrderProcessingRepository orderProcessingRepository, IMapper mapper)
        {
            _orderProcessingRepository = orderProcessingRepository;
            _mapper = mapper;
        }
        public async Task<CartDto> PlaceOrderAsync(string customerId, List<CartItemDto> items)
        {
         
            var cartItems = _mapper.Map<List<CartItemDto>>(items);

            var cart = new CartDto
            {
                CustomerId = customerId,
                OrderDate = DateTime.Now,
                OrderItems = cartItems,
                OrderStatus = OrderStatus.Pending.ToString(),

            };
            return cart;
        }
    }
}