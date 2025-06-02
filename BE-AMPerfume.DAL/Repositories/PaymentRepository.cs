using BE_AMPerfume.Core.Models;
using BE_AMPerfume.DAL.Data;
using BE_AMPerfume.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

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

    public async Task DeleteAsync(int id)
    {
        await _context.Payments
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();
    }


    public Task<Payment?> GetByCartIdAsync(int cartId)
    {
        return _context.Payments
            .Where(p => p.CartId == cartId)
            .FirstOrDefaultAsync();
    }

    public Task<Payment?> GetByTransactionCodeAsync(string orderCode)
    {
        return _context.Payments
            .Where(p => p.OrderCode == orderCode)
            .FirstOrDefaultAsync();
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
    public async Task<IEnumerable<Payment>> GetAllPaymentAdmin()
    {
        var orders = await _context.Payments
        .ToListAsync();
        if (orders == null)
            throw new InvalidOperationException($"Error or no products");

        return orders;
    }

    public Task<Payment?> GetOrdersByUserIdAsync(int userId)
    {
        return _context.Payments
            .Include(p => p.Cart)
            .Where(p => p.Cart.UserId == userId)
            .FirstOrDefaultAsync();
    }
}