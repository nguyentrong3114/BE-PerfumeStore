using BE_AMPerfume.DAL.Data;
using Microsoft.EntityFrameworkCore;

public class CartRepository : ICartRepository
{
    private readonly AMPerfumeDbContext _context;

    public CartRepository(AMPerfumeDbContext context)
    {
        _context = context;
    }

    public async Task GenerateCartAsync(Cart cart)
    {
        var exists = await _context.Carts.AnyAsync(c => c.UserId == cart.UserId);
        if (!exists)
        {
            await _context.Carts.AddAsync(cart);
        }
    }

    public Task AddProductToCartAsync(int cartId, int productId, int quantity)
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

    public async Task<Cart> GetCartByUserIdAsync(int userId)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.ProductVariant)
            .ThenInclude(p => p.Product)
            .ThenInclude(pi => pi.ProductImages)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        return cart!;
    }

    public Task<Cart> GetCartByUserIdAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveProductFromCartAsync(int cartId, int productId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCartAsync(Cart cart)
    {
        throw new NotImplementedException();
    }
}