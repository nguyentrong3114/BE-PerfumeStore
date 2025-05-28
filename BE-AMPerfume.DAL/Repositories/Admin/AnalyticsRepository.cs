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

    public async Task<List<(decimal Product, string BrandName)>> GetBrandDistributionAsync(int year)
    {
        var result = await _context.PaymentDetails
            .Where(pd => pd.Payment.IsPaid && pd.Payment.CreatedAt.Year == year)
            .GroupBy(pd => new
            {
                BrandName = pd.ProductVariant.Product.Brand.Name
            })
            .Select(g => new
            {
                g.Key.BrandName,
                Product = g.Sum(x => x.Quantity)
            })
            .OrderByDescending(x => x.Product)
            .Take(5)
            .ToListAsync();

        return result
            .Select(x => ((decimal)x.Product, x.BrandName))
            .ToList();
    }

    public async Task<List<(int Month, decimal Revenue)>> GetMonthlyRevenueRawAsync(int year)
    {
        return await _context.Payments
            .Where(p => p.IsPaid && p.CreatedAt.Year == year)
            .GroupBy(p => p.CreatedAt.Month)
            .Select(g => new ValueTuple<int, decimal>(
                g.Key,
                g.Sum(x => x.TotalAmount) ?? 0m
            ))
            .ToListAsync();
    }

    public Task<List<(int Month, decimal TotalProduct)>> GetMonthlySalesAsync(int year)
    {
        return _context.Payments
            .Where(p => p.IsPaid && p.CreatedAt.Year == year)
            .Select(p => new
            {
                p.CreatedAt.Month,
                Quantity = p.Items.Sum(i => i.Quantity)
            })
            .GroupBy(x => x.Month)
            .Select(g => new ValueTuple<int, decimal>(
                g.Key,
                g.Sum(x => (decimal)x.Quantity)
            ))
            .ToListAsync();
    }


    public Task<List<(int Month, decimal TotalUser)>> GetMonthlyUsersAsync(int year)
    {
        return _context.Users
            .Where(u => u.IsVerify && u.CreatedAt.Year == year)
            .Select(u => new
            {
                u.CreatedAt.Month,
                TotalUser = 1m
            })
            .GroupBy(x => x.Month)
            .Select(g => new ValueTuple<int, decimal>(
                g.Key,
                g.Sum(x => x.TotalUser)
            ))
            .ToListAsync();
    }

    public async Task<decimal> TotalIncome(DateTime? start, DateTime? end)
    {
        var query = _context.Payments.Where(p => p.IsPaid);

        if (start.HasValue)
            query = query.Where(p => p.CreatedAt >= start.Value);
        if (end.HasValue)
            query = query.Where(p => p.CreatedAt < end.Value);

        return (decimal)await query.SumAsync(p => p.TotalAmount);
    }

    public async Task<decimal> TotalOrder(DateTime? start, DateTime? end)
    {
        var query = _context.Payments.Where(p => p.IsPaid);

        if (start.HasValue)
            query = query.Where(p => p.CreatedAt >= start.Value);
        if (end.HasValue)
            query = query.Where(p => p.CreatedAt < end.Value);

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
            query = query.Where(x => x.CreatedAt < end.Value);

        return await query.SumAsync(x => x.Quantity);
    }


    public async Task<int> TotalUser(DateTime? start, DateTime? end)
    {
        var query = _context.Users.Where(u => u.IsVerify);

        if (start.HasValue)
            query = query.Where(u => u.CreatedAt >= start.Value);
        if (end.HasValue)
            query = query.Where(u => u.CreatedAt < end.Value);

        return await query.CountAsync();
    }

}
