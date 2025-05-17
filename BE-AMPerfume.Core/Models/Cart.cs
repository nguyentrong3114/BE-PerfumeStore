using System.ComponentModel.DataAnnotations.Schema;
using BE_AMPerfume.Core.Models;

public class Cart
{
    public int Id { get; set; }
    [ForeignKey("UserId")]
    public int? UserId { get; set; } 

    public virtual User User { get; set; } = null!;
    public List<CartItems> CartItems { get; set; } = new List<CartItems>();
    public decimal TotalPrice => CalculateTotalPrice();

    private decimal CalculateTotalPrice()
    {
        decimal total = 0;
        foreach (var item in CartItems)
        {
            total += item.ProductVariant.Price * item.Quantity;
        }
        return total;
    }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}