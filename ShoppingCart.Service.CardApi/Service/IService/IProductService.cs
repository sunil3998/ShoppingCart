using ShoppingCart.Service.CartApi.Models.Dto;

namespace ShoppingCart.Service.CartApi.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductsDto>> GetProducts();
    }
}
