using Newtonsoft.Json;
using ShoppingCart.Service.CardsApi.Models.Dto;
using ShoppingCart.Service.CartApi.Models.Dto;
using ShoppingCart.Service.CartApi.Service.IService;

namespace ShoppingCart.Service.CartApi.Service
{
    public class CouponService : ICouponService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string couponUrl;
        public CouponService(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
            this.couponUrl = "api/coupon/GetByCouponCode?couponCode=";
        }
        public async Task<CouponsDto> GetCoupon(string couponCode)
        {
            var client = _httpClientFactory.CreateClient("Coupon");
            var response = await client.GetAsync($"{couponUrl}{couponCode}");
            var apiResponse = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiResponse);
            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<CouponsDto>(Convert.ToString(resp.Result));
            }
            return new CouponsDto();
        }
    }
}
    
