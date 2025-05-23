namespace BE_AMPerfume.Core.DTOs
{
    public class PaymentDetailDTO
    {
        public int Id { get; set; }

        public int PaymentId { get; set; }

        public int ProductVariantId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice => Quantity * Price;
    }
}
