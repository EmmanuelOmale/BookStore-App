using InventoryManagementDomain.Entities;
using InventoryManagementPersistence.Repository;

namespace InventoryManagementApplication.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        public Task<IEnumerable<InventoryItem>> GetAllItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<InventoryItem> GetItemByIdAsync(string itemId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductQuantityAsync(string itemId, InventoryItemDto quantity)
        {
            throw new NotImplementedException();
        }
    }
}
