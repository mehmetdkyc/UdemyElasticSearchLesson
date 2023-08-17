using AutoMapper;
using Elasticsearch.API.Dtos;
using Elasticsearch.API.Models;


namespace Elasticsearch.API.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Product, ProductCreateDto>().
            ForCtorParam("Name", opt => opt.MapFrom(src => src.Name)).
                ForCtorParam("Price", opt => opt.MapFrom(src => src.Price)).
                ForCtorParam("Stock", opt => opt.MapFrom(src => src.Stock)).
                ForCtorParam("Feature", opt => opt.MapFrom(src => src.Feature)).
                ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            //CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<ProductFeature, ProductFeatureDto>().ForCtorParam("Height", opt => opt.MapFrom(src => src.Height)).
                ForCtorParam("Width", opt => opt.MapFrom(src => src.Width)).
                ForCtorParam("Color", opt => opt.MapFrom(src => src.Color)).
                ReverseMap();

            CreateMap<ECommerceDto, ECommerce>().ReverseMap();
            CreateMap<ProductECommerceDto, ProductECommerce>().ReverseMap();
        }
    }
}
