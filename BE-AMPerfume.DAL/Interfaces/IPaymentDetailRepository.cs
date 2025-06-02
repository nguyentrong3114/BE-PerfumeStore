
using BE_AMPerfume.Core.DTOs;
using BE_AMPerfume.Core.Models;

public interface IPaymentDetailRepository
{
    // Add a new payment detail
    Task Add(PaymentDetail paymentDetail);

    // Get a payment detail by its ID
    PaymentDetail GetById(int id);
    // Get all payment details
    IEnumerable<PaymentDetail> GetAll();

    // Update an existing payment detail
    void Update(PaymentDetail paymentDetail);

    // Delete a payment detail by its ID
    void Delete(int id);


}