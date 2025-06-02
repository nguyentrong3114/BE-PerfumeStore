public class CategoryAdminDTO
{
    public int Id { get; set; }
    public bool IsActive { get; set; } = true;
    public string Name { get; set; } = null!;
    public string slug { get; set; } = null!;
    public int ProductCount { get; set; }
}