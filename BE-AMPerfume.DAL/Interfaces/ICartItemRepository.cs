public interface ICartItemRepository
{
    Task<CartItems> GetCartItemByIdAsync(int id);
    Task<CartItems?> GetExistingCartItemAsync(int cartId, int productVariantId);
    

    Task AddToCartAsync(CartItems cartItem);
    Task RemoveFromCartAsync(int cartItemId);
    void UpdateCartItemQuantity(CartItems cartItems);
    void DeleteItemFromCart(int cartId, int productVariantId);
    Task ClearCartAsync(string userId);
}