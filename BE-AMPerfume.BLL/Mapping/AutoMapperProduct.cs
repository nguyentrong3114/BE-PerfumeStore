using AutoMapper;
using BE_AMPerfume.Core.Models;
using BE_AMPerfume.Core.DTOs;

public class AutoMapperProduct : Profile
{
    public AutoMapperProduct()
    {
        CreateMap<Product, ProductDTO>()
            .ForMember(dest => dest.ImageUrl, opt =>
                opt.MapFrom(src => src.ProductImage != null ? src.ProductImage.ThumbnailUrl : null))
            .ForMember(dest => dest.CategoryName, opt =>
                opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.BrandName, opt =>
                opt.MapFrom(src => src.Brand.Name));
    }
}
