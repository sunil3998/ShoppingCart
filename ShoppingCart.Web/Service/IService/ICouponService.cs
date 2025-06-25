using ShoppingCart.Web.Models;

namespace ShoppingCart.Web.Service.IService
{
    public interface ICouponService
    {
       Task<ResponseDto?> GetCouponByIdAsync(int couponId);
        Task<ResponseDto?> GetAllCouponAsync();
        Task<ResponseDto?> CreateCouponAsync(CouponsDto couponDto);
        Task<ResponseDto?> UpdatedCouponAsync(CouponsDto couponDto);
        Task<ResponseDto?> DeleteCouponAsync(int couponId);

    }
}
