using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BE_AMPerfume.API.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICartItemService _cartItemSerice;
        public CartController(ICartService cartService, ICartItemService cartItemService)
        {
            _cartService = cartService;
            _cartItemSerice = cartItemService;
        }

        [Authorize]
        [HttpGet()]
        public async Task<IActionResult> GetCart()
        {
            var userId = int.Parse(User.FindFirst("id")!.Value);
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            return Ok(cart);
        }
        [Authorize]
        [HttpPost()]
        public async Task<IActionResult> AddProductToCart([FromBody] CRUSCartItemDTO addCartItemDTO)
        {
            var userId = int.Parse(User.FindFirst("id")!.Value);

            var cart = await _cartService.GetCartByUserIdAsync(userId);
            if (cart == null)
                return NotFound("Không tìm thấy giỏ hàng cho người dùng.");

            await _cartItemSerice.AddCartItemAsync(cart.Id, addCartItemDTO);

            return Ok(new
            {
                message = "Đã thêm sản phẩm vào giỏ hàng."
            });
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> ChangeQuantityCartItem([FromBody] CRUSCartItemDTO updateCartItemDTO)
        {
            var userId = int.Parse(User.FindFirst("id")!.Value);

            var cart = await _cartService.GetCartByUserIdAsync(userId);
            if (cart == null)
                return NotFound("Không tìm thấy giỏ hàng cho người dùng.");

            await _cartItemSerice.UpdateCartItemAsync(cart.Id, updateCartItemDTO);

            return Ok(new
            {
                message = "Đã thêm sản phẩm vào giỏ hàng."
            });

        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> RemoveProductFromCart([FromBody] CRUSCartItemDTO deleteCartItemDTO)
        {
            var userId = int.Parse(User.FindFirst("id")!.Value);

            var cart = await _cartService.GetCartByUserIdAsync(userId);
            if (cart == null)
                return NotFound("Không tìm thấy giỏ hàng cho người dùng.");

            await _cartItemSerice.DeleteCartItemAsync(cart.Id, deleteCartItemDTO);

            return Ok(new
            {
                message = "Đã thêm sản phẩm vào giỏ hàng."
            });
        }

    }
}