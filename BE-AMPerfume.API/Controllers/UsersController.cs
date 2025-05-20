using Microsoft.AspNetCore.Mvc;

using BE_AMPerfume.DAL.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

[ApiController]

public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
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
}