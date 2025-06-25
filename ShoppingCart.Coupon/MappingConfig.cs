using AutoMapper;
using ShoppingCart.Coupon.Models;
using ShoppingCart.Coupon.Models.Dto;

namespace ShoppingCart.Coupon
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMap()
        {
            var mappingConfig=new MapperConfiguration(config=>
            {
                config.CreateMap<CouponsDto,CouponModel>();
                config.CreateMap<CouponModel,CouponsDto>();
            });
            return mappingConfig;
        }
    }
}
