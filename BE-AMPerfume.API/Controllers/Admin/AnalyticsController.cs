using Microsoft.AspNetCore.Mvc;
[Route("api/adm/analytics")]
public class AnalyticsController : Controller
{
    private readonly IAnalyticsService _analyticsService;
    public AnalyticsController(IAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }
    [HttpPost()]
    public async Task<IActionResult> GetInfo([FromBody] TimeDTO time)
    {
        var result = await _analyticsService.GetDashboardAnalyticsAsync(
            time?.day, time?.month, time?.year
        );

        return Ok(result);
    }


    [HttpGet()]
    public async Task<IActionResult> GetAllTimeInfo()
    {
        var result = await _analyticsService.GetDashboardAnalyticsAsync(null, null, null);
        return Ok(result);
    }

}