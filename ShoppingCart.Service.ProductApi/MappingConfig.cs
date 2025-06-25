using AutoMapper;
using ShoppingCart.Service.ProductApi.Models;
using ShoppingCart.Service.ProductApi.Models.Dto;

namespace ShoppingCart.Service.ProductApi
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMap()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductsDto, ProductModel>();
                config.CreateMap<ProductModel, ProductsDto>();
            });
            return mappingConfig;
        }
    }
}
