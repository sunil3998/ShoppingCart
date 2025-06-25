using ShoppingCart.Web.Models;
using ShoppingCart.Web.Service.IService;
using ShoppingCart.Web.Utility;

namespace ShoppingCart.Web.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService)
        {
            _baseService=baseService;
        }
        public async Task<ResponseDto?> CreateCouponAsync(CouponsDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.POST,
                Data= couponDto,
                Url = StaticData.CouponApiBase + "/api/coupon/Create"
            },withBearer: true);
        }

        public async Task<ResponseDto?> DeleteCouponAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.DELETE,
                Url = StaticData.CouponApiBase + "/api/coupon/Delete?id=" + couponId
            }, withBearer: true);
        }

        public async Task<ResponseDto?> GetAllCouponAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.GET,
                Url = StaticData.CouponApiBase + "/api/coupon"
            }, withBearer: true);

        }

        public async Task<ResponseDto?> GetCouponByIdAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.GET,
                Url = StaticData.CouponApiBase + "/api/coupon/" + couponId
            }, withBearer: true);
        }

        public async Task<ResponseDto?> UpdatedCouponAsync(CouponsDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.PUT,
                Data = couponDto,
                Url = StaticData.CouponApiBase + "/api/coupon/Update"
            }, withBearer: true);
        }
    }
}
