using AutoMapper;
using ShoppingCart.Service.OrderApi.Models;
using ShoppingCart.Service.OrderApi.Models.Dto;


namespace ShoppingCart.Service.OrderApi
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMap()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<OrderHeaderDto, CartHeaderDto>()
                .ForMember(dest => dest.CartTotal, u => u.MapFrom(src => src.OrderTotal)).ReverseMap();
                config.CreateMap<CartDetailsDto, OrderDetailsDto>()
                .ForMember(dest=>dest.ProductName,u=>u.MapFrom(src=>src.Product.Name))
                .ForMember(dest => dest.Price, u => u.MapFrom(src => src.Product.Price));

                config.CreateMap<OrderHeaderModel, OrderHeaderDto>().ReverseMap();
                config.CreateMap<OrderDetailsDto, CartDetailsDto>().ReverseMap();
                config.CreateMap<OrderDetailsModel, OrderDetailsDto>().ReverseMap();

            });
            return mappingConfig;
        }
    }
}
