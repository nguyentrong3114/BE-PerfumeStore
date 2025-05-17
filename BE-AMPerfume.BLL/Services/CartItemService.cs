
using AutoMapper;

public class CartItemService : ICartItemService
{
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IProductVariantRepository _productVariantRepository;
    private readonly IMapper _mapper;

    public CartItemService(ICartItemRepository cartItemRepository, IProductVariantRepository productVariantRepository, IMapper mapper)
    {
        _cartItemRepository = cartItemRepository;
        _productVariantRepository = productVariantRepository;
        _mapper = mapper;
    }

    public Task AddCartItemAsync(AddCartItemDTO cartItem)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCartItemAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<CartItems>> GetAllCartItemsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CartItems> GetCartItemByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCartItemAsync(UpdateCartItemDTO cartItem)
    {
        throw new NotImplementedException();
    }
}
