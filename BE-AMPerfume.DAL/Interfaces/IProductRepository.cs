using BE_AMPerfume.Core.Models;

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(int id);
    Task<List<Product>> GetAllProductAsync();
    Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
    Task<IEnumerable<Product>> GetProductsByBrandIdAsync(int brandId);
    Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm);
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);
}