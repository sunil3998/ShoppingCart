using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Web.Models;
using ShoppingCart.Web.Service.IService;
using ShoppingCart.Web.Utility;

namespace ShoppingCart.Web.Service
{
    public class CartService : ICartService
    {
        private readonly IBaseService _baseService;

        public CartService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.POST,
                Data = cartDto,
                Url = StaticData.CartApiBase + "/api/CartApi/ApplyCoupon"
            }, withBearer: true);
        }

        public async Task<ResponseDto?> RemoveCouponAsync([FromBody] CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.POST,
                Data = cartDto,
                Url = StaticData.CartApiBase + "/api/CartApi/RemoveCoupon"
            }, withBearer: true);
        }

        public async Task<ResponseDto?> CartUpsertAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.POST,
                Data = cartDto,
                Url = StaticData.CartApiBase + "/api/CartApi/CartUpsert"
            }, withBearer: true);
        }

        public async Task<ResponseDto?> GetCartByUserIdAsync(string UserId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.GET,
                Url = StaticData.CartApiBase + "/api/CartApi/GetCartByUserId/" + UserId
            }, withBearer: true);
        }

        public async Task<ResponseDto?> RemoveCartAsync(int cartDetailsId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.POST,
                Data = cartDetailsId,
                Url = StaticData.CartApiBase + "/api/CartApi/RemoveCart"
            }, withBearer: true);
        }

        public async Task<ResponseDto?> RemoveCartWhenCheckout(int cartHeaderId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.POST,
                Data = cartHeaderId,
                Url = StaticData.CartApiBase + "/api/CartApi/RemoveCartWhenCheckout"
            }, withBearer: true);
        }
    }
}
