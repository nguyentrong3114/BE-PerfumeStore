public interface IAnalyticsService
{
    Task<decimal> GetTotalIncomeAsync(DateTime? start, DateTime? end);
    Task<decimal> GetTotalOrdersAsync(DateTime? start, DateTime? end);
    Task<int> GetTotalUsersAsync(DateTime? start, DateTime? end);
    Task<decimal> GetTotalProductsSoldAsync(DateTime? start, DateTime? end);

    Task<AnalyticsDTO> GetDashboardAnalyticsAsync(DateTime? start, DateTime? end);
}
