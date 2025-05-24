using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/admin/[controller]")]
public class ProductManagementController : Controller
{
    private readonly IProductService _productService;
    public ProductManagementController(IProductService productService)
    {
        _productService = productService;
    }
    
}