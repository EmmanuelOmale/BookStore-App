using InventoryManagementApplication.Services;
using InventoryManagementDomain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IInventoryService _inventoryService;
        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _inventoryService.GetAllItemsAsync();
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<InventoryItemDto>> GetProductById(string productId)
        {
            var product = await _inventoryService.GetItemByIdAsync(productId);
            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProductQuantity(string itemId, [FromBody] InventoryItemDto quantity)
        {
            await _inventoryService.UpdateProductQuantityAsync(itemId, quantity);
            return Ok("Product quantity updated.");
        }
    }
}
