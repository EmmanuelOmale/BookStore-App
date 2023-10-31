namespace OrderProcessingDomain.Entities
{
    public class Cart 
    {
        public string Id { get; set; }
        public string BookId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public List<CartItem> OrderItems { get; set;} 
        
    }
}