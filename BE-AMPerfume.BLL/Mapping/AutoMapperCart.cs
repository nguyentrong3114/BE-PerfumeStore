using AutoMapper;

public class AutoMapperCart : Profile
{
    public AutoMapperCart()
    {
        CreateMap<Cart, CartDTO>()
            .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.CartItems.Sum(i => i.ProductVariant.Price * i.Quantity)));
    }
}
