using Newtonsoft.Json;
using ShoppingCart.Service.OrdersApi.Models.Dto;
using ShoppingCart.Service.OrdersApi.Service.IService;

namespace ShoppingCart.Service.OrdersApi.Service
{
    public class CartService : ICartService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string cartUrl;

        public CartService(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
            cartUrl = "api/CartApi";
        }

        public async Task<ResponseDto> RemoveCartWhenCheckout(int cartHeaderId)
        {
            var client = _httpClientFactory.CreateClient("Cart");
            var response = await client.GetAsync($"{cartUrl}/RemoveCartWhenCheckout/{cartHeaderId}");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = response.Content.ReadAsStringAsync().Result;
                return await Task.FromResult(JsonConvert.DeserializeObject<ResponseDto>(apiResponse));
            }
            return new ResponseDto
            {
                IsSuccess = false,
                Message = "Error while removing cart",
                Result = new List<string> { "Internal Server Error" }
            };
        }
    }
}