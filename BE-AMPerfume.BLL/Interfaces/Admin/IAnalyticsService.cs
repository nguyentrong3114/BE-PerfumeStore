public interface IAnalyticsService
{
    // Tổng doanh thu trong ngày
    Task<decimal> GetTotalIncomeAsync(int day, int month, int year);

    // Tổng đơn hàng trong ngày
    Task<decimal> GetTotalOrdersAsync(int day, int month, int year);

    // Tổng người dùng đăng ký trong ngày
    Task<int> GetTotalUsersAsync(int day, int month, int year);

    // Tổng sản phẩm đã bán ra trong ngày
    Task<decimal> GetTotalProductsSoldAsync(int day, int month, int year);
    Task<AnalyticsDTO> GetDashboardAnalyticsAsync(int? day, int? month, int? year);
}
