using Microsoft.AspNetCore.Mvc;
using BE_AMPerfume.DAL.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace BE_AMPerfume.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly ICartService _cartService;

        public UserController(IUserService userService, IEmailService emailService, ICartService cartService)
        {
            _emailService = emailService;
            _userService = userService;
            _cartService = cartService;
        }

        /// <summary>
        /// Đăng ký người dùng mới
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            var result = await _userService.RegisterAsync(dto);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }

        /// <summary>
        /// Lấy thông tin người dùng hiện tại (yêu cầu xác thực)
        /// </summary>
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetUserAsync()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
                return Unauthorized(new { message = "Token không hợp lệ hoặc thiếu email." });

            var user = await _userService.GetUserAsync(email);

            if (user == null)
                return NotFound(new { message = "Không tìm thấy người dùng." });

            return Ok(user);
        }

        /// <summary>
        /// Đổi mật khẩu người dùng hiện tại
        /// </summary>
        [Authorize]
        [HttpPut("me/password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO dto)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
                return Unauthorized(new { message = "Token không hợp lệ." });

            var result = await _userService.ChangePasswordAsync(email, dto);

            if ((bool)!result)
                return BadRequest(new { message = "Mật khẩu cũ không đúng." });

            return Ok(new { message = "Đổi mật khẩu thành công." });
        }

        /// <summary>
        /// Gửi mã xác minh (OTP) đến email
        /// </summary>
        [HttpPost("me/send-otp")]
        public async Task<IActionResult> SendVerification([FromBody] IsVerifyDTO dto)
        {
            var user = await _userService.GetUserAsync(dto.Email);
            if (user == null)
                return NotFound(new { message = "Người dùng không tồn tại." });

            var otp = new Random().Next(100000, 999999).ToString();
            await _emailService.SaveOtpAsync(dto.Email, otp);
            await _emailService.SendVerificationCodeAsync(dto.Email, (string)user.Name, otp);

            return Ok(new { message = "OTP đã được gửi về email." });
        }

        /// <summary>
        /// Xác minh OTP và đánh dấu người dùng đã xác thực
        /// </summary>
        [HttpPost("me/verify")]
        public async Task<IActionResult> ConfirmVerification([FromBody] IsVerifyDTO dto)
        {
            var isValid = await _emailService.VerifyOtpAsync(dto.Email, dto.Otp);
            if (!isValid)
                return BadRequest(new { message = "Mã OTP không đúng hoặc đã hết hạn." });

            var updated = await _userService.MarkUserAsVerifiedAsync(dto.Email);
            if (!updated)
                return StatusCode(500, new { message = "Không thể xác minh tài khoản." });

            return Ok(new { message = "Tài khoản đã được xác minh." });
        }
    }
}
