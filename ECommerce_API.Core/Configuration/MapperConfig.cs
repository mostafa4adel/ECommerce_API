using AutoMapper;
using ECommerce_API.Data;
using ECommerce_API.Core.Models.Product;
using ECommerce_API.Core.Models.Category;
using System.Diagnostics.Metrics;



namespace ECommerce_API.Core.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Product, BaseProductDto>().ReverseMap();

            CreateMap<Category, BaseCategoryDto>().ReverseMap();
        }
    }
}
