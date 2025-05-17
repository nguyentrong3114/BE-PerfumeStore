public interface ICartItemRepository
{
    Task<CartItems> GetCartItemByIdAsync(int id);
    Task AddToCartAsync(CartItems cartItem);
    Task RemoveFromCartAsync(int cartItemId);
    Task UpdateCartItemQuantityAsync(int cartItemId, int quantity);
    Task ClearCartAsync(string userId);
}