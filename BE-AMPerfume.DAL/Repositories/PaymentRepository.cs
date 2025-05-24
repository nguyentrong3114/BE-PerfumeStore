using BE_AMPerfume.Core.Models;
using BE_AMPerfume.DAL.Data;
using BE_AMPerfume.DAL.Interfaces;

public class PaymentRepository : IPaymentRepository
{
    private readonly AMPerfumeDbContext _context;
    public PaymentRepository(AMPerfumeDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }


    public Task<Payment?> GetByCartIdAsync(int cartId)
    {
        throw new NotImplementedException();
    }

    public Task<Payment?> GetByTransactionCodeAsync(string transactionCode)
    {
        throw new NotImplementedException();
    }

    public Task<Payment?> ShowTransaction(int cartId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Payment payment)
    {
        throw new NotImplementedException();
    }

    //Admin
    public Task<IEnumerable<Payment>> GetAllPaymentAdmin()
    {
        throw new NotImplementedException();
    }

}