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
        string? categorySlug = null,
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

        // 🔍 Tìm categoryId từ slug
        int? categoryId = null;
        if (!string.IsNullOrEmpty(categorySlug) && categorySlug.ToLower() != "all")
        {
            categoryId = await _context.Categories
                .Where(c => c.Slug == categorySlug)
                .Select(c => (int?)c.Id)
                .FirstOrDefaultAsync();
            if (categoryId == null)
                return new List<Product>();
        }
        if (categoryId.HasValue)
            query = query.Where(p => p.CategoryId == categoryId.Value);

        if (!string.IsNullOrEmpty(brand) && brand.ToLower() != "all")
            query = query.Where(p => p.Brand.Name == brand);

        if (priceMin.HasValue)
            query = query.Where(p => p.Variants.Any(v => v.Price >= priceMin.Value));

        if (priceMax.HasValue)
            query = query.Where(p => p.Variants.Any(v => v.Price <= priceMax.Value));

        if (!string.IsNullOrEmpty(notes) && notes.ToLower() != "all")
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

    public async Task<List<Product>> SearchAsync(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return new List<Product>();

        keyword = keyword.Trim().ToLower();

        var products = await _context.Products
            .Where(p => p.Name.ToLower().Contains(keyword))
            .Include(p => p.ProductImages)
            .ToListAsync();

        foreach (var product in products)
        {
            product.ProductImages = product.ProductImages
                .Where(img => img.IsThumbnail)
                .ToList();
        }
        return products;
    }

    public Task UpdateProductAsync(Product product)
    {
        throw new NotImplementedException();
    }
}