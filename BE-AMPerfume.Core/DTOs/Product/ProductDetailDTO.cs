using BE_AMPerfume.Core.Models;

public class ProductDetailDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Category { get; set; } = null!;
    public int? Star { get; set; }
    // Brand
    public int BrandId { get; set; }
    public string BrandName { get; set; } = null!;

    // Notes
    public List<string> Notes { get; set; } = new();

    // Images
    public List<string> Images { get; set; } = new();

    // Variants
    public List<ProductVariantDTO> Variants { get; set; } = new();
}