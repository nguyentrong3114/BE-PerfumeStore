using BE_AMPerfume.Core.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? ImageUrl { get; set; }
    public int? ParentId { get; set; }
    public string Slug { get; set; } = null!;
    public Category? Parent { get; set; }
    public List<Product> Products { get; set; } = new();
    public List<Category> SubCategories { get; set; } = new List<Category>();
}