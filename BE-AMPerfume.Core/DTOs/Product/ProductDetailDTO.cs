using BE_AMPerfume.Core.Models;

public class ProductDetailDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string ProductImage { get; set; }
    public decimal Price { get; set; }
    public int BrandId { get; set; }
    public string BrandName { get; set; }
    public Note notes { get; set; }
}