namespace OrderProcessingDomain.Entities
{
    public class Order : BaseEntity
    {
        public int BookId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; } = string.Empty;
        public List<OrderItem> Items { get; set;} 
        
    }
}