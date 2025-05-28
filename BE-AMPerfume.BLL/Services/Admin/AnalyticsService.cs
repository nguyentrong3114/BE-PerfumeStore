
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

    public async Task<List<MonthlyRevenueDTO>> GetMonthlyRevenueAsync(int? year = null)
    {
        int selectedYear = year ?? DateTime.Now.Year;
        var raw = await _unitOfWork.AnalyticsRepository.GetMonthlyRevenueRawAsync(selectedYear);

        var full = Enumerable.Range(1, 12)
            .Select(month =>
            {
                var match = raw.FirstOrDefault(x => x.Month == month);
                return new MonthlyRevenueDTO
                {
                    Month = month,
                    Revenue = match.Revenue
                };
            }).ToList();

        return full;
    }

    public async Task<List<MonthProductDTO>> GetMonthlyProductAsync(int? year = null)
    {
        int selectedYear = year ?? DateTime.Now.Year;
        var raw = await _unitOfWork.AnalyticsRepository.GetMonthlySalesAsync(selectedYear);

        var result = Enumerable.Range(1, 12)
            .Select(month =>
            {
                var item = raw.FirstOrDefault(x => x.Month == month);
                return new MonthProductDTO
                {
                    Month = month,
                    ProductCount = (int)item.TotalProduct
                };
            }).ToList();

        return result;
    }

    public Task<List<MonthTotalUserDTO>> GetMonthlyUserAsync(int? year = null)
    {
        int selectedYear = year ?? DateTime.Now.Year;
        return _unitOfWork.AnalyticsRepository.GetMonthlyUsersAsync(selectedYear)
            .ContinueWith(task =>
            {
                var raw = task.Result;
                return Enumerable.Range(1, 12)
                    .Select(month =>
                    {
                        var item = raw.FirstOrDefault(x => x.Month == month);
                        return new MonthTotalUserDTO
                        {
                            Month = month,
                            TotalUsers = (int)item.TotalUser
                        };
                    }).ToList();
            });
    }

    public Task<List<MonthBrandDTO>> GetMonthlyBrandAsync(int? year = null)
    {
        int selectedYear = year ?? DateTime.Now.Year;
        return _unitOfWork.AnalyticsRepository.GetBrandDistributionAsync(selectedYear)
            .ContinueWith(task =>
            {
                var raw = task.Result;
                return raw.Select(x => new MonthBrandDTO
                {
                    BrandName = x.BrandName,
                    ProductCount = (int)x.Product
                }).ToList();
            });
    }
}
