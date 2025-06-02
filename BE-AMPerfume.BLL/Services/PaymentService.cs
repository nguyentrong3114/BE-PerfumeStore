using AutoMapper;
using BE_AMPerfume.BLL.Interfaces;
using BE_AMPerfume.Core.DTOs;
using BE_AMPerfume.Core.Models;
using BE_AMPerfume.DAL.Interfaces;
public class PaymentService : IPaymentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PaymentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public Task ConfirmPaymentAsync(string transactionCode)
    {
        throw new NotImplementedException();
    }
    public async Task<int> CreatePaymentWithDetailsAsync(int userId, PaymentDTO paymentDTO, List<PaymentDetailDTO> paymentDetails)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            var cart = await _unitOfWork.CartRepository.GetCartByUserIdAsync(userId);
            int cartId = cart != null ? cart.Id : 0;

            var shippingFee = paymentDTO.ShippingFee;

            var payment = new Payment
            {
                OrderCode = $"ORD{DateTime.UtcNow:yyyyMMddHHmmss}-{Random.Shared.Next(1000, 9999)}",
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
                CancelReason = "Chưa Có",
            };

            await _unitOfWork.PaymentRepository.AddAsync(payment);
            await _unitOfWork.SaveChangesAsync();

            foreach (var detail in paymentDetails)
            {
                var entity = new PaymentDetail
                {
                    PaymentId = payment.Id,
                    ProductVariantId = detail.ProductVariantId,
                    Quantity = detail.Quantity,
                    Price = detail.Price,
                };

                await _unitOfWork.PaymentDetailRepostitory.Add(entity);
            }

            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();

            return payment.Id;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<int> CreatePaymenWithDetailsByUnknowAsync(PaymentDTO paymentDTO, List<PaymentDetailDTO> paymentDetails)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            var shippingFee = paymentDTO.ShippingFee;

            var payment = new Payment
            {
                OrderCode = $"ORD{DateTime.UtcNow:yyyyMMddHHmmss}-{Random.Shared.Next(1000, 9999)}",
                CartId = null, // Không có cartId vì là khách lạ
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
                CancelReason = "Chưa Có",
            };

            await _unitOfWork.PaymentRepository.AddAsync(payment);
            await _unitOfWork.SaveChangesAsync();

            foreach (var detail in paymentDetails)
            {
                var entity = new PaymentDetail
                {
                    PaymentId = payment.Id,
                    ProductVariantId = detail.ProductVariantId,
                    Quantity = detail.Quantity,
                    Price = detail.Price,
                };

                await _unitOfWork.PaymentDetailRepostitory.Add(entity);
            }

            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();

            return payment.Id;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public Task DeleteAsync(int paymentId)
    {
        throw new NotImplementedException();
    }

    public async Task<PagedResult<PaymentDisplayDTO>> GetAllOrderAdminAsync(int page, int size)
    {
        var orders = await _unitOfWork.PaymentRepository.GetAllPaymentAdmin();
        var dtos = _mapper.Map<List<PaymentDisplayDTO>>(orders);

        return new PagedResult<PaymentDisplayDTO>
        {
            Items = dtos,
            PageNumber = page,
            PageSize = size,
            TotalItems = orders.Count()
        };

    }

    public async Task<List<PaymentDisplayDTO>> GetAllPaymentAByUserIdAsync(int userId)
    {
        var payments = await _unitOfWork.PaymentRepository.GetOrdersByUserIdAsync(userId);
        if (payments == null)
            return new List<PaymentDisplayDTO>();

        return new List<PaymentDisplayDTO>
        {
            _mapper.Map<PaymentDisplayDTO>(payments)
        };
    }

    public Task<PaymentDTO?> GetByCartIdAsync(int cartId)
    {
        throw new NotImplementedException();
    }

    public async Task<PaymentDisplayDTO?> GetOrderByTransactionCode(string orderCode)
    {
        var payments = await _unitOfWork.PaymentRepository.GetByTransactionCodeAsync(orderCode);
        if (payments == null)
            return new PaymentDisplayDTO();

        return _mapper.Map<PaymentDisplayDTO>(payments);
    }


    public Task<bool> IsPaidAsync(int cartId)
    {
        throw new NotImplementedException();
    }
}