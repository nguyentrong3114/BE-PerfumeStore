using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BE_AMPerfume.API.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [Authorize]
        [HttpGet()]
        public async Task<IActionResult> GetCart()
        {
            var userId = int.Parse(User.FindFirst("id")!.Value);
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            return Ok(cart);
        }
    }
}