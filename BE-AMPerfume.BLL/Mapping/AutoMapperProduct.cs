using AutoMapper;
using BE_AMPerfume.Core.Models;
using BE_AMPerfume.Core.DTOs;
using System.Linq;

public class AutoMapperProduct : Profile
{
    public AutoMapperProduct()
    {
        // Mapping cho danh sách
        CreateMap<Product, ProductDTO>()
            .ForMember(dest => dest.ImageUrl, opt =>
                opt.MapFrom(src => src.ProductImage != null ? src.ProductImage.ThumbnailUrl : null))
            .ForMember(dest => dest.CategoryName, opt =>
                opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.BrandName, opt =>
                opt.MapFrom(src => src.Brand.Name));

        // Mapping cho chi tiết
        CreateMap<Product, ProductDetailDTO>()
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => new List<string>
            {
                src.ProductImage.ImageUrl,
                src.ProductImage.ImageUrl2,
                src.ProductImage.ImageUrl3,
                src.ProductImage.ImageUrl4,
                src.ProductImage.ImageUrl5
            }.Where(url => !string.IsNullOrEmpty(url)).ToList()))
            .ForMember(dest => dest.Variants, opt => opt.MapFrom(src => src.Variants));

        // Mapping cho variant
        CreateMap<ProductVariant, ProductVariantDTO>();
    }
}
