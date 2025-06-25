using ShoppingCart.Web.Models;

namespace ShoppingCart.Web.Service.IService
{
    public interface IProductService
    {
       Task<ResponseDto?> GetProductByIdAsync(int productId);
        Task<ResponseDto?> GetAllProductAsync();
        Task<ResponseDto?> CreateProductAsync(ProductsDto productDto);
        Task<ResponseDto?> UpdatedProductAsync(ProductsDto productDto);
        Task<ResponseDto?> DeleteProductAsync(int productId);

    }
}
