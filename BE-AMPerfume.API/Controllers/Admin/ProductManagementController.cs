using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
// [Authorize(Roles = "Admin")]
[ApiController]
[Route("api/adm/products")]
public class ProductManagementController : Controller
{
    private readonly IProductService _productService;
    public ProductManagementController(IProductService productService)
    {
        _productService = productService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllProductAdmin([FromQuery] int page = 1, [FromQuery] int size = 10)
    {
        var result = await _productService.GetAllProductAdminAsync(page, size);
        return Ok(result);
    }
}