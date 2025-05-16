using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Security.Claims;

namespace BE_AMPerfume.API.Controllers
{
    [ApiController]
    [Route("api/session")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        // POST /api/session  Đăng nhập
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var result = await _authService.LoginAsync(dto);
            if (result == null )
            {
                return Unauthorized(new { message = result?.Message ?? "Đăng nhập thất bại" });
            }
            // Set token vào HttpOnly cookie
            if (!string.IsNullOrEmpty(result.Token))
            {
                HttpContext.Response.Cookies.Append("token", result.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false, //dev
                    SameSite = SameSiteMode.Lax,
                    Expires = DateTime.UtcNow.AddHours(1)
                });
            }
            else
            {
                return StatusCode(500, "Token generation failed.");
            }

            return Ok(new
            {
                fullName = result.FullName,
                email = result.Email,
                message = "Đăng nhập thành công"
            });
        }

        // GET /api/session  Kiểm tra đăng nhập (isLogin)
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> IsLogin()
        {

            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
                return Unauthorized("Token không hợp lệ hoặc không chứa ClaimTypes.Name");

            var result = await _authService.IsLogin(email);

            if (result == null)
                return NotFound("Không tìm thấy người dùng");

            return Ok(new
            {
                email = result.Email,
                fullName = result.FullName,
                message = "Đã đăng nhập"
            });
        }
        [HttpDelete()]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("token");
            return Ok(new { message = "Đăng xuất thành công" });
        }

    }
}
