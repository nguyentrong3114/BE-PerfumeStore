public interface IProductService
{
    Task<List<ProductDTO>> GetAllProductAsync();
    Task<ProductDTO> GetProductByIdAsync(int id);
    Task<ProductDTO> CreateAsync(ProductDTO productDto);
    Task<ProductDTO> UpdateAsync(int id, ProductDTO productDto);
    Task<bool> DeleteAsync(int id);
}