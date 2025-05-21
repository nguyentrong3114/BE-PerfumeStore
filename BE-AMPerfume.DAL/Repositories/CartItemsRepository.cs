using BE_AMPerfume.DAL.Data;
using Microsoft.EntityFrameworkCore;

public class CartItemsRepository : ICartItemRepository
{
    private readonly AMPerfumeDbContext _context;

    public CartItemsRepository(AMPerfumeDbContext context)
    {
        _context = context;
    }

    public async Task AddToCartAsync(CartItems cartItem)
    {
        await _context.CartItems.AddAsync(cartItem);
        await _context.SaveChangesAsync();
    }

    public Task ClearCartAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public void DeleteItemFromCart(int cartId, int productVariantId)
    {
        var item = _context.CartItems
        .FirstOrDefault(x => x.CartId == cartId && x.ProductVariantId == productVariantId);
        if (item != null)
        {
            _context.CartItems.Remove(item);
        }
    }

    public Task<CartItems> GetCartItemByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<CartItems?> GetExistingCartItemAsync(int cartId, int productVariantId)
    {
        return await _context.CartItems
            .FirstOrDefaultAsync(x => x.CartId == cartId && x.ProductVariantId == productVariantId);
    }

    public Task RemoveFromCartAsync(int cartItemId)
    {
        throw new NotImplementedException();
    }

    public void UpdateCartItemQuantity(CartItems cartItems)
    {
        _context.Update(cartItems);
    }
}