using Microsoft.AspNetCore.Mvc;

using BE_AMPerfume.DAL.Interfaces;

[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // POST /api/users
    [HttpPost("api/users")]
    public async Task<IActionResult> Register(RegisterDTO dto)
    {
        var success = await _userService.RegisterAsync(dto);
        if (!success)
            return BadRequest("Email already registered.");

        return Ok("User registered successfully.");
    }
}