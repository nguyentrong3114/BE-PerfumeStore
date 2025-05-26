public interface IAnalyticsRepository
{
    Task<decimal> TotalIncome(DateTime? start, DateTime? end);
    Task<decimal> TotalSales(DateTime? start, DateTime? end);
    Task<decimal> TotalOrder(DateTime? start, DateTime? end);
    Task<int> TotalUser(DateTime? start, DateTime? end);

}