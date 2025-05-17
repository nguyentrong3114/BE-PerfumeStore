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
    [HttpPost("/api/users")]
    public async Task<IActionResult> Register(RegisterDTO dto)
    {
        var result = await _userService.RegisterAsync(dto);

        if (!result.IsSuccess)
            return BadRequest(new { message = result.Message });

        return Ok(new { message = result.Message });
    }

}