using BE_AMPerfume.Core.Models;

public class ProductImage
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = null!;
    public bool IsThumbnail { get; set; } = false; 

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
