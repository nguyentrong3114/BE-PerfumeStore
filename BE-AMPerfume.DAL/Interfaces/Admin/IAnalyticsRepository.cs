public interface IAnalyticsRepository
{
    // Tổng theo thời gian
    Task<decimal> TotalIncome(DateTime? start, DateTime? end);
    Task<decimal> TotalSales(DateTime? start, DateTime? end);
    Task<decimal> TotalOrder(DateTime? start, DateTime? end);
    Task<int> TotalUser(DateTime? start, DateTime? end);

    // Theo từng tháng trong năm
    Task<List<(int Month, decimal Revenue)>> GetMonthlyRevenueRawAsync(int year);
    Task<List<(int Month, decimal TotalProduct)>> GetMonthlySalesAsync(int year);
    Task<List<(int Month, decimal TotalUser)>> GetMonthlyUsersAsync(int year);
    Task<List<(decimal Product,string BrandName)>> GetBrandDistributionAsync(int year);
}
