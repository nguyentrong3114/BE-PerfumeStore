namespace BE_AMPerfume.Core.Models;

public class ProductImage
{
    public int Id { get; set; }

    public string? ThumbnailUrl { get; set; }
    public string ImageUrl { get; set; } = null!;
    public string? ImageUrl2 { get; set; }
    public string? ImageUrl3 { get; set; }
    public string? ImageUrl4 { get; set; }
    public string? ImageUrl5 { get; set; }
    public int? ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
