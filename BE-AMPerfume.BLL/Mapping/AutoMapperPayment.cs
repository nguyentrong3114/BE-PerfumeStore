using AutoMapper;
using BE_AMPerfume.Core.DTOs;
using BE_AMPerfume.Core.Models;

public class AutoMapperPayment : Profile
{
    public AutoMapperPayment()
    {
        CreateMap<PaymentDTO, Payment>();

        CreateMap<Payment, PaymentDisplayDTO>()
        .ForMember(dest => dest.TotalAmount,
                    opt => opt.MapFrom(src => src.Amount + src.ShippingFee));
        CreateMap<PaymentDetail, PaymentDetailDTO>();
    }
    
}