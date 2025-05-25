using BE_AMPerfume.Core.Models;

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(int id);
    Task<List<Product>> SearchAsync(string keyword);
    Task<List<Product>> GetAllProductAsync(string? gender, string? category, decimal? priceMin, decimal? priceMax, string? notes);
    Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
    Task<IEnumerable<Product>> GetProductsByBrandIdAsync(int brandId);
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);
    //Admin
    Task<IEnumerable<Product>> GetAllProductsAdmin(); 
}