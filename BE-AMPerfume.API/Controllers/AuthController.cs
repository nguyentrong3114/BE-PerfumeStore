using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Cookies;
using AspNet.Security.OAuth.GitHub;

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
            if (result == null)
            {
                return Unauthorized(new { message = result?.Message ?? "Đăng nhập thất bại" });
            }

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
                return Unauthorized("Token không hợp lệ hoặc không chứa ClaimTypes.Email");

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
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Response.Cookies.Delete("token");
            return Ok(new { message = "Đăng xuất thành công" });
        }
        [HttpGet("google")]
        public IActionResult GoogleLogin()
        {
            var props = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(ExternalLoginCallback), new { provider = "Google" })!
            };
            return Challenge(props, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("facebook")]
        public IActionResult FacebookLogin()
        {
            var props = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(ExternalLoginCallback), new { provider = "Facebook" })!
            };
            return Challenge(props, FacebookDefaults.AuthenticationScheme);
        }
        [HttpGet("github")]
        public IActionResult LoginWithGitHub()
        {
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(ExternalLoginCallback), new { provider = "GitHub" })!
            }, GitHubAuthenticationDefaults.AuthenticationScheme);
        }

        [HttpGet("callback")]
        public async Task<IActionResult> ExternalLoginCallback(string provider)
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded)
                return Unauthorized();

            var token = await _authService.HandleExternalLoginAsync(result.Principal);

            HttpContext.Response.Cookies.Append("token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddHours(1)
            });

            return Redirect("http://localhost:3000/");
        }


    }
}
