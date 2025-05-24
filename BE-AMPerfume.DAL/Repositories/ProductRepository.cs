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

    public async Task<List<Product>> GetAllProductAsync(
        string? gender = null,
        string? brand = null,
        decimal? priceMin = null,
        decimal? priceMax = null,
        string? notes = null
    )
    {
        var query = _context.Products
            .Include(p => p.ProductImages)
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .Include(p => p.Notes)
            .Include(p => p.Variants)
            .AsQueryable();

        if (!string.IsNullOrEmpty(gender))
            query = query.Where(p => p.Gender == gender);

        if (!string.IsNullOrEmpty(brand))
            query = query.Where(p => p.Brand.Name == brand);

        if (priceMin.HasValue)
            query = query.Where(p => p.Variants.Any(v => v.Price >= priceMin.Value));

        if (priceMax.HasValue)
            query = query.Where(p => p.Variants.Any(v => v.Price <= priceMax.Value));

        if (!string.IsNullOrEmpty(notes))
            query = query.Where(p =>
                p.Notes.Top.Contains(notes) ||
                p.Notes.Middle.Contains(notes) ||
                p.Notes.Base.Contains(notes)
            );

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetAllProductsAdmin()
    {
        var product = await _context.Products
            .Include(p => p.ProductImages)
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .Include(p => p.Variants)
            .ToListAsync();

        if (product == null)
            throw new InvalidOperationException($"Error or no products");

        return product;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        var product = await _context.Products
            .Include(p => p.ProductImages)
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .Include(p => p.Notes)
            .Include(p => p.Variants)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            throw new InvalidOperationException($"Product with id {id} not found.");

        return product;
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