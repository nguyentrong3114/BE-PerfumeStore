using BE_AMPerfume.Core.DTOs;

public class CreatePaymentRequestDTO
{
    public PaymentDTO Payment { get; set; }
    public List<PaymentDetailDTO> Items { get; set; }
}
