using BE_AMPerfume.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
// [Authorize(Roles = "Admin")]
[ApiController]
[Route("api/adm/orders")]
public class OrderManagementController : Controller
{
    private readonly IPaymentService _paymentService;
    public OrderManagementController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllProductAdmin([FromQuery] int page = 1, [FromQuery] int size = 10)
    {
        var result = await _paymentService.GetAllOrderAdminAsync(page, size);
        return Ok(result);
    }
}