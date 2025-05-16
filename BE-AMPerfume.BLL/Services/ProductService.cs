using AutoMapper;
using BE_AMPerfume.DAL.Interfaces;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _productRepository = unitOfWork.ProductRepository;
    }

    public Task<ProductDTO> CreateAsync(ProductDTO productDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ProductDTO>> GetAllProductAsync(string? gender, string? category, decimal? priceMin, decimal? priceMax, string? notes)
    {
        var products = await _unitOfWork.ProductRepository.GetAllProductAsync(gender, category, priceMin, priceMax, notes);
        return _mapper.Map<List<ProductDTO>>(products);
    }

    public Task<ProductDTO> GetProductByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ProductDTO> UpdateAsync(int id, ProductDTO productDto)
    {
        throw new NotImplementedException();
    }
}