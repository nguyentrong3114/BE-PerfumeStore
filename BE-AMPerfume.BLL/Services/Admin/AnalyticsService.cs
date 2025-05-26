
using BE_AMPerfume.DAL.Interfaces;

public class AnalyticsService : IAnalyticsService
{
    private readonly IUnitOfWork _unitOfWork;
    public AnalyticsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<decimal> GetTotalIncomeAsync(DateTime? start, DateTime? end)
    {
        var result = await _unitOfWork.AnalyticsRepository.TotalIncome(start, end);
        return result;
    }

    public async Task<decimal> GetTotalOrdersAsync(DateTime? start, DateTime? end)
    {
        return await _unitOfWork.AnalyticsRepository.TotalOrder(start, end);
    }

    public async Task<int> GetTotalUsersAsync(DateTime? start, DateTime? end)
    {
        return await _unitOfWork.AnalyticsRepository.TotalUser(start, end);
    }

    public async Task<decimal> GetTotalProductsSoldAsync(DateTime? start, DateTime? end)
    {
        return await _unitOfWork.AnalyticsRepository.TotalSales(start, end);
    }
    public async Task<AnalyticsDTO> GetDashboardAnalyticsAsync(DateTime? start, DateTime? end)
    {
        var income = await GetTotalIncomeAsync(start, end);
        var orders = await GetTotalOrdersAsync(start, end);
        var users = await GetTotalUsersAsync(start, end);
        var productsSold = await GetTotalProductsSoldAsync(start, end);

        return new AnalyticsDTO
        {
            TotalIncome = income,
            TotalOrders = orders,
            TotalUsers = users,
            TotalProductsSold = productsSold
        };
    }

}
