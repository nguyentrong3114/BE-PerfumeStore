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

    public async Task<decimal> TotalIncome(int? day, int? month, int? year)
    {
        var query = from payment in _context.Payments
                    join detail in _context.PaymentDetails
                        on payment.Id equals detail.PaymentId
                    where payment.IsPaid
                    select new { payment.CreatedAt, detail.Price, detail.Quantity };

        if (year.HasValue)
            query = query.Where(x => x.CreatedAt.Year == year.Value);
        if (month.HasValue)
            query = query.Where(x => x.CreatedAt.Month == month.Value);
        if (day.HasValue)
            query = query.Where(x => x.CreatedAt.Day == day.Value);

        return await query.SumAsync(x => x.Price * x.Quantity);
    }

    public async Task<decimal> TotalOrder(int? day, int? month, int? year)
    {
        var query = _context.Payments.AsQueryable().Where(p => p.IsPaid);

        if (year.HasValue)
            query = query.Where(p => p.CreatedAt.Year == year.Value);
        if (month.HasValue)
            query = query.Where(p => p.CreatedAt.Month == month.Value);
        if (day.HasValue)
            query = query.Where(p => p.CreatedAt.Day == day.Value);

        return await query.CountAsync();
    }

    public async Task<decimal> TotalSales(int? day, int? month, int? year)
    {
        var query = from payment in _context.Payments
                    join detail in _context.PaymentDetails
                        on payment.Id equals detail.PaymentId
                    where payment.IsPaid
                    select new { payment.CreatedAt, detail.Quantity };

        if (year.HasValue)
            query = query.Where(x => x.CreatedAt.Year == year.Value);
        if (month.HasValue)
            query = query.Where(x => x.CreatedAt.Month == month.Value);
        if (day.HasValue)
            query = query.Where(x => x.CreatedAt.Day == day.Value);

        return await query.SumAsync(x => x.Quantity);
    }

    public async Task<int> TotalUser(int? day, int? month, int? year)
    {
        var query = _context.Users.AsQueryable().Where(u => u.IsVerify);

        if (year.HasValue)
            query = query.Where(u => u.CreatedAt.Year == year.Value);
        if (month.HasValue)
            query = query.Where(u => u.CreatedAt.Month == month.Value);
        if (day.HasValue)
            query = query.Where(u => u.CreatedAt.Day == day.Value);

        return await query.CountAsync();
    }
}
