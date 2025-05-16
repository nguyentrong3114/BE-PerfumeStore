
namespace BE_AMPerfume.API.Controllers
{

    using Microsoft.AspNetCore.Mvc;

    [Route("api/products")]
    
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        // GET /api/products
        [HttpGet]
        public async Task<IActionResult> GetAllProducts(
            [FromQuery] string? gender = null,
            [FromQuery] string? brand = null,
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null,
            [FromQuery] string? sortBy = null)
        {
            var products = await _productService.GetAllProductAsync(gender, brand, minPrice, maxPrice, sortBy);
            return Ok(products);
        }

        // GET /api/products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
    }
}