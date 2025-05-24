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

    public async Task<PagedResult<ProductDetailDTO>> GetAllProductAdminAsync(int page, int size)
    {
        var product = await _unitOfWork.ProductRepository.GetAllProductsAdmin();
        var dtos = _mapper.Map<List<ProductDetailDTO>>(product);

        return new PagedResult<ProductDetailDTO>
        {
            Items = dtos,
            PageNumber = page,
            PageSize = size,
            TotalItems = product.Count()
        };

    }

    public async Task<List<ProductDTO>> GetAllProductAsync(string? gender, string? brand, decimal? priceMin, decimal? priceMax, string? notes)
    {
        var products = await _unitOfWork.ProductRepository.GetAllProductAsync(gender, brand, priceMin, priceMax, notes);
        return _mapper.Map<List<ProductDTO>>(products);
    }

    public async Task<ProductDetailDTO> GetProductByIdAsync(int id)
    {
        var product = await _unitOfWork.ProductRepository.GetProductByIdAsync(id);
        return _mapper.Map<ProductDetailDTO>(product);
    }

    public Task<ProductDTO> UpdateAsync(int id, ProductDTO productDto)
    {
        throw new NotImplementedException();
    }
}