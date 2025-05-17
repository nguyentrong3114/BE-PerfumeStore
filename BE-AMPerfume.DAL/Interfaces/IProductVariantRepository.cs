using BE_AMPerfume.Core.Models;

public interface IProductVariantRepository
{
    Task AddProductVariantAsync(ProductVariant productVariant);
    Task UpdateProductVariantAsync(ProductVariant productVariant);
    Task DeleteProductVariantAsync(int id);
    Task<ProductVariant> GetProductVariantByIdAsync(int id);
    Task<List<ProductVariant>> GetAllProductVariantsAsync();
}