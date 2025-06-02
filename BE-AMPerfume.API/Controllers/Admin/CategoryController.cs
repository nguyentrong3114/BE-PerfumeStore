using Microsoft.AspNetCore.Mvc;
[ApiController]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("api/categories")]
    public async Task<IActionResult> GetCategories()
    {
        try
        {
            var categories = await _categoryService.GetAllCategoriesAvailableAsync();
            return Ok(categories);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error");
        }
    }
    [HttpGet("api/adm/categories")]
    public async Task<IActionResult> GetAdminDashBoardCategories()
    {
        try
        {
            var categories = await _categoryService.CategoryDashBoardAdmin();
            return Ok(categories);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error");
        }
    }
}