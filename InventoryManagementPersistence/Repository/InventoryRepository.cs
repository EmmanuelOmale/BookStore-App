using InventoryManagementDomain.Entities;
using InventoryManagementPersistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementPersistence.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly InventoryDbContext _context;
        public InventoryRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<InventoryItem>> GetAllAvailableItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<InventoryItem> GetAvailableItemByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateItemQuantityAsync(InventoryItem item)
        {
            throw new NotImplementedException();
        }
    }
}
