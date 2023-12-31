﻿namespace InventoryManagementDomain.Entities
{
    public class InventoryItem 
    {
        public string Id { get; set; }
        public string BookId { get; set; }
        public int Quantity { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}