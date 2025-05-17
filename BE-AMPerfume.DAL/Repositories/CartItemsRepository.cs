using BE_AMPerfume.DAL.Data;

public class CartItemsRepository : ICartItemRepository
{
    private readonly AMPerfumeDbContext _context;

    public CartItemsRepository(AMPerfumeDbContext context)
    {
        _context = context;
    }

    public Task AddToCartAsync(CartItems cartItem)
    {
        throw new NotImplementedException();
    }

    public Task ClearCartAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<CartItems> GetCartItemByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveFromCartAsync(int cartItemId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCartItemQuantityAsync(int cartItemId, int quantity)
    {
        throw new NotImplementedException();
    }
}