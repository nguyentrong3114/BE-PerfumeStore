
using BE_AMPerfume.DAL.Interfaces;

public class AnalyticsService : IAnalyticsService
{
    private readonly IUnitOfWork _unitOfWork;
    public AnalyticsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public Task<decimal> GetTotalIncomeAsync(int day, int month, int year)
    {
        var result = _unitOfWork.AnalyticsRepository.TotalIncome(day, month, year);
        return result;
    }

    public Task<decimal> GetTotalOrdersAsync(int day, int month, int year)
    {
        var result = _unitOfWork.AnalyticsRepository.TotalOrder(day, month, year);
        return result;
    }

    public Task<decimal> GetTotalProductsSoldAsync(int day, int month, int year)
    {
        var result = _unitOfWork.AnalyticsRepository.TotalSales(day, month, year);
        return result;
    }

    public Task<int> GetTotalUsersAsync(int day, int month, int year)
    {
        var result = _unitOfWork.AnalyticsRepository.TotalUser(day, month, year);
        return result;
    }
    public async Task<AnalyticsDTO> GetDashboardAnalyticsAsync(int? day, int? month, int? year)
    {
        var totalIncome = await _unitOfWork.AnalyticsRepository.TotalIncome(day, month, year);
        var totalOrders = await _unitOfWork.AnalyticsRepository.TotalOrder(day, month, year);
        var totalSales = await _unitOfWork.AnalyticsRepository.TotalSales(day, month, year);
        var totalUsers = await _unitOfWork.AnalyticsRepository.TotalUser(day, month, year);

        return new AnalyticsDTO
        {
            TotalIncome = totalIncome,
            TotalOrder = totalOrders,
            TotalSales = totalSales,
            TotalUser = totalUsers
        };
    }

}
