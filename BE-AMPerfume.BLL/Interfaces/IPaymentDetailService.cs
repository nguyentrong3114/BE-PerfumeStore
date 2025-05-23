using BE_AMPerfume.Core.DTOs;

namespace BE_AMPerfume.DAL.Interfaces;
public interface IPaymentDetailService
{
    // Add a new payment detail
    Task Add(PaymentDetailDTO paymentDetail);
    

    // Get a payment detail by its ID
    PaymentDetail GetById(int id);

    // Get all payment details
    IEnumerable<PaymentDetailDTO> GetAll();

    // Update an existing payment detail
    void Update(PaymentDetailDTO paymentDetail);

    // Delete a payment detail by its ID
    void Delete(int id);
}