public class PaymentDisplayDTO
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string Method { get; set; }
    public decimal Amount { get; set; }
    public decimal ShippingFee { get; set; }
    public decimal TotalAmount => Amount + ShippingFee;
    public bool IsPaid { get; set; }
    public DateTime? PaidAt { get; set; }
    public string? TransactionCode { get; set; }
}
