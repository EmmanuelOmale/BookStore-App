namespace OrderProcessingDomain.Entities.Dtos
{
    public class CartItemDto
    {
        public string BookId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string CartId { get; set; }
    }
}
