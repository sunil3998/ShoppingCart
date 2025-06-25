using AutoMapper;
using ShoppingCart.Service.CardsApi.Models;
using ShoppingCart.Service.CardsApi.Models.Dto;
using ShoppingCart.Service.CartApi.Models;
using ShoppingCart.Service.CartApi.Models.Dto;

namespace ShoppingCart.Service.CardsApi
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMap()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CartHeaderDto, CartHeaderModel>();
                config.CreateMap<CartHeaderModel, CartHeaderDto>();
                config.CreateMap<CartDetailsDto, CartDetailsModel>();
                config.CreateMap<CartDetailsModel, CartDetailsDto>();
            });
            return mappingConfig;
        }
    }
}
