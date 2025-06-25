using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Service.OrdersApi.Models.Dto;

namespace ShoppingCart.Service.OrdersApi.Service.IService
{
    public interface ICartService
    {
        Task<ResponseDto> RemoveCartWhenCheckout(int cartHeaderId);
    }
}
