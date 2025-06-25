using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Web.Models;

namespace ShoppingCart.Web.Service.IService
{
    public interface IOrderService
    {
        Task<ResponseDto?> CreateOrder(CartDto cartDto);
        Task<ResponseDto?> GetAllOrder(string? userId = "");
        Task<ResponseDto?> GetOrder(int id);
        Task<ResponseDto?> UpdateOrderStatus(int orderId, string newStatus);
    }
}
