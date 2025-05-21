
using AutoMapper;
using BE_AMPerfume.DAL.Interfaces;

public class CartItemService : ICartItemService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CartItemService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task AddCartItemAsync(int cartId, CRUSCartItemDTO cartItem)
    {
        if (cartId < 1)
            return;

        if (cartItem == null)
            throw new ArgumentNullException(nameof(cartItem));

        var existingItem = await _unitOfWork.CartItemsRepository
            .GetExistingCartItemAsync(cartId, cartItem.ProductVariantId);

        if (existingItem != null)
        {
            existingItem.Quantity += cartItem.Quantity;
            _unitOfWork.CartItemsRepository.UpdateCartItemQuantity(existingItem);

        }
        else
        {
            var entity = new CartItems
            {
                ProductVariantId = cartItem.ProductVariantId,
                Quantity = cartItem.Quantity,
                CartId = cartId,
            };

            await _unitOfWork.CartItemsRepository.AddToCartAsync(entity);
        }

        await _unitOfWork.SaveChangesAsync();
    }


    public async Task DeleteCartItemAsync(int cartId, CRUSCartItemDTO cartItem)
    {
        if (cartId < 1)
            return;

        if (cartItem == null)
            throw new ArgumentNullException(nameof(cartItem));

        if (cartItem.ProductVariantId <= 0)
            throw new ArgumentException("ProductVariantId không được nhỏ hơn hoặc bằng 0.", nameof(cartItem));

        var existingItem = await _unitOfWork.CartItemsRepository
            .GetExistingCartItemAsync(cartId, cartItem.ProductVariantId);
        if (existingItem == null)
            throw new InvalidOperationException("Sản phẩm không tồn tại trong giỏ hàng.");
        _unitOfWork.CartItemsRepository.DeleteItemFromCart(cartId, existingItem.ProductVariantId.Value);
        await _unitOfWork.SaveChangesAsync();
    }

    public Task<List<CartItems>> GetAllCartItemsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CartItems> GetCartItemByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateCartItemAsync(int cartId, CRUSCartItemDTO cartItem)
    {
        if (cartId < 1)
            return;

        if (cartItem == null)
            throw new ArgumentNullException(nameof(cartItem));

        var existingItem = await _unitOfWork.CartItemsRepository
            .GetExistingCartItemAsync(cartId, cartItem.ProductVariantId);

        if (existingItem != null)
        {
            existingItem.Quantity = cartItem.Quantity;
            _unitOfWork.CartItemsRepository.UpdateCartItemQuantity(existingItem);

        }
        else
        {
            var entity = new CartItems
            {
                ProductVariantId = cartItem.ProductVariantId,
                Quantity = cartItem.Quantity,
                CartId = cartId,
            };

            await _unitOfWork.CartItemsRepository.AddToCartAsync(entity);
        }

        await _unitOfWork.SaveChangesAsync();
    }

}
