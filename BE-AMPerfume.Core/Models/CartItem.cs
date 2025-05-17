using System.ComponentModel.DataAnnotations.Schema;
using BE_AMPerfume.Core.Models;

public class CartItems
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public int Quantity { get; set; }

    // Navigation properties
    public virtual Cart Cart { get; set; } = null!;
    public int? ProductVariantId { get; set; }
    public virtual ProductVariant ProductVariant { get; set; } = null!;
    [NotMapped]
    public decimal TotalPrice => ProductVariant.Price * Quantity;
}