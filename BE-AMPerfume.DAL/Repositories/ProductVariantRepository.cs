using BE_AMPerfume.Core.Models;
using BE_AMPerfume.DAL.Data;
using Microsoft.EntityFrameworkCore;

public class ProductVariantRepository : IProductVariantRepository
{
    private readonly AMPerfumeDbContext _context;

    public ProductVariantRepository(AMPerfumeDbContext context)
    {
        _context = context;
    }

    public Task AddProductVariantAsync(ProductVariant productVariant)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProductVariantAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<ProductVariant>> GetAllProductVariantsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ProductVariant?> GetProductVariantByIdAsync(int id)
    {
        return await _context.ProductVariants
            .Include(p => p.Product)
            .Include(p => p.Size)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public Task UpdateProductVariantAsync(ProductVariant productVariant)
    {
        throw new NotImplementedException();
    }
}