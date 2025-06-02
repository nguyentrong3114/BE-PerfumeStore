using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BE_AMPerfume.Core.Models;

[Table("Payments")]
public class Payment
{
    [Key]
    public int Id { get; set; }
    public string? OrderCode { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public int? CartId { get; set; }

    [ForeignKey("CartId")]
    public virtual Cart Cart { get; set; }

    [Required]
    [StringLength(50)]
    public string Method { get; set; } = "COD";

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }
    public decimal? TotalAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal ShippingFee { get; set; } = 0;

    public bool IsPaid { get; set; } = false;

    public DateTime? PaidAt { get; set; }

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "Pending";

    public string? CancelReason { get; set; }
    public virtual ICollection<PaymentDetail> Items { get; set; } = new List<PaymentDetail>();

}
