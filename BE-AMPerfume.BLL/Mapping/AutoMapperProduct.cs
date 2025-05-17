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
                opt.MapFrom((src, dest) =>
                    src.ProductImages.FirstOrDefault(img => img.IsThumbnail) != null
                        ? src.ProductImages.FirstOrDefault(img => img.IsThumbnail)!.ImageUrl
                        : null
                ))
            .ForMember(dest => dest.CategoryName, opt =>
                opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.BrandName, opt =>
                opt.MapFrom(src => src.Brand.Name));


        // Mapping cho chi tiết
        CreateMap<Product, ProductDetailDTO>()
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src =>
                src.ProductImages
                    .Where(img => !string.IsNullOrEmpty(img.ImageUrl))
                    .Select(img => img.ImageUrl)
                    .ToList()
            ))
            .ForMember(dest => dest.Variants, opt => opt.MapFrom(src => src.Variants));


        // Mapping cho variant
        CreateMap<ProductVariant, ProductVariantDTO>();
    }
}
