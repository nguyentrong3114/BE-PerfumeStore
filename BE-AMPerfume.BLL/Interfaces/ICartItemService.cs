public interface ICartItemService
{
    Task AddCartItemAsync(AddCartItemDTO  cartItem);
    Task UpdateCartItemAsync(UpdateCartItemDTO cartItem);
    Task DeleteCartItemAsync(int id);
    Task<CartItems> GetCartItemByIdAsync(int id);
    Task<List<CartItems>> GetAllCartItemsAsync();
}