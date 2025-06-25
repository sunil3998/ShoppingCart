using ShoppingCart.Service.OrdersApi.Models.Dto;

namespace ShoppingCart.Service.OrdersApi.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductsDto>> GetProducts();
    }
}
