using AutoMapper;
using Microsoft.AspNetCore.Http;
public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartService(ICartRepository cartRepository, IProductRepository productRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public Task AddCartAsync(CartDTO cart)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCartAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Cart>> GetAllCartsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CartDTO> GetCartAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Cart> GetCartByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<CartDTO> GetCartByUserIdAsync(int userId)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        return _mapper.Map<CartDTO>(cart);
    }


    public Task UpdateCartAsync(CartDTO cart)
    {
        throw new NotImplementedException();
    }
}