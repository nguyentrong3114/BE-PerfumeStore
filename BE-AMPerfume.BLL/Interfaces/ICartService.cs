public interface ICartService
{
    Task<CartDTO> GetCartByUserIdAsync(int id);
    Task AddCartAsync(CartDTO cart);
    Task UpdateCartAsync(CartDTO cart);
    Task DeleteCartAsync(int id);
    Task<List<Cart>> GetAllCartsAsync();
}