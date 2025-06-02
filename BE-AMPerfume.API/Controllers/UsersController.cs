using Microsoft.AspNetCore.Mvc;

using BE_AMPerfume.DAL.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

[ApiController]

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

    // POST /api/users
    [HttpPost("/api/users")]
    public async Task<IActionResult> Register(RegisterDTO dto)
    {
        var result = await _userService.RegisterAsync(dto);
        if (!result.IsSuccess)
            return BadRequest(new { message = result.Message });
        
        return Ok(new { message = result.Message });
    }
    // [HttpGet("/api/users/me")]
    [Authorize]
    [HttpGet("/api/users/me")]
    public async Task<IActionResult> GetUserAsync()
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        if (string.IsNullOrEmpty(email))
            return Unauthorized("Token không hợp lệ hoặc thiếu email.");

        var user = await _userService.GetUserAsync(email);

        if (user == null)
            return NotFound("Không tìm thấy người dùng.");

        return Ok(user);
    }
    [Authorize]
    [HttpPut("api/users/me/password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO dto)
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        if (string.IsNullOrEmpty(email))
            return Unauthorized(new { message = "Token không hợp lệ." });

        var result = await _userService.ChangePasswordAsync(email, dto);

        if (result == false)
            return BadRequest(new { message = "Mật khẩu cũ không đúng." });

        return Ok(new { message = "Đổi mật khẩu thành công." });
    }
    [HttpPost("api/users/me/send-otp")]
    public async Task<IActionResult> SendVerification([FromBody] IsVerifyDTO email)
    {
        var user = await _userService.GetUserAsync(email.Email);
        if (user == null)
            return NotFound("Người dùng không tồn tại");

        var otp = new Random().Next(100000, 999999).ToString();
        await _emailService.SaveOtpAsync(email.Email, otp);
        await _emailService.SendVerificationCodeAsync(email.Email, (string)user.Name, otp);

        return Ok("OTP đã được gửi về email.");
    }

    [HttpPost("api/users/me/verify")]
    public async Task<IActionResult> ConfirmVerification([FromBody] IsVerifyDTO isVerify)
    {

        var isValid = await _emailService.VerifyOtpAsync(isVerify.Email, isVerify.Otp);
        if (!isValid)
            return BadRequest("Mã OTP không đúng hoặc đã hết hạn.");

        var updated = await _userService.MarkUserAsVerifiedAsync(isVerify.Email);
        if (!updated)
            return StatusCode(500, "Không thể xác minh tài khoản.");

        return Ok("Tài khoản đã được xác minh.");
    }
}