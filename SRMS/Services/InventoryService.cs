using SRMS.Models;
using SRMS.Repositories;

namespace SRMS.Services
{
    public class InventoryService
    {
        private readonly ProductRepository _productRepository;

        public InventoryService()
        {
            _productRepository = new ProductRepository();
        }

        public async Task<List<Product>> GetAvailableStockAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }
    }
}
