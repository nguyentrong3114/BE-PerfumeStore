public class TimeDTO
{
    // Dành cho tìm kiếm theo ngày đơn
    public int? Day { get; set; }
    public int? Month { get; set; }
    public int? Year { get; set; }

    // Dành cho tìm kiếm theo khoảng ngày
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
