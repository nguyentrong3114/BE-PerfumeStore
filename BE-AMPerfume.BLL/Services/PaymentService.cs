using BE_AMPerfume.BLL.Interfaces;
using BE_AMPerfume.Core.DTOs;
using BE_AMPerfume.Core.Models;
using BE_AMPerfume.DAL.Interfaces;
public class PaymentService : IPaymentService
{
    private readonly IUnitOfWork _unitOfWork;

    public PaymentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public Task ConfirmPaymentAsync(string transactionCode)
    {
        throw new NotImplementedException();
    }

    public async Task CreatePaymentAsync(int userId, PaymentDTO paymentDTO)
    {
        var cart = await _unitOfWork.CartRepository.GetCartByUserIdAsync(userId);
        int cartId = cart.Id;
        var shippingFee = paymentDTO.ShippingFee;
        var payment = new Payment
        {
            CartId = cartId,
            FullName = paymentDTO.FullName,
            Address = paymentDTO.Address ?? string.Empty,
            Email = paymentDTO.Email,
            Status = "Pending",
            Method = paymentDTO.Method ?? "COD",
            Amount = paymentDTO.Amount,
            TotalAmount = 0,
            ShippingFee = shippingFee,
            IsPaid = false,
            PaidAt = null,
            TransactionCode = null,
            CancelReason = "Chưa Có",
        };
        await _unitOfWork.PaymentRepository.AddAsync(payment);
        await _unitOfWork.SaveChangesAsync();
    }

    public Task DeleteAsync(int paymentId)
    {
        throw new NotImplementedException();
    }

    public Task<PaymentDTO?> GetByCartIdAsync(int cartId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsPaidAsync(int cartId)
    {
        throw new NotImplementedException();
    }
}