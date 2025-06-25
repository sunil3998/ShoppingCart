using ShoppingCart.Service.OrderApi.Models.Dto;

namespace ShoppingCart.Service.OrderApi.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductsDto>> GetProducts();
    }
}
