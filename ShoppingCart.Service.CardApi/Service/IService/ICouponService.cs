using ShoppingCart.Service.CartApi.Models.Dto;

namespace ShoppingCart.Service.CartApi.Service.IService
{
    public interface ICouponService
    {
       public Task<CouponsDto> GetCoupon(string couponCode);
    }
}
