using System.ComponentModel.DataAnnotations.Schema;

namespace BE_AMPerfume.Core.Models;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public string? Scent { get; set; }
    public int? star{ get; set; } = 5;
    public string? Description { get; set; }
    public string Gender { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public int BrandId { get; set; }
    public Brand Brand { get; set; } = null!;
    public Note Notes { get; set; } = null!;
    public int? ProductImageId { get; set; }
    public ProductImage? ProductImage { get; set; } = null!;
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    // Nhiều ảnh cho mỗi sản phẩm
    public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
    public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
    

}
