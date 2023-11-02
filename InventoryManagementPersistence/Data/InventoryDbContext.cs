using InventoryManagementDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementPersistence.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options): base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<InventoryItem> InventoryItems { get; set; }
    }
}