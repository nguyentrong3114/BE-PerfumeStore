using BE_AMPerfume.BLL.Interfaces;
using BE_AMPerfume.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/payments")]
public class PaymentController : Controller
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    [Authorize]
    [HttpPost()]
    public async Task<IActionResult> CreatePaymentForUsers([FromBody] PaymentDTO paymentDTO)
    {
        try
        {
            var userIdStr = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out var userId))
            {
                return Unauthorized("Không thể xác định người dùng.");
            }

            await _paymentService.CreatePaymentAsync(userId, paymentDTO);
            return Ok(new { message = "Tạo thanh toán thành công" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Lỗi khi tạo thanh toán", error = ex.Message });
        }
    }

}