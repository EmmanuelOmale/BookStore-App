namespace BookStoreDomain.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public string? ISBN { get; set; } 
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Description { get; set; } = "";
        public DateTime PublicationDate { get; set; }
        public DateTime LastUpdatedDate { get; set;}

    }
}