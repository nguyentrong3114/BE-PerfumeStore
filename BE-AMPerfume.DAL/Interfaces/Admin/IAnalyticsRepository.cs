public interface IAnalyticsRepository
{
    Task<decimal> TotalSales(int? day, int? month, int? year);
    Task<decimal> TotalIncome(int? day, int? month, int? year);
    Task<decimal> TotalOrder(int? day, int? month, int? year);
    Task<int> TotalUser(int? day, int? month, int? year);  
}