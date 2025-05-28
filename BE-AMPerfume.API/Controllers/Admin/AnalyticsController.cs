using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/adm/analytics")]
public class AnalyticsController : Controller
{
    private readonly IAnalyticsService _analyticsService;
    public AnalyticsController(IAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }
    [HttpPost()]
    public async Task<IActionResult> GetAnalytics([FromBody] TimeDTO dto)
    {
        DateTime? start = dto.StartDate;
        DateTime? end = dto.EndDate;

        if (!start.HasValue && dto.Day.HasValue && dto.Month.HasValue && dto.Year.HasValue)
        {
            var date = new DateTime(dto.Year.Value, dto.Month.Value, dto.Day.Value);
            start = date;
            end = date;
        }
        if (start.HasValue && end.HasValue && start.Value.Date == end.Value.Date)
        {
            end = end.Value.Date.AddDays(1);
        }
        var analytics = await _analyticsService.GetDashboardAnalyticsAsync(start, end);
        return Ok(analytics);
    }



    [HttpGet]
    public async Task<IActionResult> GetAllTimeInfo()
    {
        var currentYear = DateTime.Now.Year;
        var start = new DateTime(currentYear, 1, 1);
        var end = new DateTime(currentYear, 12, 31, 23, 59, 59);

        var result = await _analyticsService.GetDashboardAnalyticsAsync(start, end);
        return Ok(result);
    }
    [HttpGet("chart")]
    public async Task<IActionResult> GetMonthlyRevenue([FromQuery] int? year)
    {
        var income = await _analyticsService.GetMonthlyRevenueAsync(year);
        var totalProduct = await _analyticsService.GetMonthlyProductAsync(year);
        var totalUser = await _analyticsService.GetMonthlyUserAsync(year);
        var totalProductByBrand = await _analyticsService.GetMonthlyBrandAsync(year);
        var result = new ChartDTO
        {
            Revenue = income,
            ProductSold = totalProduct,
            TotalUsers = totalUser,
            TotalProductByBrand = totalProductByBrand
        };
        return Ok(result);
    }
}