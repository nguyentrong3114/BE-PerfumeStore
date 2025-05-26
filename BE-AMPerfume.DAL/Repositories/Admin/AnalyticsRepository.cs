using BE_AMPerfume.Core.Models;
using BE_AMPerfume.DAL.Data;
using Microsoft.EntityFrameworkCore;

public class AnalyticsRepository : IAnalyticsRepository
{
    private readonly AMPerfumeDbContext _context;

    public AnalyticsRepository(AMPerfumeDbContext context)
    {
        _context = context;
    }

    public async Task<decimal> TotalIncome(DateTime? start, DateTime? end)
    {
        var query = from payment in _context.Payments
                    join detail in _context.PaymentDetails
                        on payment.Id equals detail.PaymentId
                    where payment.IsPaid
                    select new { payment.CreatedAt, detail.Price, detail.Quantity };

        if (start.HasValue)
            query = query.Where(x => x.CreatedAt >= start.Value);
        if (end.HasValue)
            query = query.Where(x => x.CreatedAt <= end.Value);

        return await query.SumAsync(x => x.Price * x.Quantity);
    }


    public async Task<decimal> TotalOrder(DateTime? start, DateTime? end)
    {
        var query = _context.Payments.Where(p => p.IsPaid);

        if (start.HasValue)
            query = query.Where(p => p.CreatedAt >= start.Value);
        if (end.HasValue)
            query = query.Where(p => p.CreatedAt <= end.Value);

        return await query.CountAsync();
    }


    public async Task<decimal> TotalSales(DateTime? start, DateTime? end)
    {
        var query = from payment in _context.Payments
                    join detail in _context.PaymentDetails
                        on payment.Id equals detail.PaymentId
                    where payment.IsPaid
                    select new { payment.CreatedAt, detail.Quantity };

        if (start.HasValue)
            query = query.Where(x => x.CreatedAt >= start.Value);
        if (end.HasValue)
            query = query.Where(x => x.CreatedAt <= end.Value);

        return await query.SumAsync(x => x.Quantity);
    }


    public async Task<int> TotalUser(DateTime? start, DateTime? end)
    {
        var query = _context.Users.Where(u => u.IsVerify);

        if (start.HasValue)
            query = query.Where(u => u.CreatedAt >= start.Value);
        if (end.HasValue)
            query = query.Where(u => u.CreatedAt <= end.Value);

        return await query.CountAsync();
    }

}
