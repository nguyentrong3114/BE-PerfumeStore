using System.ComponentModel.DataAnnotations.Schema;

namespace BE_AMPerfume.Core.Models;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public string? Scent { get; set; }
    public int? star { get; set; } = 5;
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public float MinPrice { get; set; }
    public float MaxPrice { get; set; }
    public int BrandId { get; set; }
    public Brand Brand { get; set; } = null!;
    public Note Notes { get; set; } = null!;
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
    public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
}
