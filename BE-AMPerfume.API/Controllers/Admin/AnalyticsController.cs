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
        DateTime? start = null;
        DateTime? end = null;

        if (dto.StartDate.HasValue && dto.EndDate.HasValue)
        {
            start = dto.StartDate;
            end = dto.EndDate;
        }
        else if (dto.Day.HasValue && dto.Month.HasValue && dto.Year.HasValue)
        {
            var date = new DateTime(dto.Year.Value, dto.Month.Value, dto.Day.Value);
            start = date;
            end = date;
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
}