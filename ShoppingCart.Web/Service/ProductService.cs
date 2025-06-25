using ShoppingCart.Web.Models;
using ShoppingCart.Web.Service.IService;
using ShoppingCart.Web.Utility;

namespace ShoppingCart.Web.Service
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;
        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> CreateProductAsync(ProductsDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.POST,
                Data = productDto,
                Url = StaticData.ProductApiBase + "/api/Products/Create"
            }, withBearer: true);
        }

        public async Task<ResponseDto?> DeleteProductAsync(int productId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.DELETE,
                Url = StaticData.ProductApiBase + "/api/Products/Delete?id=" + productId
            }, withBearer: true);
        }

        public async Task<ResponseDto?> GetAllProductAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.GET,
                Url = StaticData.ProductApiBase + "/api/Products"
            }, withBearer: true);
        }

        public async Task<ResponseDto?> GetProductByIdAsync(int productId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.GET,
                Url = StaticData.ProductApiBase + "/api/Products/"+ productId
            }, withBearer: true);
        }

        public async Task<ResponseDto?> UpdatedProductAsync(ProductsDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.PUT,
                Data = productDto,
                Url = StaticData.ProductApiBase + "/api/Products"
            }, withBearer: true);
        }
    }
}
