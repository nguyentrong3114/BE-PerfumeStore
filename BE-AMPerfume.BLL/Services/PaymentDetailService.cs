
using BE_AMPerfume.Core.DTOs;
using BE_AMPerfume.Core.Models;
using BE_AMPerfume.DAL.Interfaces;

public class PaymentDetailService : IPaymentDetailService
{
    private readonly IUnitOfWork _unitOfWork;

    public PaymentDetailService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task Add(PaymentDetailDTO paymentDetail)
    {
        throw new NotImplementedException();
    }



    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<PaymentDetailDTO> GetAll()
    {
        throw new NotImplementedException();
    }

    public PaymentDetail GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(PaymentDetailDTO paymentDetail)
    {
        throw new NotImplementedException();
    }
}