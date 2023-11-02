using InventoryManagementDomain.Entities;

namespace InventoryManagementApplication.Services
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryItem>> GetAllItemsAsync();
        Task<InventoryItem> GetItemByIdAsync(string itemId);
        Task UpdateProductQuantityAsync(string itemId, InventoryItemDto quantity);
    }
}
