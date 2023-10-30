namespace InventoryManagementDomain.Entities
{
    public class InventoryItem : BaseEntity
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}