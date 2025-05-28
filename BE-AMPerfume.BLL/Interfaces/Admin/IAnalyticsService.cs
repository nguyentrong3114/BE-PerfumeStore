public interface IAnalyticsService
{
    Task<decimal> GetTotalIncomeAsync(DateTime? start, DateTime? end);
    Task<decimal> GetTotalOrdersAsync(DateTime? start, DateTime? end);
    Task<int> GetTotalUsersAsync(DateTime? start, DateTime? end);
    Task<decimal> GetTotalProductsSoldAsync(DateTime? start, DateTime? end);

    Task<AnalyticsDTO> GetDashboardAnalyticsAsync(DateTime? start, DateTime? end);

    //Chart
    Task<List<MonthlyRevenueDTO>> GetMonthlyRevenueAsync(int? year = null);
    Task<List<MonthProductDTO>> GetMonthlyProductAsync(int? year = null);
    Task<List<MonthTotalUserDTO>> GetMonthlyUserAsync(int? year = null);
    Task<List<MonthBrandDTO>> GetMonthlyBrandAsync(int? year = null);
}
