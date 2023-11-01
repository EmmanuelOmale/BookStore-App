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
        public async Task<CartDto> PlaceOrderAsync(string bookid, string cartid, int quantity)
        {
            var retrievebook = 200;
            

            var cart = new CartItem()
            {
                BookId = bookid,
                Quantity = quantity,
                UnitPrice = retrievebook

            };
            
            return cart;
        }
    }
}