using static OrderProcessingDomain.Entities.Enum;

namespace OrderProcessingDomain.Entities
{
    public class Cart 
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; } 
        public List<CartItem> OrderItems { get; set;} 
        
    }
}