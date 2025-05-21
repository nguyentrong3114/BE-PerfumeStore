public interface ICartItemService
{
    Task AddCartItemAsync(int cartId,CRUSCartItemDTO  cartItem);
    Task UpdateCartItemAsync(int cartId,CRUSCartItemDTO cartItem);
    Task DeleteCartItemAsync(int cartId,CRUSCartItemDTO cartItem);
    Task<CartItems> GetCartItemByIdAsync(int id);
    Task<List<CartItems>> GetAllCartItemsAsync();
}