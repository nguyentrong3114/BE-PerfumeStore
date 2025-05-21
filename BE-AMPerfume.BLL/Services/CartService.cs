using AutoMapper;
using BE_AMPerfume.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
public class CartService : ICartService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartService(IMapper mapper, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task GenerateCartAsync(CartDTO cart)
    {
        var entity = new Cart
        {
            UserId = cart.UserId,
            CreatedAt = DateTime.UtcNow,
        };

        await _unitOfWork.CartRepository.GenerateCartAsync(entity);
        await _unitOfWork.SaveChangesAsync();
    }



    public Task DeleteCartAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Cart>> GetAllCartsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CartDTO> GetCartAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Cart> GetCartByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<CartDTO> GetCartByUserIdAsync(int userId)
    {
        var cart = await _unitOfWork.CartRepository.GetCartByUserIdAsync(userId);

        if (cart == null)
        {
            var newCart = new Cart
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
            };

            await _unitOfWork.CartRepository.GenerateCartAsync(newCart);
            await _unitOfWork.SaveChangesAsync(); 

            cart = newCart;
        }

        return _mapper.Map<CartDTO>(cart);
    }


    public Task UpdateCartAsync(CartDTO cart)
    {
        throw new NotImplementedException();
    }
}