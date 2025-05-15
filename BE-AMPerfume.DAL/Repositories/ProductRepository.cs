using BE_AMPerfume.Core.Models;
using BE_AMPerfume.DAL.Data;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly AMPerfumeDbContext _context;

    public ProductRepository(AMPerfumeDbContext context)
    {
        _context = context;
    }

    public Task AddProductAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProductAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Product>> GetAllProductAsync()
    {
        return await _context.Products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .Include(p => p.Notes)
            .ToListAsync();
    }

    public Task<Product> GetProductByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetProductsByBrandIdAsync(int brandId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public Task UpdateProductAsync(Product product)
    {
        throw new NotImplementedException();
    }
}