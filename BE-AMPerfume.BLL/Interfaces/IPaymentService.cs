using System.Threading.Tasks;
using BE_AMPerfume.Core.DTOs;

namespace BE_AMPerfume.BLL.Interfaces
{
    public interface IPaymentService
    {
        /// <summary>
        /// Lấy thông tin thanh toán của 1 đơn hàng
        /// </summary>
        Task<PaymentDTO?> GetByCartIdAsync(int cartId);

        /// <summary>
        /// Tạo bản ghi thanh toán cho đơn hàng (COD, MoMo, VNPAY...)
        /// </summary>
        Task<int> CreatePaymentWithDetailsAsync(int userId, PaymentDTO paymentDTO, List<PaymentDetailDTO> paymentDetails);
        /// <summary>
        /// Cập nhật trạng thái đã thanh toán cho đơn hàng
        /// </summary>
        Task ConfirmPaymentAsync(string transactionCode);

        /// <summary>
        /// Kiểm tra đã thanh toán chưa (dành cho frontend)
        /// </summary>
        Task<bool> IsPaidAsync(int cartId);

        /// <summary>
        /// Xoá payment (nếu đơn bị huỷ và rollback)
        /// </summary>
        Task DeleteAsync(int paymentId);
    }
}
