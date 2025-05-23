using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BE_AMPerfume.Core.Models;

[Table("PaymentDetails")]
public class PaymentDetail
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int PaymentId { get; set; }

    [ForeignKey("PaymentId")]
    public virtual Payment Payment { get; set; }

    [Required]
    public int ProductVariantId { get; set; }

    [ForeignKey("ProductVariantId")]
    public virtual ProductVariant ProductVariant { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice => Quantity * Price;
}
