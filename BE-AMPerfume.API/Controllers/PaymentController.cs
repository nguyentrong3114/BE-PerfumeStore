using BE_AMPerfume.BLL.Interfaces;
using BE_AMPerfume.Core.DTOs;
using BE_AMPerfume.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/payments")]
public class PaymentController : Controller
{
    private readonly IPaymentService _paymentService;
    private readonly IPaymentDetailService _paymentDetailService;

    public PaymentController(IPaymentService paymentService, IPaymentDetailService paymentDetailService)
    {
        _paymentService = paymentService;
        _paymentDetailService = paymentDetailService;
    }
    [Authorize]
    [HttpPost()]
    public async Task<IActionResult> CreatePaymentForUsers([FromBody] CreatePaymentRequestDTO request)
    {
        try
        {
            var userIdStr = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out var userId))
            {
                return Unauthorized("Không thể xác định người dùng.");
            }

            var paymentId = await _paymentService.CreatePaymentWithDetailsAsync(userId, request.Payment, request.Items);

            return Ok(new { message = "Tạo thanh toán thành công", paymentId });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Lỗi khi tạo thanh toán", error = ex.Message });
        }
    }
    [HttpPost("unknown")]
    public async Task<IActionResult> CreatePaymentForUnknowUsers([FromBody] CreatePaymentRequestDTO request)
    {
        try
        {
            var paymentId = await _paymentService.CreatePaymenWithDetailsByUnknowAsync(request.Payment, request.Items);

            return Ok(new { message = "Tạo thanh toán thành công", paymentId });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Lỗi khi tạo thanh toán", error = ex.Message });
        }
    }
    [Authorize]
    [HttpGet("user/me")]
    public async Task<IActionResult> GetPaymentByUserId()
    {
        try
        {
            var userIdStr = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out var userId))
            {
                return Unauthorized("Không thể xác định người dùng.");
            }

            var payment = await _paymentService.GetAllPaymentAByUserIdAsync(userId);
            if (payment == null)
            {
                return NotFound("Không tìm thấy thanh toán cho người dùng này.");
            }

            return Ok(payment);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Lỗi khi lấy thông tin thanh toán", error = ex.Message });
        }
    }
    [Authorize]
    [HttpPost("order")]
    public async Task<IActionResult> FindOrderByTransactionCode([FromBody] PaymentDTO orderCode)
    {
        try
        {
            if (string.IsNullOrEmpty(orderCode.OrderCode))
            {
                return BadRequest("Mã giao dịch không được để trống.");
            }

            var order = await _paymentService.GetOrderByTransactionCode(orderCode.OrderCode);
            if (order == null)
            {
                return NotFound("Không tìm thấy đơn hàng với mã giao dịch này.");
            }

            return Ok(order);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Lỗi khi tìm kiếm đơn hàng", error = ex.Message });
        }
    }

}