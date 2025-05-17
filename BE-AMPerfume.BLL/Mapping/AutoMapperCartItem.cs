using AutoMapper;

public class AutoMapperCartItem : Profile
{
    public AutoMapperCartItem()
    {
        CreateMap<CartItems, CartItemDTO>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductVariant.Product.Name))
            .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.ProductVariant.Size))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ProductVariant.Product.ProductImages.FirstOrDefault().ImageUrl ?? ""))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ProductVariant.Price));
    }
}