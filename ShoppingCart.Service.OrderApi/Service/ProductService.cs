using Newtonsoft.Json;
using ShoppingCart.Service.OrderApi.Models.Dto;
using ShoppingCart.Service.OrderApi.Service.IService;

namespace ShoppingCart.Service.OrderApi.Service
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string productUrl;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            productUrl = "api/products";
        }

        public async Task<IEnumerable<ProductsDto>> GetProducts()
        {
            var client = _httpClientFactory.CreateClient("Product");
            var response = await client.GetAsync(productUrl);
            var apiResponse = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiResponse);
            if (resp.IsSuccess)
            {
               return JsonConvert.DeserializeObject<List<ProductsDto>>(Convert.ToString(resp.Result));
            }
            return new List<ProductsDto>();
        }
    }
}
