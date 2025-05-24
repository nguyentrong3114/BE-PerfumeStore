using BE_AMPerfume.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/adm/users")]
public class UserManagemantController : Controller
{
    private readonly IUserService _userService;
    public UserManagemantController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllUsersAsync([FromQuery] int page = 1, [FromQuery] int size = 10)
    {
        var result = await _userService.GetAllAsync(page, size);
        return Ok(result);
    }
    
}