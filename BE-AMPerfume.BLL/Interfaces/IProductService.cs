public interface IProductService
{
    Task<List<ProductDTO>> GetAllProductAsync(string? gender, string? category, decimal? priceMin, decimal? priceMax, string? notes);
    Task<ProductDTO> GetProductByIdAsync(int id);
    Task<ProductDTO> CreateAsync(ProductDTO productDto);
    Task<ProductDTO> UpdateAsync(int id, ProductDTO productDto);
    Task<bool> DeleteAsync(int id);
}