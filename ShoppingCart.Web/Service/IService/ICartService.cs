using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Web.Models;

namespace ShoppingCart.Web.Service.IService
{
    public interface ICartService
    {
        Task<ResponseDto> GetCartByUserIdAsync(string UserId);
        Task<ResponseDto> ApplyCouponAsync(CartDto cartDto);
        Task<ResponseDto> RemoveCouponAsync([FromBody] CartDto cartDto);
        Task<ResponseDto> RemoveCartAsync(int cartDetailsId);
        Task<ResponseDto> CartUpsertAsync(CartDto cartDto);
        Task<ResponseDto> RemoveCartWhenCheckout(int cartHeaderId);
    }
}
