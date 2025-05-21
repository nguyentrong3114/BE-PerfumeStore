public interface ICartRepository
{
    Task<Cart> GetCartByUserIdAsync(int userId);

    Task GenerateCartAsync(Cart cart);
    Task UpdateCartAsync(Cart cart);
    Task DeleteCartAsync(int id);
}