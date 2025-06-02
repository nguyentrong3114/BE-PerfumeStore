public class CategoryAdminDisplayDTO
{
    public int TotalCategories { get; set; }
    public int TotalActiveCategories { get; set; }
    public int TotalInactiveCategories { get; set; }
    public int TotalProducts { get; set; }
    public List<CategoryAdminDTO> Categories { get; set; } = new List<CategoryAdminDTO>();
}