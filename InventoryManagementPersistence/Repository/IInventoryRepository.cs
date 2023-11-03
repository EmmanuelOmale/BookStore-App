using InventoryManagementDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementPersistence.Repository
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<InventoryItem>> GetAllAvailableItemsAsync();
        Task<InventoryItem> GetAvailableItemByIdAsync(string id);
        Task UpdateItemQuantityAsync(InventoryItem item);
    }
}
